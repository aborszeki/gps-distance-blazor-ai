# GeoDistance

Distance between two GPS coordinates on a chosen Earth model, as a Blazor Web App (.NET 10, interactive server).

```
GeoDistance.slnx
src/
  GeoDistance.Domain/ the domain layer, no dependencies: the service contracts (IDistanceCalculator,
                      IRouteInterpolator, ITravelEstimator, IPlaceCatalog, ITuneCatalog) and the types
                      they use (GeoPoint, Ellipsoid, EarthModels, DistanceResult, City/Country/Continent,
                      Tune, TravelMode, ...)
  GeoDistance.Core/   implementations of those contracts: Vincenty calculator, great-circle interpolator,
                      travel estimator, built-in place and tune catalogs, coordinate parser
  GeoDistance.Web/    Blazor Web App; registers the implementations in DependencyInjection/
    Api/              GET /distance minimal API (same calculation as the page)
    Components/       App, Routes, layout, pages and shared components (*.razor + scoped *.razor.css)
    Interop/          typed wrappers around the ES modules in wwwroot/js (storage, audio, map, background)
    Localization/     Hungarian / English / German texts and place names
    Models/           small view models
    wwwroot/          app.css (theme), SVG backgrounds, cursor and js modules
legacy/               the previous single-page (static HTML + minimal API) version, kept for reference
```

## Run

```bash
dotnet run --project src/GeoDistance.Web
```

Then open http://localhost:5056 (set in `Properties/launchSettings.json`). Requires the .NET 10 SDK.

The page loads Leaflet, the map tiles and the continent outlines from public CDNs, so the map popup and the
world background need an internet connection.

## API

`GET /distance?lat1=47.4979&lon1=19.0402&lat2=48.2082&lon2=16.3738&model=wgs84`

`model` is one of `wgs84` (default), `grs80`, `intl1924`, `clarke1866`, `bessel1841`.
