namespace GeoDistance.Domain.Geodesy;

/// <summary>A computed geodesic distance.</summary>
public readonly record struct DistanceResult(double Meters, DistanceMethod Method)
{
    private const double MilesPerKilometer = 0.621371192;

    public double Kilometers => Meters / 1000;

    public double Miles => Kilometers * MilesPerKilometer;
}