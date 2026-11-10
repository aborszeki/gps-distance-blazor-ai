using GeoDistance.Domain.Geodesy;

namespace GeoDistance.Domain.Places;

/// <summary>A selectable city. Names are English; the UI translates them.</summary>
public sealed record City(string Name, GeoPoint Location);