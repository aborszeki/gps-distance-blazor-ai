namespace GeoDistance.Domain.Music;

/// <summary>A tune rendered to absolute frequencies and times, ready for a synthesizer.</summary>
public sealed record ToneSequence(
    string Wave,
    string Style,
    IReadOnlyList<ScheduledNote> Notes,
    IReadOnlyList<double> DroneFrequencies,
    double TotalSeconds);