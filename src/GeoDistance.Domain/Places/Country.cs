namespace GeoDistance.Domain.Places;

public sealed record Country(string Name, IReadOnlyList<City> Cities);