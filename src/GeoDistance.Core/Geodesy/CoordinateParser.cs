using GeoDistance.Domain.Geodesy;
using System.Globalization;

namespace GeoDistance.Core.Geodesy;

public static class CoordinateParser
{
    /// <summary>
    /// Parses "lat, lon" (comma separated, '.' as decimal mark). Range checks are left to <see cref="GeoPoint.IsValid"/>.
    /// </summary>
    public static bool TryParse(string? text, out GeoPoint point)
    {
        point = default;

        var parts = text?.Split(',', StringSplitOptions.TrimEntries);
        if (parts is not [var lat, var lon])
        {
            return false;
        }

        const NumberStyles Style = NumberStyles.Float;
        if (!double.TryParse(lat, Style, CultureInfo.InvariantCulture, out double latitude) ||
            !double.TryParse(lon, Style, CultureInfo.InvariantCulture, out double longitude))
        {
            return false;
        }

        point = new GeoPoint(latitude, longitude);
        return true;
    }
}
