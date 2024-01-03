using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaTabKeyIssue;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<TestViewModel> _items = new();

    public MainWindowViewModel()
    {
        Items.Add(new TestViewModel("Item _1", true));
        Items.Add(new TestViewModel("Item _2", true));
        Items.Add(new TestViewModel("Item _3", true));
    }
}