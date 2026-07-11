using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using SkiaSharp;

namespace CompositionRingSpinner.Controls;

internal sealed class CompositionRingSpinnerVisualHandler : CompositionCustomVisualHandler
{
    private const float MinSweepAngle = 10F;
    private const float MaxSweepAngle = 280F;
    private static readonly TimeSpan RootRotationDuration = TimeSpan.FromSeconds(3);
    private static readonly TimeSpan SliceDuration = TimeSpan.FromMilliseconds(1200);

    private bool _isActive = true;
    private Size _size;
    private Color _foregroundColor = Colors.DodgerBlue;
    private Color _trackColor = Colors.Transparent;
    private double _strokeThickness = 2D;
    private TimeSpan? _firstFrame;
    private TimeSpan _currentFrame;
    private bool _isAnimationFrameUpdateRegistered;

    public override void OnMessage(object message)
    {
        if (message is not CompositionRingSpinnerVisualState state)
            return;

        _isActive = state.IsActive;
        _size = state.Size;
        _foregroundColor = state.ForegroundColor;
        _trackColor = state.TrackColor;
        _strokeThickness = state.StrokeThickness;

        if (_isActive)
            RegisterForNextAnimationFrameUpdateIfNeeded();
        else
            _firstFrame = null;

        Invalidate();
    }

    public override void OnAnimationFrameUpdate()
    {
        _isAnimationFrameUpdateRegistered = false;

        if (!_isActive)
            return;

        _currentFrame = CompositionNow;
        _firstFrame ??= _currentFrame;
        Invalidate();
        RegisterForNextAnimationFrameUpdateIfNeeded();
    }

    public override void OnRender(ImmediateDrawingContext drawingContext)
    {
        if (_size.Width <= 0D || _size.Height <= 0D)
            return;

        if (!drawingContext.TryGetFeature<ISkiaSharpApiLeaseFeature>(out var leaseFeature))
            return;

        using var lease = leaseFeature.Lease();
        var canvas = lease.SkCanvas;
        var side = (float)Math.Min(_size.Width, _size.Height);
        if (side <= 0F)
            return;

        var stroke = Math.Clamp((float)_strokeThickness, 1F, side / 2F);
        var inset = stroke / 2F;
        var rect = SKRect.Create(
            ((float)_size.Width - side) / 2F + inset,
            ((float)_size.Height - side) / 2F + inset,
            side - stroke,
            side - stroke);

        if (_trackColor.A > 0)
        {
            using var trackPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeCap = SKStrokeCap.Round,
                StrokeWidth = stroke,
                IsAntialias = true,
                Color = _trackColor.ToSKColor(),
            };

            canvas.DrawOval(rect, trackPaint);
        }

        if (!_isActive)
            return;

        var elapsed = _firstFrame.HasValue ? _currentFrame - _firstFrame.Value : TimeSpan.Zero;
        var rootRotation = Progress(elapsed, RootRotationDuration) * 359.99F;
        var sliceProgress = Progress(elapsed, SliceDuration);
        var sliceRotation = 0F;
        var sweep = MaxSweepAngle;

        if (sliceProgress < 0.33F)
        {
            var progress = EaseOutQuadratic(sliceProgress / 0.33F);
            sweep = Lerp(MinSweepAngle, MaxSweepAngle, progress);
        }
        else if (sliceProgress > 0.66F)
        {
            var progress = EaseOutQuadratic((sliceProgress - 0.66F) / 0.34F);
            sliceRotation = Lerp(0F, 359.99F, progress);
            sweep = Lerp(MaxSweepAngle, MinSweepAngle, progress);
        }

        var start = rootRotation + sliceRotation - 90F;

        using var arcPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = stroke,
            IsAntialias = true,
            Color = _foregroundColor.ToSKColor(),
        };

        canvas.DrawArc(rect, start, sweep, false, arcPaint);
    }

    public override Rect GetRenderBounds() => new(_size);

    private static float Progress(TimeSpan elapsed, TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero)
            return 0F;

        var ticks = elapsed.Ticks % duration.Ticks;
        return (float)((double)ticks / duration.Ticks);
    }

    private static float EaseOutQuadratic(float value)
    {
        value = Math.Clamp(value, 0F, 1F);
        return 1F - (1F - value) * (1F - value);
    }

    private static float Lerp(float start, float end, float progress) =>
        start + (end - start) * progress;

    private void RegisterForNextAnimationFrameUpdateIfNeeded()
    {
        if (_isAnimationFrameUpdateRegistered)
            return;

        _isAnimationFrameUpdateRegistered = true;
        RegisterForNextAnimationFrameUpdate();
    }
}
