using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaMoveVisual.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var window = new Window();
        Panel.Children.Remove(Control1);
        var newPanel = new Panel();
        window.Content = newPanel;
        var userControl2 = new UserControl1();
        userControl2.DataContext = Control1.DataContext;
        newPanel.Children.Add(userControl2);
        //newPanel.Children.Add(Control1);
        window.Show(this);
    }
}