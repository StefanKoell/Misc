using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Threading;

namespace AvaFlyoutIssue;

public partial class UserControlLife : UserControl
{
    public UserControlLife()
    {
        // #1
        InitializeComponent();
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        // #2
        base.OnAttachedToLogicalTree(e);
    }

    protected override void OnInitialized()
    {
        // #3
        base.OnInitialized();
    }

    protected override void OnDataContextBeginUpdate()
    {
        // #4
        base.OnDataContextBeginUpdate();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        // #5
        base.OnDataContextChanged(e);
    }

    protected override void OnDataContextEndUpdate()
    {
        // #6
        base.OnDataContextEndUpdate();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        // #7
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        // #8
        base.OnApplyTemplate(e);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        // #9
        base.OnLoaded(e);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
    }

    protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        // #10
        base.OnDetachedFromLogicalTree(e);
    }
    
    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        // #11
        base.OnDetachedFromVisualTree(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        // #12
        base.OnUnloaded(e);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        Dispatcher.UIThread.Post(() =>
        {
            var popup = button.GetLogicalAncestors().OfType<Popup>().FirstOrDefault();
            popup?.Host?.Hide();

            // Execute following code as a workaround:
            // var flyout = FlyoutButton.Flyout;
            // FlyoutButton.Flyout = null;
            // FlyoutButton.Flyout = flyout;
        });
    }
}