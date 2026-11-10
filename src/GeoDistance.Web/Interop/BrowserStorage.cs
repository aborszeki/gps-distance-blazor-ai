using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>localStorage and a few document-level browser facts (module: js/storage.js).</summary>
public sealed class BrowserStorage(IJSRuntime js) : JsModule(js, "./js/storage.js")
{
    public ValueTask<string?> GetItemAsync(string key) => InvokeAsync<string?>("getItem", key);

    public ValueTask SetItemAsync(string key, string value) => InvokeVoidAsync("setItem", key, value);

    public ValueTask<string> GetBrowserLanguageAsync() => InvokeAsync<string>("getBrowserLanguage");

    public ValueTask SetDocumentLanguageAsync(string code) => InvokeVoidAsync("setDocumentLanguage", code);
}
