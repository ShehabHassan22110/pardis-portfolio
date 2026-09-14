// @ts-check
const { defineConfig, devices } = require("@playwright/test");

const PORT = 5199;
const BASE = `http://localhost:${PORT}`;

/**
 * E2E config for the BARDEES CMS. Launches the ASP.NET Core app itself (dotnet run)
 * against LocalDB, then exercises the public site and the admin CMS.
 */
module.exports = defineConfig({
  testDir: "./tests",
  fullyParallel: false,          // single app instance + shared DB → run serially
  workers: 1,
  retries: 0,
  timeout: 30_000,
  expect: { timeout: 10_000 },
  reporter: [["list"]],
  use: {
    baseURL: BASE,
    trace: "retain-on-failure",
    screenshot: "only-on-failure",
    ignoreHTTPSErrors: true,
  },
  projects: [
    { name: "chromium", use: { ...devices["Desktop Chrome"] }, testIgnore: /responsive\.spec\.js/ },
    { name: "mobile", use: { ...devices["Pixel 7"] }, testMatch: /responsive\.spec\.js/ },
  ],
  webServer: {
    command: "dotnet run --no-launch-profile -c Debug",
    cwd: "../../src/BardeesCms.Web",
    url: BASE,
    timeout: 150_000,
    reuseExistingServer: true,
    stdout: "pipe",
    stderr: "pipe",
    env: {
      ASPNETCORE_ENVIRONMENT: "Development",
      ASPNETCORE_URLS: BASE,
      AdminSeed__Email: "admin@bardeesrefaat.com",
      AdminSeed__Password: "Bardees!2026Dev",
      AdminSeed__FullName: "Bardees Admin",
    },
  },
});
