using System.Collections.Generic;
using AvaMarkupExtensionTranslate.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaMarkupExtensionTranslate.Utilities;

public enum Language
{
    English,
    German
}

public static class LocalizationService
{
    private static Language _language;

    private static readonly Dictionary<string, string> English = new()
    {
        { "Welcome", "Welcome" },
        { "English", "English" },
        { "German", "German" },
    };

    private static readonly Dictionary<string, string> German = new()
    {
        { "Welcome", "Willkommen" },
        { "English", "Englisch" },
        { "German", "Deutsch" },
    };

    public static void SetLanguage(Language language)
    {
        _language = language;
        WeakReferenceMessenger.Default.Send(new LanguageChangedMessage());
    }
    public static string Translate(string text)
    {
        return _language switch
        {
            Language.English => English.GetValueOrDefault(text, text),
            Language.German => German.GetValueOrDefault(text, text),
            _ => text
        };
    } 
}