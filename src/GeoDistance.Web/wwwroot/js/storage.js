// localStorage and document-level helpers. Every storage call is guarded: it can throw when site data is blocked.

export function getItem(key) {
  try {
    return localStorage.getItem(key);
  } catch {
    return null;
  }
}

export function setItem(key, value) {
  try {
    localStorage.setItem(key, value);
  } catch {
    /* private mode or blocked storage: the choice just is not remembered */
  }
}

export function getBrowserLanguage() {
  return (navigator.language || "en").slice(0, 2);
}

export function setDocumentLanguage(code) {
  document.documentElement.lang = code;
}
