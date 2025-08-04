using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaDevToolIssue;

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

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        
        var overlayPopup = new Popup
        {
            PlacementTarget = Content,
            Placement = PlacementMode.AnchorAndGravity,
            PlacementConstraintAdjustment = PopupPositionerConstraintAdjustment.None,
            IsLightDismissEnabled = false,
            Width = 300,
            Height = 300,
            Topmost = false,
            ShouldUseOverlayLayer = true,
        };

        var border = new Border()
        {
            Background = new SolidColorBrush(Colors.CornflowerBlue)
        };

        border.Child = new TextBlock()
        {
            Text = "Hello Overlay"
        };
        
        overlayPopup.Child = border;
        
        DocContent.Children.Add(overlayPopup);

        overlayPopup.Open();
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