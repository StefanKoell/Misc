using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMica;

internal partial class AccentComboBoxItem : ObservableObject
{
    [ObservableProperty] public partial Color Color { get; set; } = Colors.Transparent;
    [ObservableProperty] public partial string Text { get; set; } = string.Empty;
    [ObservableProperty] public partial object? Value { get; set; }
}