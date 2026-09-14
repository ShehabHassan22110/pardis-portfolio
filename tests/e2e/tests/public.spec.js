const { test, expect } = require("@playwright/test");

test.describe("Public — homepage", () => {
  test("loads with all key sections", async ({ page }) => {
    await page.goto("/");
    await expect(page).toHaveTitle(/BARDEES/i);
    await expect(page.locator(".hero__mast")).toBeVisible();
    // Editorial sections present
    for (const sel of ["#about", "#clients", "#why", "#work", "#services", "#faq", ".cta-band", "footer.footer"]) {
      await expect(page.locator(sel).first()).toBeVisible();
    }
    // Seeded content actually rendered
    await expect(page.locator("body")).toContainText("Brand Ambassador");
  });

  test("hero carousel advances", async ({ page }) => {
    await page.goto("/");
    const idx = page.locator("#heroIndex");
    await expect(idx).toHaveText("01");
    await page.locator("#heroNext").click();
    await expect(idx).not.toHaveText("01");
  });

  test("theme toggle flips data-theme", async ({ page }) => {
    await page.goto("/");
    const html = page.locator("html");
    const before = await html.getAttribute("data-theme");
    await page.locator("[data-theme-toggle]").first().click();
    await expect(html).not.toHaveAttribute("data-theme", before || "dark");
  });

  test("FAQ accordion opens", async ({ page }) => {
    await page.goto("/");
    const first = page.locator(".faq-item").first();
    await first.locator(".faq-q").click();
    await expect(first).toHaveClass(/open/);
  });
});

test.describe("Public — language (EN/AR)", () => {
  test("switches to Arabic RTL and back", async ({ page }) => {
    await page.goto("/");
    await expect(page.locator("html")).toHaveAttribute("dir", "ltr");
    await page.locator('a[href^="/set-language/ar"]').first().click();
    await expect(page.locator("html")).toHaveAttribute("dir", "rtl");
    await expect(page.locator("html")).toHaveAttribute("lang", "ar");
    // Arabic content shows
    await expect(page.locator("body")).toContainText("برديس");
    await page.locator('a[href^="/set-language/en"]').first().click();
    await expect(page.locator("html")).toHaveAttribute("dir", "ltr");
  });
});

test.describe("Public — navigation & work", () => {
  test("nav to Work, filter by discipline, open a project", async ({ page }) => {
    await page.goto("/work");
    await expect(page.locator(".work-filter")).toBeVisible();
    await expect(page.locator(".mtile").first()).toBeVisible();

    // Filter by a discipline
    await page.locator('.work-filter a[href*="discipline="]').first().click();
    await expect(page).toHaveURL(/discipline=/);

    // Open a project detail
    await page.goto("/work");
    const firstCard = page.locator("a.mtile").first();
    const href = await firstCard.getAttribute("href");
    await firstCard.click();
    await expect(page).toHaveURL(new RegExp(href.replace(/[/]/g, "\\/")));
    await expect(page.locator("h2.tc-h").first()).toBeVisible();
  });

  test("services index and detail", async ({ page }) => {
    await page.goto("/services");
    await expect(page.locator(".svc").first()).toBeVisible();
    await page.goto("/services/brand-ambassador");
    await expect(page.locator("h2.tc-h")).toContainText(/Brand Ambassador/i);
  });

  test("videos page renders", async ({ page }) => {
    await page.goto("/videos");
    await expect(page.locator("#videosGrid")).toBeVisible();
  });
});

test.describe("Public — contact form", () => {
  test("valid submission shows success", async ({ page }) => {
    await page.goto("/contact");
    await page.fill("#Form_Name", "E2E Tester");
    await page.fill("#Form_Email", "e2e@example.com");
    await page.fill("#Form_Message", "This is an automated e2e test message.");
    await page.click('button[type="submit"]');
    await expect(page.locator(".form-success")).toBeVisible();
  });

  test("invalid submission is blocked with validation", async ({ page }) => {
    await page.goto("/contact");
    await page.click('button[type="submit"]');
    // Either client- or server-side validation must prevent success
    await expect(page.locator(".form-success")).toHaveCount(0);
    await expect(page.locator(".field-validation-error, .input-validation-error").first()).toBeVisible();
  });
});

test.describe("Public — infra", () => {
  test("404 page", async ({ page }) => {
    const res = await page.goto("/this-does-not-exist");
    expect(res.status()).toBe(404);
    await expect(page.locator("body")).toContainText(/not found|404/i);
  });

  test("robots.txt and sitemap.xml", async ({ request }) => {
    const robots = await request.get("/robots.txt");
    expect(robots.ok()).toBeTruthy();
    expect(await robots.text()).toContain("Sitemap:");
    const sitemap = await request.get("/sitemap.xml");
    expect(sitemap.ok()).toBeTruthy();
    expect(await sitemap.text()).toContain("<urlset");
  });

  test("SEO: canonical + JSON-LD on home", async ({ page }) => {
    await page.goto("/");
    await expect(page.locator('link[rel="canonical"]')).toHaveCount(1);
    const ld = await page.locator('script[type="application/ld+json"]').count();
    expect(ld).toBeGreaterThanOrEqual(2);
  });
});
