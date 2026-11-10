namespace GeoDistance.Domain.Places;

public sealed record Continent(string Name, IReadOnlyList<Country> Countries);