const { test, expect } = require("@playwright/test");
const { login, uid } = require("./helpers");

test.describe("Admin — auth", () => {
  test("unauthenticated admin redirects to login", async ({ page }) => {
    await page.goto("/Admin/Dashboard");
    await expect(page).toHaveURL(/\/Identity\/Account\/Login/);
  });

  test("login reaches dashboard with stats", async ({ page }) => {
    await login(page);
    await page.goto("/Admin/Dashboard");
    await expect(page.locator(".page-head")).toContainText(/Welcome back/i);
    await expect(page.locator(".stat").first()).toBeVisible();
    // Stat cards for the core entities
    await expect(page.locator(".stat-grid")).toContainText("Projects");
    await expect(page.locator(".stat-grid")).toContainText("Services");
  });
});

test.describe("Admin — FAQ CRUD lifecycle", () => {
  test("create → edit → delete a FAQ", async ({ page }) => {
    await login(page);
    const q = "E2E question " + uid();
    const qEdited = q + " (edited)";

    // Create (scope to the form's Save button — the topbar has a logout submit too)
    await page.goto("/Admin/Faqs/Create");
    await page.fill("#Question", q);
    await page.fill("#Answer", "Automated answer.");
    await page.locator('form button[type="submit"].btn-gold').click();
    await expect(page).toHaveURL(/\/Admin\/Faqs$/);
    await expect(page.locator("table.table-cms")).toContainText(q);

    // Edit — open the row's edit link
    await page.getByRole("link", { name: q }).click();
    await expect(page.locator("#Question")).toHaveValue(q);
    await page.fill("#Question", qEdited);
    await page.locator('form button[type="submit"].btn-gold').click();
    await expect(page.locator("table.table-cms")).toContainText(qEdited);

    // Delete via SweetAlert confirm
    const row = page.locator("tr", { hasText: qEdited });
    await row.locator("[data-delete-url]").click();
    await page.locator(".swal2-confirm").click();
    await expect(page.locator("table.table-cms")).not.toContainText(qEdited);
  });
});

test.describe("Admin — AJAX active toggle", () => {
  test("toggling a service persists", async ({ page }) => {
    await login(page);
    await page.goto("/Admin/Services");
    const toggle = page.locator('input[data-toggle-url]').first();
    const was = await toggle.isChecked();
    // The checkbox is visually hidden; the user clicks the slider label.
    await page.locator('label.switch:has(input[data-toggle-url])').first().click();
    await expect(page.locator(".swal2-toast")).toBeVisible({ timeout: 8000 });
    await page.reload();
    const now = await page.locator('input[data-toggle-url]').first().isChecked();
    expect(now).toBe(!was);
    // restore
    await page.locator('label.switch:has(input[data-toggle-url])').first().click();
    await expect(page.locator(".swal2-toast")).toBeVisible({ timeout: 8000 });
  });
});

test.describe("Admin — media upload", () => {
  test("upload an image generates a library entry, then delete", async ({ page }) => {
    await login(page);
    await page.goto("/Admin/MediaLibrary");
    const before = await page.locator(".media-grid .card-cms").count();
    // Use an existing asset as the upload source
    const filePath = require("path").resolve(
      __dirname, "../../../src/BardeesCms.Web/wwwroot/assets/img/beauty-01-sq.jpg");
    await page.setInputFiles("#mediaFiles", filePath);
    await page.click('button:has-text("Upload")');
    await expect(page.locator(".media-grid .card-cms")).toHaveCount(before + 1);
    // Delete the newest (first) item
    await page.locator(".media-grid .card-cms [data-delete-url]").first().click();
    await page.locator(".swal2-confirm").click();
    await expect(page.locator(".media-grid .card-cms")).toHaveCount(before);
  });
});

test.describe("Admin — messages inbox", () => {
  test("a submitted contact message appears in the inbox", async ({ page, context }) => {
    // Submit a message as an anonymous visitor first
    const visitor = await context.newPage();
    const subj = "E2E inbox " + uid();
    await visitor.goto("/contact");
    await visitor.fill("#Form_Name", "Inbox Tester");
    await visitor.fill("#Form_Email", "inbox@example.com");
    await visitor.fill("#Form_Message", subj);
    await visitor.click('button[type="submit"]');
    await expect(visitor.locator(".form-success")).toBeVisible();
    await visitor.close();

    await login(page);
    await page.goto("/Admin/ContactMessages");
    await expect(page.locator("body")).toContainText("Inbox Tester");
  });
});

test.describe("Admin — system pages", () => {
  test("users and activity log render", async ({ page }) => {
    await login(page);
    await page.goto("/Admin/Users");
    await expect(page.locator("body")).toContainText(/admin@bardeesrefaat\.com/i);
    await page.goto("/Admin/ActivityLog");
    await expect(page.locator("table.table-cms, .empty-state").first()).toBeVisible();
  });

  test("logout returns to public site", async ({ page }) => {
    await login(page);
    await page.goto("/Admin/Dashboard");
    await page.locator('form[action*="Logout"] button[type="submit"]').click();
    await expect(page).toHaveURL(/localhost:5199\/?$/);
  });
});
