using GeoDistance.Domain.Geodesy;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GeoDistance.Web.Api;

public static class DistanceEndpoints
{
    /// <summary>
    /// <c>GET /distance?lat1=&amp;lon1=&amp;lat2=&amp;lon2=&amp;model=wgs84</c> — the same calculation the page uses.
    /// </summary>
    public static IEndpointRouteBuilder MapDistanceApi(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/distance", Distance)
            .WithName("GetDistance")
            .WithSummary("Geodesic distance between two GPS coordinates on a chosen reference ellipsoid.");
        return routes;
    }

    private static Results<Ok<DistanceResponse>, BadRequest<ErrorResponse>> Distance(
        double? lat1, double? lon1, double? lat2, double? lon2, string? model, IDistanceCalculator calculator)
    {
        if (lat1 is null || lon1 is null || lat2 is null || lon2 is null)
        {
            return TypedResults.BadRequest(new ErrorResponse("Required query params: lat1, lon1, lat2, lon2"));
        }

        GeoPoint from = new(lat1.Value, lon1.Value), to = new(lat2.Value, lon2.Value);
        if (!from.IsValid || !to.IsValid)
        {
            return TypedResults.BadRequest(new ErrorResponse("lat must be in [-90, 90], lon in [-180, 180]"));
        }

        if (EarthModels.Resolve(model) is not { } ellipsoid)
        {
            var known = string.Join(", ", EarthModels.All.Select(m => m.Key));
            return TypedResults.BadRequest(new ErrorResponse($"Unknown model. Use one of: {known}"));
        }

        var result = calculator.Distance(from, to, ellipsoid);
        return TypedResults.Ok(new DistanceResponse(
            Kilometers: Math.Round(result.Kilometers, 3),
            Miles: Math.Round(result.Miles, 3),
            Meters: Math.Round(result.Meters, 1),
            Model: ellipsoid.Name,
            Method: result.Method == DistanceMethod.Vincenty ? "vincenty" : "sphere-fallback"));
    }
}
