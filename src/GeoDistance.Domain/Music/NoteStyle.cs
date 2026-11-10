namespace GeoDistance.Domain.Music;

/// <summary>How a note's volume evolves: quick attack + long decay (Pluck) or held (Reed, Sustain).</summary>
public enum NoteStyle { Pluck, Reed, Sustain }