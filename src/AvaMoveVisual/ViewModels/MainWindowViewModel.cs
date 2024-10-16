using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaMoveVisual.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    
    [ObservableProperty] private ObservableCollection<string> _items = new();
    [ObservableProperty] private string? _selectedItem;

    public MainWindowViewModel()
    {
        Items.Add("1");
        Items.Add("2");
        Items.Add("3");
        SelectedItem = "1";
    }
}