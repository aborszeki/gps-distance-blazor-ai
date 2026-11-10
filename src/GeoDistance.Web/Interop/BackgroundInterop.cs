using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>Draws the real-world continents behind the page (module: js/background.js).</summary>
public sealed class BackgroundInterop(IJSRuntime js) : JsModule(js, "./js/background.js")
{
    public ValueTask DrawWorldAsync(ElementReference container) => InvokeVoidAsync("drawWorld", container);
}
