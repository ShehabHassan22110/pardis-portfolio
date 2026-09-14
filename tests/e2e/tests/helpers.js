// Shared helpers for the E2E suite.
const ADMIN_EMAIL = "admin@bardeesrefaat.com";
const ADMIN_PASSWORD = "Bardees!2026Dev";

/** Log into the admin via the Identity UI and land back on the site. */
async function login(page) {
  await page.goto("/Identity/Account/Login");
  await page.fill("#Input_Email", ADMIN_EMAIL);
  await page.fill("#Input_Password", ADMIN_PASSWORD);
  await Promise.all([
    page.waitForURL((u) => !u.pathname.startsWith("/Identity"), { timeout: 15000 }),
    page.click('button[type="submit"]'),
  ]);
}

/** A short unique token for creating throwaway records. */
function uid() {
  return "e2e-" + Math.random().toString(36).slice(2, 8);
}

module.exports = { login, uid, ADMIN_EMAIL, ADMIN_PASSWORD };
