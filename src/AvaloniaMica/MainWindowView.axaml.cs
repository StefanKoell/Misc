using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaMica;

internal partial class MainWindowView : Window
{
    private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;
    public MainWindowView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        ViewModel.UpdateSystemColor();
        App.MainWindow?.PlatformSettings?.ColorValuesChanged += (_, _) =>
        {
            ViewModel.UpdateSystemColor();
        };
    }
}