using GeoDistance.Domain.Geodesy;
namespace GeoDistance.Core.Geodesy;

public sealed class GreatCircleInterpolator : IRouteInterpolator
{
    /// <inheritdoc />
    public IReadOnlyList<GeoPoint> Interpolate(GeoPoint from, GeoPoint to, int segments = 64)
    {
        static double Rad(double d) => d * Math.PI / 180;
        static double Deg(double r) => r * 180 / Math.PI;

        double p1 = Rad(from.Latitude), l1 = Rad(from.Longitude);
        double p2 = Rad(to.Latitude), l2 = Rad(to.Longitude);

        double d = 2 * Math.Asin(Math.Sqrt(
            Math.Pow(Math.Sin((p2 - p1) / 2), 2) +
            Math.Cos(p1) * Math.Cos(p2) * Math.Pow(Math.Sin((l2 - l1) / 2), 2)));

        if (d < 1e-9 || Math.Abs(Math.Sin(d)) < 1e-9)
        {
            return [from, to];
        }

        var points = new List<GeoPoint>(segments + 1);
        double previousLon = from.Longitude;

        for (int i = 0; i <= segments; i++)
        {
            double f = (double)i / segments;
            double a = Math.Sin((1 - f) * d) / Math.Sin(d);
            double b = Math.Sin(f * d) / Math.Sin(d);

            double x = a * Math.Cos(p1) * Math.Cos(l1) + b * Math.Cos(p2) * Math.Cos(l2);
            double y = a * Math.Cos(p1) * Math.Sin(l1) + b * Math.Cos(p2) * Math.Sin(l2);
            double z = a * Math.Sin(p1) + b * Math.Sin(p2);

            double lon = Deg(Math.Atan2(y, x));
            while (lon - previousLon > 180)
            {
                lon -= 360;
            }

            while (lon - previousLon < -180)
            {
                lon += 360;
            }

            previousLon = lon;
            points.Add(new GeoPoint(Deg(Math.Atan2(z, double.Hypot(x, y))), lon));
        }

        return points;
    }
}
