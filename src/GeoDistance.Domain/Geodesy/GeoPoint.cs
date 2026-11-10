namespace GeoDistance.Domain.Geodesy;

/// <summary>A position on Earth in decimal degrees.</summary>
public readonly record struct GeoPoint(double Latitude, double Longitude)
{
    /// <summary>True when latitude is within ±90° and longitude within ±180° (false for NaN).</summary>
    public bool IsValid => Latitude is >= -90 and <= 90 && Longitude is >= -180 and <= 180;

    /// <summary>Formats as "lat, lon" using the invariant culture (the format the UI accepts back).</summary>
    public override string ToString() => FormattableString.Invariant($"{Latitude}, {Longitude}");
}
