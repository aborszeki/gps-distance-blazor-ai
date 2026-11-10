namespace GeoDistance.Domain.Geodesy;

/// <summary>Computes the distance between two points on an Earth model.</summary>
public interface IDistanceCalculator
{
    /// <summary>
    /// Shortest distance between <paramref name="from"/> and <paramref name="to"/> on the surface of
    /// <paramref name="ellipsoid"/>. Implementations may fall back to an estimate when an exact result is not
    /// reachable; <see cref="DistanceResult.Method"/> says which was used.
    /// </summary>
    DistanceResult Distance(GeoPoint from, GeoPoint to, Ellipsoid ellipsoid);
}
