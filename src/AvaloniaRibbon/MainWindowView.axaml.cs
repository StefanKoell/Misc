using Avalonia.Controls;

namespace AvaloniaRibbon;

public partial class MainWindowView : Window
{
    public MainWindowView()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}