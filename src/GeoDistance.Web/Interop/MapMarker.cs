namespace GeoDistance.Web.Interop;

/// <summary>A labelled marker on the route map.</summary>
public sealed record MapMarker(double Latitude, double Longitude, string Label);