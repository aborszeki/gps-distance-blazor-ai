using GeoDistance.Core.Geodesy;
using GeoDistance.Core.Music;
using GeoDistance.Core.Places;
using GeoDistance.Core.Travel;
using GeoDistance.Domain.Geodesy;
using GeoDistance.Domain.Music;
using GeoDistance.Domain.Places;
using GeoDistance.Domain.Travel;

namespace GeoDistance.Web.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the domain services of <c>GeoDistance.Core</c>. They are stateless and immutable, so one
    /// instance (singleton) is shared by every browser tab and by the <c>/distance</c> API.
    /// To use other data or another algorithm, register a different implementation of the interface here.
    /// </summary>
    public static IServiceCollection AddGeoDistanceCore(this IServiceCollection services)
    {
        services.AddSingleton<IDistanceCalculator, GeodesicCalculator>();
        services.AddSingleton<IRouteInterpolator, GreatCircleInterpolator>();
        services.AddSingleton<ITravelEstimator, TravelEstimator>();
        services.AddSingleton<IPlaceCatalog, BuiltInPlaceCatalog>();
        services.AddSingleton<ITuneCatalog, BuiltInTuneCatalog>();
        return services;
    }
}
