namespace GeoDistance.Domain.Music;

/// <summary>One note to play, in seconds from the start of the tune.</summary>
public sealed record ScheduledNote(double Frequency, double Start, double Duration);