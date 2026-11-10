namespace GeoDistance.Domain.Geodesy;

/// <summary>A reference ellipsoid: the Earth model horizontal distances are computed on.</summary>
/// <param name="Key">Stable identifier used by the API and the UI.</param>
/// <param name="Name">Short name shown next to a result.</param>
/// <param name="Label">Name shown in the model drop-down.</param>
/// <param name="SemiMajorAxis">Equatorial radius <c>a</c> in metres.</param>
/// <param name="Flattening">Flattening <c>f</c> = (a − b) / a.</param>
public sealed record Ellipsoid(string Key, string Name, string Label, double SemiMajorAxis, double Flattening)
{
    /// <summary>Polar radius <c>b</c> in metres.</summary>
    public double SemiMinorAxis => SemiMajorAxis * (1 - Flattening);

    /// <summary>Mean radius (2a + b) / 3 in metres.</summary>
    public double MeanRadius => SemiMajorAxis * (1 - Flattening / 3);
}
