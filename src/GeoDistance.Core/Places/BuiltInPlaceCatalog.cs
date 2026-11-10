using GeoDistance.Domain.Places;
using GeoDistance.Domain.Geodesy;

namespace GeoDistance.Core.Places;

/// <summary>The built-in hierarchy: 3 continents × 3 countries × 3 cities. Immutable, so it can be a singleton.</summary>
public sealed class BuiltInPlaceCatalog : IPlaceCatalog
{
    private static readonly IReadOnlyList<Continent> Data =
    [
        new("Europe",
        [
            new("Hungary", [new("Budapest", new(47.4979, 19.0402)), new("Debrecen", new(47.5316, 21.6273)), new("Szeged", new(46.2530, 20.1414))]),
            new("France", [new("Paris", new(48.8566, 2.3522)), new("Lyon", new(45.7640, 4.8357)), new("Marseille", new(43.2965, 5.3698))]),
            new("Italy", [new("Rome", new(41.9028, 12.4964)), new("Milan", new(45.4642, 9.1900)), new("Naples", new(40.8518, 14.2681))]),
        ]),
        new("Asia",
        [
            new("Japan", [new("Tokyo", new(35.6762, 139.6503)), new("Osaka", new(34.6937, 135.5023)), new("Kyoto", new(35.0116, 135.7681))]),
            new("India", [new("New Delhi", new(28.6139, 77.2090)), new("Mumbai", new(19.0760, 72.8777)), new("Bengaluru", new(12.9716, 77.5946))]),
            new("China", [new("Beijing", new(39.9042, 116.4074)), new("Shanghai", new(31.2304, 121.4737)), new("Guangzhou", new(23.1291, 113.2644))]),
        ]),
        new("North America",
        [
            new("United States", [new("Washington, D.C.", new(38.9072, -77.0369)), new("New York", new(40.7128, -74.0060)), new("Los Angeles", new(34.0522, -118.2437))]),
            new("Canada", [new("Ottawa", new(45.4215, -75.6972)), new("Toronto", new(43.6532, -79.3832)), new("Vancouver", new(49.2827, -123.1207))]),
            new("Mexico", [new("Mexico City", new(19.4326, -99.1332)), new("Guadalajara", new(20.6597, -103.3496)), new("Monterrey", new(25.6866, -100.3161))]),
        ]),
    ];

    public IReadOnlyList<Continent> Continents => Data;

    public City? FindCity(GeoPoint location) =>
        Data.SelectMany(c => c.Countries).SelectMany(c => c.Cities).FirstOrDefault(c => c.Location == location);

    public Country? FindCountry(GeoPoint location) =>
        Data.SelectMany(c => c.Countries).FirstOrDefault(c => c.Cities.Any(city => city.Location == location));
}
