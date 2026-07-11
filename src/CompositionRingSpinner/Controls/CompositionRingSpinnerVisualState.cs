using Avalonia;
using Avalonia.Media;

namespace CompositionRingSpinner.Controls;

internal readonly record struct CompositionRingSpinnerVisualState(
    bool IsActive,
    Size Size,
    Color ForegroundColor,
    Color TrackColor,
    double StrokeThickness);
