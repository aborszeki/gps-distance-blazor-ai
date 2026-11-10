namespace GeoDistance.Domain.Geodesy;

/// <summary>How a distance was obtained.</summary>
public enum DistanceMethod
{
    /// <summary>Vincenty's inverse formula on the ellipsoid (≈ millimetre accuracy).</summary>
    Vincenty,

    /// <summary>Haversine on the ellipsoid's mean radius, used when Vincenty does not converge (nearly antipodal points).</summary>
    SphereFallback,
}