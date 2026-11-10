using GeoDistance.Domain.Geodesy;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>Leaflet route map (module: js/map.js).</summary>
public sealed class MapInterop(IJSRuntime js) : JsModule(js, "./js/map.js")
{
    /// <summary>Draws the dotted route and both markers in <paramref name="element"/>.</summary>
    /// <param name="onEscape">Called from JavaScript when Escape is pressed (it must expose an invokable <c>Close</c>).</param>
    public ValueTask ShowAsync(
        ElementReference element,
        IReadOnlyList<GeoPoint> route,
        IReadOnlyList<MapMarker> markers,
        DotNetObjectReference<MapDialogCallbacks> onEscape) =>
        InvokeVoidAsync("show", element, route.Select(p => new[] { p.Latitude, p.Longitude }), markers, onEscape);

    public ValueTask DestroyAsync() => InvokeVoidAsync("destroy");
}
