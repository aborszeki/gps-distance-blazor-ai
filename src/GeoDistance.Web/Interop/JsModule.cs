using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>Base class for services wrapping an ES module under <c>wwwroot/js</c>; the module is imported on first use.</summary>
public abstract class JsModule(IJSRuntime js, string path) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> module =
        new(() => js.InvokeAsync<IJSObjectReference>("import", path).AsTask());

    protected async ValueTask InvokeVoidAsync(string identifier, params object?[] args)
    {
        var jsModule = await module.Value;
        await jsModule.InvokeVoidAsync(identifier, args);
    }

    protected async ValueTask<T> InvokeAsync<T>(string identifier, params object?[] args)
    {
        var jsModule = await module.Value;
        return await jsModule.InvokeAsync<T>(identifier, args);
    }

    public virtual async ValueTask DisposeAsync()
    {
        if (!module.IsValueCreated)
        {
            return;
        }

        try
        {
            await (await module.Value).DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // The circuit is already gone; nothing left to release.
        }
    }
}
