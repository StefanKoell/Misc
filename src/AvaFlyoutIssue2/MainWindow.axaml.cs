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
        // trying this workaround is also not working
        // in this case, I can't navigate anywhere using the keyboard
        // FlyoutButton1.Flyout.Opened += (sender, args) =>
        // {
        //     Dispatcher.UIThread.Invoke(() =>
        //     {
        //         ButtonA.Focus();
        //     }, DispatcherPriority.Background);
        // };
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