namespace GeoDistance.Domain.Travel;

public interface ITravelEstimator
{
    /// <summary>Rough average speed in km/h.</summary>
    double SpeedKmh(TravelMode mode);

    /// <summary>Straight-line travel time. Minutes are dropped once the trip takes a day or more.</summary>
    TravelDuration Estimate(double kilometers, TravelMode mode);
}
