using GeoDistance.Domain.Geodesy;

namespace GeoDistance.Web.Models;

/// <summary>What the route map dialog should show.</summary>
public sealed record MapRequest(GeoPoint From, GeoPoint To, string FromLabel, string ToLabel, string Title);
