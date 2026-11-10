using GeoDistance.Core.Geodesy;
using GeoDistance.Domain.Geodesy;
using GeoDistance.Domain.Places;

namespace GeoDistance.Web.Models;

/// <summary>What a location combobox holds: either a picked city or free text ("lat, lon").</summary>
public sealed class LocationInput
{
    /// <summary>The picked city; <c>null</c> when the user typed (typing replaces a pick).</summary>
    public City? City { get; private set; }

    /// <summary>Typed text (only meaningful when no city is picked).</summary>
    public string Text { get; private set; } = "";

    public void Pick(City city) => City = city;

    public void Type(string? text)
    {
        City = null;
        Text = text ?? "";
    }

    public void Clear()
    {
        City = null;
        Text = "";
    }

    /// <summary>A picked city supplies its coordinates; otherwise the typed text is parsed.</summary>
    public bool TryGetPoint(out GeoPoint point)
    {
        if (City is { } city)
        {
            point = city.Location;
            return true;
        }

        return CoordinateParser.TryParse(Text, out point);
    }
}
