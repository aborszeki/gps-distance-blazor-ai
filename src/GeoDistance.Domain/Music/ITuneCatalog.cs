namespace GeoDistance.Domain.Music;

/// <summary>Looks up the theme that is played for a destination country.</summary>
public interface ITuneCatalog
{
    /// <summary>A short jingle for destinations that are not one of the listed cities (it has no attribution).</summary>
    Tune Generic { get; }

    /// <summary>The tune of <paramref name="country"/> (English name), or <see cref="Generic"/> when it has none.</summary>
    Tune For(string? country);
}
