const { test, expect } = require("@playwright/test");

// Runs under the "mobile" project (Pixel 7 viewport).
const pages = ["/", "/work", "/services", "/videos", "/contact", "/about"];

for (const path of pages) {
  test(`no horizontal overflow on ${path} (mobile)`, async ({ page }) => {
    await page.goto(path);
    await page.waitForTimeout(600);
    const { scrollW, clientW } = await page.evaluate(() => ({
      scrollW: document.documentElement.scrollWidth,
      clientW: document.documentElement.clientWidth,
    }));
    // Allow 1px rounding slack.
    expect(scrollW).toBeLessThanOrEqual(clientW + 1);
  });
}

test("mobile nav collapses to a burger", async ({ page }) => {
  await page.goto("/");
  await expect(page.locator(".navbar-toggler")).toBeVisible();
});
