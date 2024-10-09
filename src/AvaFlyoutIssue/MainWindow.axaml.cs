using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Threading;

namespace AvaFlyoutIssue;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        Dispatcher.UIThread.Post(() =>
        {
            var popup = button.GetLogicalAncestors().OfType<Popup>().FirstOrDefault();
            popup!.IsOpen = false;
        });
    }
}