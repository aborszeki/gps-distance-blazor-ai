using Microsoft.AspNetCore.Components;

namespace GeoDistance.Web.Localization;

/// <summary>Base class for components that show translated text: re-renders when the language changes.</summary>
public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject]
    protected Localizer L { get; set; } = default!;

    protected override void OnInitialized() => L.Changed += OnLanguageChanged;

    private void OnLanguageChanged() => _ = InvokeAsync(StateHasChanged);

    public virtual void Dispose()
    {
        L.Changed -= OnLanguageChanged;
        GC.SuppressFinalize(this);
    }
}
