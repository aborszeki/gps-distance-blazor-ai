namespace GeoDistance.Web.Api;

public sealed record DistanceResponse(double Kilometers, double Miles, double Meters, string Model, string Method);