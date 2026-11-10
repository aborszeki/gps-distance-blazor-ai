// Leaflet route map shown in the dialog. Leaflet itself is loaded as a global (`L`) by App.razor.

let map = null;
let escapeHandler = null;

/**
 * @param {HTMLElement} element the container (must be visible and sized)
 * @param {number[][]} route [lat, lon] pairs along the great circle
 * @param {{latitude: number, longitude: number, label: string}[]} markers
 * @param {{invokeMethodAsync: (name: string) => Promise<void>}} callbacks .NET object with an invokable `Close`
 */
export function show(element, route, markers, callbacks) {
  destroy();
  if (typeof L === "undefined") return; // Leaflet could not be loaded (offline)

  map = L.map(element, { worldCopyJump: true });
  L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
    maxZoom: 18,
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
  }).addTo(map);

  const line = L.polyline(route, {
    color: "#6b2d12", weight: 4, opacity: 0.95, dashArray: "1 9", lineCap: "round",
  }).addTo(map);

  for (const marker of markers) {
    L.circleMarker([marker.latitude, marker.longitude], {
      radius: 8, color: "#3b2a1a", weight: 2, fillColor: "#c08a2e", fillOpacity: 1,
    })
      .bindTooltip(marker.label, { permanent: true, direction: "top", offset: [0, -8], className: "map-label" })
      .addTo(map);
  }

  map.fitBounds(line.getBounds(), { padding: [60, 60], maxZoom: 10 });

  escapeHandler = event => {
    if (event.key === "Escape") callbacks.invokeMethodAsync("Close");
  };
  document.addEventListener("keydown", escapeHandler);
}

export function destroy() {
  if (escapeHandler) {
    document.removeEventListener("keydown", escapeHandler);
    escapeHandler = null;
  }
  if (map) {
    map.remove();
    map = null;
  }
}
