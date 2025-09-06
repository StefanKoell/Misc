using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace AvaloniaMica;

internal partial class App : Application
{
    public static TopLevel? MainWindow { get; private set; }
    public static ModernTheme? Theme => ModernTheme.TryGetCurrent(out var theme) ? theme : null;
    public static ColorPaletteFactory? ColorPaletteFactory => Theme?.Definition?.ColorPaletteFactory as ColorPaletteFactory;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindowView
            {
                DataContext = new MainWindowViewModel(),
            };
            MainWindow = desktop.MainWindow;
            ColorPaletteFactory?.StartSystemAccentColorWatcher();
        }

        base.OnFrameworkInitializationCompleted();
    }
}