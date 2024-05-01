using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaMarkupExtensionTranslate.Utilities;

namespace AvaMarkupExtensionTranslate;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ButtonEnglish_OnClick(object? sender, RoutedEventArgs e)
    {
        LocalizationService.SetLanguage(Language.English);
    }

    private void ButtonGerman_OnClick(object? sender, RoutedEventArgs e)
    {
        LocalizationService.SetLanguage(Language.German);
    }
}