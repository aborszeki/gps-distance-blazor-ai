using GeoDistance.Domain.Travel;
namespace GeoDistance.Core.Travel;

public sealed class TravelEstimator : ITravelEstimator
{
    /// <inheritdoc />
    public double SpeedKmh(TravelMode mode) => mode switch
    {
        TravelMode.Walk => 5,
        TravelMode.Cycle => 15,
        TravelMode.Car => 80,
        TravelMode.Plane => 850,
        _ => throw new ArgumentOutOfRangeException(nameof(mode)),
    };

    /// <inheritdoc />
    public TravelDuration Estimate(double kilometers, TravelMode mode)
    {
        long total = (long)Math.Round(kilometers / SpeedKmh(mode) * 60, MidpointRounding.AwayFromZero);
        int days = (int)(total / 1440);
        int hours = (int)(total % 1440 / 60);
        int minutes = days > 0 ? 0 : (int)(total % 60);
        return new(days, hours, minutes);
    }
}
