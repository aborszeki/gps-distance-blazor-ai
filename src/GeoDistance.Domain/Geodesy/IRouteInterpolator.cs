namespace GeoDistance.Domain.Geodesy;

public interface IRouteInterpolator
{
    /// <summary>
    /// Points along the route from <paramref name="from"/> to <paramref name="to"/>.
    /// Longitudes are unwrapped (they may leave ±180°) so a line crossing the antimeridian stays continuous on a map.
    /// </summary>
    IReadOnlyList<GeoPoint> Interpolate(GeoPoint from, GeoPoint to, int segments = 64);
}
