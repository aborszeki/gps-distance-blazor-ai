using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>JavaScript → .NET callbacks of the map dialog.</summary>
public sealed class MapDialogCallbacks(Func<Task> close)
{
    [JSInvokable]
    public Task Close() => close();
}