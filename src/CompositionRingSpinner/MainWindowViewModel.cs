using CommunityToolkit.Mvvm.Input;

namespace CompositionRingSpinner;

public partial class MainWindowViewModel
{
    [RelayCommand]
    private static void BlockUiThread()
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
}
