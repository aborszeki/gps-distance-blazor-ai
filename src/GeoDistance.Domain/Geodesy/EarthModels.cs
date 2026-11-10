namespace GeoDistance.Domain.Geodesy;

/// <summary>The five supported Earth models, WGS 84 first (the default).</summary>
public static class EarthModels
{
    public static IReadOnlyList<Ellipsoid> All { get; } =
    [
        new("wgs84", "WGS 84", "WGS 84 (GPS)", 6378137.0, 1 / 298.257223563),
        new("grs80", "GRS 80", "GRS 80", 6378137.0, 1 / 298.257222101),
        new("intl1924", "International 1924", "International 1924 (Hayford)", 6378388.0, 1 / 297.0),
        new("clarke1866", "Clarke 1866", "Clarke 1866", 6378206.4, 1 / 294.9786982),
        new("bessel1841", "Bessel 1841", "Bessel 1841", 6377397.155, 1 / 299.1528128),
    ];

    public static Ellipsoid Default => All[0];

    /// <summary>Finds a model by key (case-insensitive). A blank key means the default; an unknown key gives <c>null</c>.</summary>
    public static Ellipsoid? Resolve(string? key) =>
        string.IsNullOrWhiteSpace(key)
            ? Default
            : All.FirstOrDefault(m => string.Equals(m.Key, key, StringComparison.OrdinalIgnoreCase));
}