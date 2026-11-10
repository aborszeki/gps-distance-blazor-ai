using GeoDistance.Web.Api;
using GeoDistance.Web.Components;
using GeoDistance.Web.DependencyInjection;
using GeoDistance.Web.Interop;
using GeoDistance.Web.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddProblemDetails();

// Domain services (distance calculation, places, tunes), shared by the page and the API.
builder.Services.AddGeoDistanceCore();

// Per-circuit (per browser tab) services.
builder.Services.AddScoped<BrowserStorage>();
builder.Services.AddScoped<Localizer>();
builder.Services.AddScoped<AudioPlayer>();
builder.Services.AddScoped<MapInterop>();
builder.Services.AddScoped<BackgroundInterop>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapDistanceApi();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
