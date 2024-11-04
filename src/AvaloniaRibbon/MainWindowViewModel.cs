using System.Collections.ObjectModel;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaRibbon;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private bool _darkModeEnabled;
    [ObservableProperty] private bool _borderEnabled;

    [ObservableProperty] private bool _transparencyEnabled;
    [ObservableProperty] private ObservableCollection<WindowTransparencyLevel> _transparencyLevelHint = [];
    
    [ObservableProperty] private RibbonViewModel _ribbon;

    public MainWindowViewModel()
    {
        _darkModeEnabled = Application.Current!.ActualThemeVariant == ThemeVariant.Dark;
        
        Ribbon = new()
        {
            Tabs =
            {
                new RibbonTabViewModel("File", "File")
                {
                    Groups =
                    {
                        new RibbonGroupViewModel("Document", "Document")
                        {
                            Items =
                            {
                                new BarButtonViewModel("Test", "Test")
                                {
                                    Command = new RelayCommand(() => {}),
                                }
                            }
                        }
                    }
                }
            }
        };
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