using GeoDistance.Domain.Geodesy;

namespace GeoDistance.Domain.Places;

/// <summary>The continent &gt; country &gt; city hierarchy the user can pick from.</summary>
public interface IPlaceCatalog
{
    IReadOnlyList<Continent> Continents { get; }

    /// <summary>The listed city at exactly these coordinates, if any.</summary>
    City? FindCity(GeoPoint location);

    /// <summary>The country of the listed city at exactly these coordinates, if any.</summary>
    Country? FindCountry(GeoPoint location);
}
