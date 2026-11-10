using GeoDistance.Domain.Geodesy;
namespace GeoDistance.Core.Geodesy;

/// <summary>Vincenty-based <see cref="IDistanceCalculator"/>; stateless and thread-safe, so it can be a singleton.</summary>
public sealed class GeodesicCalculator : IDistanceCalculator
{
    private const int MaxIterations = 200;
    private const double Tolerance = 1e-12;

    private static double Rad(double degrees) => degrees * Math.PI / 180;

    /// <inheritdoc />
    public DistanceResult Distance(GeoPoint from, GeoPoint to, Ellipsoid ellipsoid)
    {
        double a = ellipsoid.SemiMajorAxis, f = ellipsoid.Flattening, b = ellipsoid.SemiMinorAxis;

        if (from == to)
        {
            return new(0, DistanceMethod.Vincenty);
        }

        double u1 = Math.Atan((1 - f) * Math.Tan(Rad(from.Latitude)));
        double u2 = Math.Atan((1 - f) * Math.Tan(Rad(to.Latitude)));
        double sinU1 = Math.Sin(u1), cosU1 = Math.Cos(u1);
        double sinU2 = Math.Sin(u2), cosU2 = Math.Cos(u2);

        double l = Rad(to.Longitude - from.Longitude);
        double lambda = l, lambdaPrev;
        double sinSigma, cosSigma, sigma, cos2Alpha, cos2SigmaM;
        int iterations = 0;

        do
        {
            double sinLambda = Math.Sin(lambda), cosLambda = Math.Cos(lambda);
            sinSigma = Math.Sqrt(Math.Pow(cosU2 * sinLambda, 2) + Math.Pow(cosU1 * sinU2 - sinU1 * cosU2 * cosLambda, 2));
            if (sinSigma == 0)
            {
                return new(0, DistanceMethod.Vincenty); // coincident points
            }

            cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda;
            sigma = Math.Atan2(sinSigma, cosSigma);
            double sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma;
            cos2Alpha = 1 - sinAlpha * sinAlpha;
            cos2SigmaM = cos2Alpha != 0 ? cosSigma - 2 * sinU1 * sinU2 / cos2Alpha : 0; // 0 on the equatorial line
            double c = f / 16 * cos2Alpha * (4 + f * (4 - 3 * cos2Alpha));

            lambdaPrev = lambda;
            lambda = l + (1 - c) * f * sinAlpha *
                     (sigma + c * sinSigma * (cos2SigmaM + c * cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)));

            if (Math.Abs(lambda) > Math.PI)
            {
                return SphereFallback(from, to, ellipsoid); // antipodal: no convergence
            }
        }
        while (Math.Abs(lambda - lambdaPrev) > Tolerance && ++iterations < MaxIterations);

        if (iterations >= MaxIterations)
        {
            return SphereFallback(from, to, ellipsoid);
        }

        double uSq = cos2Alpha * (a * a - b * b) / (b * b);
        double bigA = 1 + uSq / 16384 * (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)));
        double bigB = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)));
        double deltaSigma = bigB * sinSigma * (cos2SigmaM + bigB / 4 *
            (cosSigma * (-1 + 2 * cos2SigmaM * cos2SigmaM)
             - bigB / 6 * cos2SigmaM * (-3 + 4 * sinSigma * sinSigma) * (-3 + 4 * cos2SigmaM * cos2SigmaM)));

        return new(b * bigA * (sigma - deltaSigma), DistanceMethod.Vincenty);
    }

    private static DistanceResult SphereFallback(GeoPoint from, GeoPoint to, Ellipsoid ellipsoid)
    {
        double dLat = Rad(to.Latitude - from.Latitude);
        double dLon = Rad(to.Longitude - from.Longitude);
        double h = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(Rad(from.Latitude)) * Math.Cos(Rad(to.Latitude)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        return new(2 * ellipsoid.MeanRadius * Math.Asin(Math.Sqrt(h)), DistanceMethod.SphereFallback);
    }
}
