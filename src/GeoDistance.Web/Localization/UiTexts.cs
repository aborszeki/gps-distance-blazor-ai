using System.Collections.Frozen;

namespace GeoDistance.Web.Localization;

/// <summary>Interface texts (English, Hungarian, German) and translated place names.</summary>
public static class UiTexts
{
    public const string DefaultLanguage = "en";

    /// <summary>Supported languages in flag order.</summary>
    public static IReadOnlyList<LanguageInfo> Languages { get; } =
    [
        new("hu", "Magyar"),
        new("en", "English"),
        new("de", "Deutsch"),
    ];

    private static readonly FrozenDictionary<string, FrozenDictionary<string, string>> Texts =
        new Dictionary<string, FrozenDictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                ["title"] = "GPS Distance",
                ["from"] = "From (lat, lon)",
                ["to"] = "To (lat, lon)",
                ["phFrom"] = "Pick a city or type 47.4979, 19.0402",
                ["phTo"] = "Pick a city or type 48.2082, 16.3738",
                ["reset"] = "Reset",
                ["calc"] = "Calculate distance",
                ["km"] = "km",
                ["mi"] = "mi",
                ["close"] = "Close",
                ["model"] = "Earth model (ellipsoid)",
                ["walk"] = "On foot",
                ["cycle"] = "By bike",
                ["car"] = "By car",
                ["plane"] = "By plane",
                ["d"] = "d",
                ["h"] = "h",
                ["min"] = "min",
                ["showList"] = "Show list",
                ["needBoth"] = "Enter both coordinates as \"lat, lon\" (or pick a city).",
                ["invalid"] = "Invalid coordinates: latitude must be within ±90, longitude within ±180.",
            }.ToFrozenDictionary(),
            ["hu"] = new Dictionary<string, string>
            {
                ["title"] = "GPS-távolság",
                ["from"] = "Honnan (szélesség, hosszúság)",
                ["to"] = "Hová (szélesség, hosszúság)",
                ["phFrom"] = "Válasszon várost, vagy írjon: 47.4979, 19.0402",
                ["phTo"] = "Válasszon várost, vagy írjon: 48.2082, 16.3738",
                ["reset"] = "Törlés",
                ["calc"] = "Távolság kiszámítása",
                ["km"] = "km",
                ["mi"] = "mérföld",
                ["close"] = "Bezárás",
                ["model"] = "Földmodell (ellipszoid)",
                ["walk"] = "Gyalog",
                ["cycle"] = "Biciklivel",
                ["car"] = "Autóval",
                ["plane"] = "Repülővel",
                ["d"] = "nap",
                ["h"] = "óra",
                ["min"] = "perc",
                ["showList"] = "Lista megjelenítése",
                ["needBoth"] = "Adja meg mindkét koordinátát „szélesség, hosszúság” alakban (vagy válasszon várost).",
                ["invalid"] = "Érvénytelen koordináták: a szélesség ±90, a hosszúság ±180 között lehet.",
            }.ToFrozenDictionary(),
            ["de"] = new Dictionary<string, string>
            {
                ["title"] = "GPS-Entfernung",
                ["from"] = "Von (Breite, Länge)",
                ["to"] = "Nach (Breite, Länge)",
                ["phFrom"] = "Stadt wählen oder eingeben: 47.4979, 19.0402",
                ["phTo"] = "Stadt wählen oder eingeben: 48.2082, 16.3738",
                ["reset"] = "Zurücksetzen",
                ["calc"] = "Entfernung berechnen",
                ["km"] = "km",
                ["mi"] = "Meilen",
                ["close"] = "Schließen",
                ["model"] = "Erdmodell (Ellipsoid)",
                ["walk"] = "Zu Fuß",
                ["cycle"] = "Mit dem Fahrrad",
                ["car"] = "Mit dem Auto",
                ["plane"] = "Mit dem Flugzeug",
                ["d"] = "Tg.",
                ["h"] = "Std.",
                ["min"] = "Min.",
                ["showList"] = "Liste anzeigen",
                ["needBoth"] = "Beide Koordinaten als „Breite, Länge“ eingeben (oder eine Stadt wählen).",
                ["invalid"] = "Ungültige Koordinaten: Breite muss zwischen ±90, Länge zwischen ±180 liegen.",
            }.ToFrozenDictionary(),
        }.ToFrozenDictionary();

    /// <summary>Hungarian and German names; places missing here keep their English name.</summary>
    private static readonly FrozenDictionary<string, FrozenDictionary<string, string>> PlaceNames =
        new Dictionary<string, FrozenDictionary<string, string>>
        {
            ["hu"] = new Dictionary<string, string>
            {
                ["Europe"] = "Európa", ["Asia"] = "Ázsia", ["North America"] = "Észak-Amerika",
                ["Hungary"] = "Magyarország", ["France"] = "Franciaország", ["Italy"] = "Olaszország",
                ["Japan"] = "Japán", ["India"] = "India", ["China"] = "Kína",
                ["United States"] = "Egyesült Államok", ["Canada"] = "Kanada", ["Mexico"] = "Mexikó",
                ["Rome"] = "Róma", ["Milan"] = "Milánó", ["Naples"] = "Nápoly", ["Marseille"] = "Marseille",
                ["Tokyo"] = "Tokió", ["New Delhi"] = "Új-Delhi", ["Beijing"] = "Peking", ["Shanghai"] = "Sanghaj",
                ["Guangzhou"] = "Kanton", ["Washington, D.C."] = "Washington, D.C.", ["New York"] = "New York",
                ["Mexico City"] = "Mexikóváros", ["Bengaluru"] = "Bengaluru",
            }.ToFrozenDictionary(),
            ["de"] = new Dictionary<string, string>
            {
                ["Europe"] = "Europa", ["Asia"] = "Asien", ["North America"] = "Nordamerika",
                ["Hungary"] = "Ungarn", ["France"] = "Frankreich", ["Italy"] = "Italien",
                ["Japan"] = "Japan", ["India"] = "Indien", ["China"] = "China",
                ["United States"] = "Vereinigte Staaten", ["Canada"] = "Kanada", ["Mexico"] = "Mexiko",
                ["Rome"] = "Rom", ["Milan"] = "Mailand", ["Naples"] = "Neapel",
                ["Tokyo"] = "Tokio", ["New Delhi"] = "Neu-Delhi", ["Beijing"] = "Peking", ["Shanghai"] = "Schanghai",
                ["Guangzhou"] = "Kanton", ["Mexico City"] = "Mexiko-Stadt",
            }.ToFrozenDictionary(),
        }.ToFrozenDictionary();

    public static bool IsSupported(string? code) => code is not null && Texts.ContainsKey(code);

    public static string Text(string language, string key) =>
        Texts.TryGetValue(language, out var table) && table.TryGetValue(key, out var text) ? text : key;

    public static string PlaceName(string language, string englishName) =>
        PlaceNames.TryGetValue(language, out var table) && table.TryGetValue(englishName, out var name) ? name : englishName;
}
