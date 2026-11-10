using System.Globalization;

namespace GeoDistance.Domain.Music;

/// <summary>
/// A short theme. <paramref name="Notes"/> is a space separated list of <c>name:beats</c>
/// (e.g. <c>"C#5:1 E5:.5"</c>); flats and sharps are written <c>b</c> and <c>#</c>.
/// </summary>
public sealed record Tune(
    string Composer,
    string Piece,
    int Bpm,
    Waveform Wave,
    NoteStyle Style,
    string Notes,
    IReadOnlyList<string>? Drone = null)
{
    /// <summary>Fraction of its length a note sounds before the next one (leaves a tiny gap).</summary>
    private const double Articulation = 0.95;

    public bool HasAttribution => Composer.Length > 0;

    public ToneSequence Render()
    {
        double beat = 60.0 / Bpm;
        double time = 0;
        var notes = new List<ScheduledNote>();

        foreach (var token in Notes.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            var separator = token.IndexOf(':');
            var length = double.Parse(token[(separator + 1)..], CultureInfo.InvariantCulture) * beat;
            notes.Add(new ScheduledNote(NoteFrequency(token[..separator]), time, length * Articulation));
            time += length;
        }

        var drone = Drone?.Select(NoteFrequency).ToArray() ?? [];
        return new ToneSequence(Wave.ToString().ToLowerInvariant(), Style.ToString().ToLowerInvariant(), notes, drone, time);
    }

    /// <summary>Equal-tempered frequency of a note name such as <c>"A4"</c>, <c>"D#5"</c> or <c>"Db4"</c>.</summary>
    public static double NoteFrequency(string name)
    {
        int semitone = name[0] switch
        {
            'C' => 0, 'D' => 2, 'E' => 4, 'F' => 5, 'G' => 7, 'A' => 9, 'B' => 11,
            _ => throw new FormatException($"Unknown note '{name}'."),
        };

        int index = 1;
        if (name[index] == '#')
        {
            semitone++;
            index++;
        }
        else if (name[index] == 'b')
        {
            semitone--;
            index++;
        }

        int octave = int.Parse(name[index..], CultureInfo.InvariantCulture);
        int midi = 12 * (octave + 1) + semitone;
        return 440 * Math.Pow(2, (midi - 69) / 12.0);
    }
}
