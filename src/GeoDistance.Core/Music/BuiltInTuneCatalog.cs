using GeoDistance.Domain.Music;
namespace GeoDistance.Core.Music;

/// <summary>
/// One short theme per country by a composer from that country (all public domain).
/// The notes were written from memory: approximations of the themes, not exact transcriptions.
/// Immutable, so it can be a singleton.
/// </summary>
public sealed class BuiltInTuneCatalog : ITuneCatalog
{
    private static readonly Dictionary<string, Tune> ByCountry = new()
    {
        ["Hungary"] = new("Franz Liszt", "Hungarian Rhapsody No. 2", 150, Waveform.Sawtooth, NoteStyle.Reed,
            "C#5:1 D#5:.5 E5:.5 G5:1 G#5:1 G5:.5 E5:.5 D#5:.5 C#5:.5 C5:1 C#5:2"),
        ["France"] = new("Erik Satie", "Gymnopédie No. 1", 110, Waveform.Triangle, NoteStyle.Pluck,
            "F#5:2 A5:1 G5:2 F#5:1 C#5:1 B4:1"),
        ["Italy"] = new("Antonio Vivaldi", "Spring (The Four Seasons)", 170, Waveform.Sawtooth, NoteStyle.Reed,
            "E4:1 G#4:1 G#4:1 G#4:.5 F#4:.5 G#4:1 A4:2 A4:1 A4:1 A4:1 G#4:.5 A4:.5 B4:2"),
        ["Japan"] = new("Rentarō Taki", "Kōjō no Tsuki", 150, Waveform.Triangle, NoteStyle.Pluck,
            "E4:1 E4:.5 E4:.5 A4:1 B4:1 C5:1.5 B4:.5 A4:1 E4:2"),
        ["India"] = new("Rabindranath Tagore", "Jana Gana Mana", 170, Waveform.Sine, NoteStyle.Sustain,
            "C4:1 C4:.5 C4:.5 C4:1 D4:1 E4:2 E4:1 D4:.5 E4:.5 F4:2", Drone: ["C3", "G3"]),
        ["China"] = new("Nie Er", "March of the Volunteers", 150, Waveform.Square, NoteStyle.Reed,
            "G4:.5 C5:1.5 C5:.5 E5:1 G5:1 E5:.5 C5:.5 D5:1 C5:2"),
        ["United States"] = new("George Gershwin", "Summertime", 120, Waveform.Triangle, NoteStyle.Sustain,
            "E5:2 E5:.5 D5:.5 C5:1 D5:2 E5:2"),
        ["Canada"] = new("Calixa Lavallée", "O Canada", 160, Waveform.Square, NoteStyle.Reed,
            "G4:.5 C5:1.5 C5:.5 C5:1 E5:1 D5:1 C5:1.5 B4:.5 C5:2"),
        ["Mexico"] = new("Juventino Rosas", "Sobre las olas", 180, Waveform.Sawtooth, NoteStyle.Reed,
            "B4:1 E5:1 E5:1 D#5:1 E5:1 G5:1 F#5:3"),
    };

    public Tune Generic { get; } =
        new("", "", 160, Waveform.Triangle, NoteStyle.Pluck, "C4:.5 E4:.5 G4:.5 C5:.5 E5:1 C5:.5 G4:.5 C5:2");

    public Tune For(string? country) =>
        country is not null && ByCountry.TryGetValue(country, out var tune) ? tune : Generic;
}
