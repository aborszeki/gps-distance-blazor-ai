namespace GeoDistance.Domain.Travel;

/// <summary>A duration split into the parts shown to the user.</summary>
public readonly record struct TravelDuration(int Days, int Hours, int Minutes)
{
    public bool IsUnderOneMinute => Days == 0 && Hours == 0 && Minutes == 0;
}