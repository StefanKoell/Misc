using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;

namespace CompositionRingSpinner.Controls;

public sealed class CompositionRingSpinner : Control
{
    private CompositionCustomVisual? _customVisual;
    private CompositionRingSpinnerVisualHandler? _handler;
    private List<IDisposable>? _visibilitySubscriptions;
    private CompositionRingSpinnerVisualState? _lastVisualState;

    public static readonly StyledProperty<bool> IsActiveProperty =
        AvaloniaProperty.Register<CompositionRingSpinner, bool>(nameof(IsActive), true);

    public static readonly StyledProperty<Color> ForegroundColorProperty =
        AvaloniaProperty.Register<CompositionRingSpinner, Color>(nameof(ForegroundColor), Colors.DodgerBlue);

    public static readonly StyledProperty<Color> TrackColorProperty =
        AvaloniaProperty.Register<CompositionRingSpinner, Color>(nameof(TrackColor), Colors.Transparent);

    public static readonly StyledProperty<double> StrokeThicknessProperty =
        AvaloniaProperty.Register<CompositionRingSpinner, double>(nameof(StrokeThickness), 2D);

    public bool IsActive
    {
        get => GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public Color ForegroundColor
    {
        get => GetValue(ForegroundColorProperty);
        set => SetValue(ForegroundColorProperty, value);
    }

    public Color TrackColor
    {
        get => GetValue(TrackColorProperty);
        set => SetValue(TrackColorProperty, value);
    }

    public double StrokeThickness
    {
        get => GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (!double.IsFinite(availableSize.Width) && !double.IsFinite(availableSize.Height))
            return default;

        var availableSide = double.IsFinite(availableSize.Width) && double.IsFinite(availableSize.Height)
            ? Math.Min(availableSize.Width, availableSize.Height)
            : double.IsFinite(availableSize.Width)
                ? availableSize.Width
                : availableSize.Height;
        availableSide = Math.Max(0D, availableSide);
        return new Size(availableSide, availableSide);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        UpdateVisual();
        return finalSize;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        AttachVisibilitySubscriptions();
        AttachVisual();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        DetachVisibilitySubscriptions();
        _customVisual?.SendHandlerMessage(
            new CompositionRingSpinnerVisualState(
                false,
                default,
                ForegroundColor,
                TrackColor,
                Math.Max(1D, StrokeThickness)));
        ElementComposition.SetElementChildVisual(this, null);
        _customVisual = null;
        _handler = null;
        _lastVisualState = null;
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IsActiveProperty
            || change.Property == ForegroundColorProperty
            || change.Property == TrackColorProperty
            || change.Property == StrokeThicknessProperty
            || change.Property == IsVisibleProperty
            || change.Property == BoundsProperty)
        {
            UpdateVisual();
        }
    }

    private void AttachVisual()
    {
        var elementVisual = ElementComposition.GetElementVisual(this);
        if (elementVisual == null)
            return;

        _handler = new CompositionRingSpinnerVisualHandler();
        _customVisual = elementVisual.Compositor.CreateCustomVisual(_handler);
        ElementComposition.SetElementChildVisual(this, _customVisual);
        UpdateVisual();
    }

    private void AttachVisibilitySubscriptions()
    {
        DetachVisibilitySubscriptions();

        _visibilitySubscriptions =
        [
            this.GetObservable(IsVisibleProperty).Subscribe(new VisibilityObserver(UpdateVisual)),
        ];

        foreach (var ancestor in this.GetVisualAncestors())
        {
            _visibilitySubscriptions.Add(
                ancestor.GetObservable(IsVisibleProperty).Subscribe(new VisibilityObserver(UpdateVisual)));
        }
    }

    private void DetachVisibilitySubscriptions()
    {
        if (_visibilitySubscriptions is null)
            return;

        foreach (var disposable in _visibilitySubscriptions)
            disposable.Dispose();

        _visibilitySubscriptions = null;
    }

    private void UpdateVisual()
    {
        if (_customVisual == null || _handler == null)
            return;

        _customVisual.Size = new Vector(Bounds.Width, Bounds.Height);
        var isRenderingActive = IsActive
                                && IsEffectivelyVisible
                                && Bounds is { Width: > 0D, Height: > 0D };
        var state = new CompositionRingSpinnerVisualState(
            isRenderingActive,
            Bounds.Size,
            ForegroundColor,
            TrackColor,
            Math.Max(1D, StrokeThickness));

        if (_lastVisualState == state)
            return;

        _lastVisualState = state;
        _customVisual.SendHandlerMessage(state);
    }

    private sealed class VisibilityObserver(Action valueChanged) : IObserver<bool>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(bool value)
        {
            valueChanged();
        }
    }
}
