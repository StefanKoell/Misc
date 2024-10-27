using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaMica;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private bool _darkModeEnabled;
    [ObservableProperty] private bool _borderEnabled;

    [ObservableProperty] private bool _transparencyEnabled;
    [ObservableProperty] private ObservableCollection<WindowTransparencyLevel> _transparencyLevelHint = [];
    
    public MainWindowViewModel()
    {
        _darkModeEnabled = Application.Current!.ActualThemeVariant == ThemeVariant.Dark;
    }
    
    partial void OnDarkModeEnabledChanged(bool value)
    {
        Application.Current!.RequestedThemeVariant = value 
            ? ThemeVariant.Dark 
            : ThemeVariant.Light;
    }

    partial void OnBorderEnabledChanged(bool value)
    {
        Application.Current!.Resources["UIWindowBorderColorActive"] = value
            ? App.MainWindow.PlatformSettings?.GetColorValues().AccentColor1 ?? Colors.Transparent
            : Colors.Transparent;
    }

    partial void OnTransparencyEnabledChanged(bool value)
    {
        if (value && TransparencyLevelHint.Contains(WindowTransparencyLevel.Mica))
            return;

        TransparencyLevelHint = value
            ? [WindowTransparencyLevel.Mica]
            : [];
        
        Application.Current!.Resources[nameof(UIGlobal.TransparencyEnabled)] = value;
    }
}