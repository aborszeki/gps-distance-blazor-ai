using GeoDistance.Domain.Music;
using Microsoft.JSInterop;

namespace GeoDistance.Web.Interop;

/// <summary>Web Audio synthesizer (module: js/audio.js).</summary>
public sealed class AudioPlayer(IJSRuntime js) : JsModule(js, "./js/audio.js")
{
    /// <summary>Creates/resumes the audio context. Call it first thing in a click handler: browsers only allow sound after a user gesture.</summary>
    public ValueTask UnlockAsync() => InvokeVoidAsync("unlock");

    public ValueTask PlayAsync(ToneSequence sequence) => InvokeVoidAsync("play", sequence);
}
