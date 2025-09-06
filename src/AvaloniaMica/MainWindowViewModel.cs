using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMica;

internal partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] public partial bool DarkModeEnabled { get; set; }
    [ObservableProperty] public partial bool BorderEnabled { get; set; }
    [ObservableProperty] public partial bool TransparencyEnabled { get; set; }
    [ObservableProperty] public partial bool CustomColorEnabled { get; set; }
    [ObservableProperty] public partial AccentColor SelectedAccentColor { get; set; } = AccentColor.System;
    [ObservableProperty] public partial Color CustomColor { get; set; } = Colors.Coral;
    [ObservableProperty] public partial ObservableCollection<AccentComboBoxItem> AccentColors { get; set; } = [];
    [ObservableProperty] public partial ObservableCollection<WindowTransparencyLevel> TransparencyLevelHint { get; set; } = [];
    
    public MainWindowViewModel()
    {
        DarkModeEnabled = Application.Current!.ActualThemeVariant == ThemeVariant.Dark;
        foreach (AccentColor value in Enum.GetValues(typeof(AccentColor)))
        {
            AccentColors.Add(new AccentComboBoxItem
            {
                Color = App.ColorPaletteFactory!.GetColor(value),
                Text = value.ToString(),
                Value = value,
            });
        }
    }

    public void UpdateSystemColor() => AccentColors[0].Color = App.ColorPaletteFactory!.GetColor(AccentColor.System);

    partial void OnDarkModeEnabledChanged(bool value) =>
        Application.Current!.RequestedThemeVariant = value 
            ? ThemeVariant.Dark 
            : ThemeVariant.Light;

    partial void OnBorderEnabledChanged(bool value) => 
        App.ColorPaletteFactory?.BorderAccentColorEnabled = value;

    partial void OnTransparencyEnabledChanged(bool value)
    {
        if (value && TransparencyLevelHint.Contains(WindowTransparencyLevel.Mica))
            return;

        TransparencyLevelHint = value
            ? [WindowTransparencyLevel.Mica]
            : [];
        
        Application.Current!.Resources[nameof(UIGlobal.TransparencyEnabled)] = value;
    }

    partial void OnSelectedAccentColorChanged(AccentColor value)
    {
        App.ColorPaletteFactory!.AccentColor = value;
        CustomColorEnabled = value == AccentColor.Custom;
    }

    partial void OnCustomColorChanged(Color value)
    {
        App.ColorPaletteFactory!.CustomAccentColor = value;
        AccentColors[1].Color = value;
    }
}