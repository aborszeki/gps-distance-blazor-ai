using GeoDistance.Web.Interop;

namespace GeoDistance.Web.Localization;

/// <summary>
/// The current UI language for one browser tab. The choice is remembered in localStorage;
/// on the first visit the browser language is used (English when it is not supported).
/// </summary>
public sealed class Localizer(BrowserStorage storage)
{
    private const string StorageKey = "lang";

    public string Language { get; private set; } = UiTexts.DefaultLanguage;

    /// <summary>Raised after the language changed so components can re-render.</summary>
    public event Action? Changed;

    public string this[string key] => UiTexts.Text(Language, key);

    /// <summary>Translated name of a continent, country or city (English names are the keys).</summary>
    public string Place(string englishName) => UiTexts.PlaceName(Language, englishName);

    /// <summary>Reads the saved or browser language. Needs JS interop, so call it after the first render.</summary>
    public async Task InitializeAsync()
    {
        var saved = await storage.GetItemAsync(StorageKey);
        var language = UiTexts.IsSupported(saved) ? saved! : await storage.GetBrowserLanguageAsync();
        await ApplyAsync(UiTexts.IsSupported(language) ? language : UiTexts.DefaultLanguage);
    }

    public async Task SetLanguageAsync(string code)
    {
        if (!UiTexts.IsSupported(code) || code == Language)
        {
            return;
        }

        await storage.SetItemAsync(StorageKey, code);
        await ApplyAsync(code);
    }

    private async Task ApplyAsync(string code)
    {
        Language = code;
        await storage.SetDocumentLanguageAsync(code);
        Changed?.Invoke();
    }
}
