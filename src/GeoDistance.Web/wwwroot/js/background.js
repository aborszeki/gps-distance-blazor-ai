// Real-world continents (Natural Earth 110m land, Mercator) drawn in parchment colours behind the page.

const LAND_URL = "https://cdn.jsdelivr.net/npm/world-atlas@2/land-110m.json";
const TOPOJSON_URL = "https://cdn.jsdelivr.net/npm/topojson-client@3/+esm";

const WIDTH = 1200;
const LAT_TOP = 80;
const LAT_BOTTOM = -58;

const mercator = lat => Math.log(Math.tan(Math.PI / 4 + (lat * Math.PI) / 360));

/** @param {HTMLElement} container receives an <svg> with the land outline */
export async function drawWorld(container) {
  try {
    const [topojson, topology] = await Promise.all([
      import(TOPOJSON_URL),
      fetch(LAND_URL).then(response => response.json()),
    ]);

    const land = topojson.feature(topology, topology.objects.land); // Feature or FeatureCollection
    const geometries = land.features ? land.features.map(f => f.geometry) : [land.geometry];
    const polygons = geometries.flatMap(g => (g.type === "Polygon" ? [g.coordinates] : g.coordinates));

    const span = mercator(LAT_TOP) - mercator(LAT_BOTTOM);
    const height = (WIDTH * span) / (2 * Math.PI);
    const project = ([lon, lat]) => {
      const clamped = Math.min(LAT_TOP, Math.max(LAT_BOTTOM, lat));
      const y = ((mercator(LAT_TOP) - mercator(clamped)) / span) * height;
      return `${(((lon + 180) / 360) * WIDTH).toFixed(1)},${y.toFixed(1)}`;
    };

    let path = "";
    for (const polygon of polygons) {
      for (const ring of polygon) {
        if (Math.max(...ring.map(p => p[1])) < -56) continue; // skip Antarctica
        path += `M${ring.map(project).join("L")}Z`;
      }
    }

    container.innerHTML = `
      <svg viewBox="0 0 ${WIDTH} ${height.toFixed(0)}" preserveAspectRatio="xMidYMid slice">
        <path d="${path}" fill="none" stroke="#c4aa72" stroke-opacity=".4" stroke-width="7" stroke-linejoin="round"/>
        <path d="${path}" fill="#e0cfa0" fill-opacity=".92" fill-rule="evenodd" stroke="#a9884f" stroke-opacity=".75" stroke-width="1.4" stroke-linejoin="round"/>
      </svg>`;
  } catch {
    /* offline: the paper background alone is fine */
  }
}
