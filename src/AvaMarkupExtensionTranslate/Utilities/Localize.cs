using AvaMarkupExtensionTranslate.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaMarkupExtensionTranslate.Utilities;

public partial class Localize : ObservableObject
{
    private readonly string _text;
    [ObservableProperty] private string? _value;

    public Localize(string text)
    {
        _text = text;
        WeakReferenceMessenger.Default.Register<LanguageChangedMessage>(this, OnLanguageChanged);
        OnLanguageChanged(this, null);
    }

    private static void OnLanguageChanged(object recipient, LanguageChangedMessage? message)
    {
        var self = (Localize)recipient;
        self.Value = LocalizationService.Translate(self._text);
    }
}