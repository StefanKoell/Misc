using Avalonia.Controls;

namespace AvaTabKeyIssue;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}