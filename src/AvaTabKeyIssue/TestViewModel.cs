using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaTabKeyIssue;

public partial class TestViewModel : ObservableObject
{
    [ObservableProperty] private string _caption;
    [ObservableProperty] private bool _expanded = true;
    [ObservableProperty] private ObservableCollection<TestViewModel> _items = new();

    public TestViewModel(string caption, bool addSubItems)
    {
        Caption = caption;
        if (!addSubItems)
            return;
        Items.Add(new TestViewModel(Caption + "-1", false));
        Items.Add(new TestViewModel(Caption + "-2", false));
        Items.Add(new TestViewModel(Caption + "-3", false));
    }
}