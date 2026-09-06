/* =============================================================================
   dashboard.js — studio dashboard logic (frontend only, mock data)
   ============================================================================= */
(function () {
  "use strict";
  const $ = (s, c = document) => c.querySelector(s);
  const $$ = (s, c = document) => Array.from(c.querySelectorAll(s));
  const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

  /* ---------- Theme toggle (shared behaviour with the site) ----------------- */
  const THEME_KEY = "noor-theme";
  function setTheme(t) {
    const html = document.documentElement;
    html.classList.add("theming");
    html.setAttribute("data-theme", t);
    try { localStorage.setItem(THEME_KEY, t); } catch (e) {}
    setTimeout(() => html.classList.remove("theming"), 650);
  }
  const themeBtn = document.createElement("button");
  themeBtn.className = "theme-toggle";
  themeBtn.setAttribute("aria-label", "Toggle theme");
  themeBtn.innerHTML = `
    <svg class="i-sun" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.4" stroke-linecap="round"><circle cx="12" cy="12" r="4.2"/><path d="M12 2v2.6M12 19.4V22M2 12h2.6M19.4 12H22M4.9 4.9l1.8 1.8M17.3 17.3l1.8 1.8M19.1 4.9l-1.8 1.8M6.7 17.3l-1.8 1.8"/></svg>
    <svg class="i-moon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.4" stroke-linecap="round" stroke-linejoin="round"><path d="M20 14.5A8 8 0 0 1 9.5 4a8 8 0 1 0 10.5 10.5z"/></svg>`;
  themeBtn.addEventListener("click", () =>
    setTheme((document.documentElement.getAttribute("data-theme") || "dark") === "dark" ? "light" : "dark")
  );
  const slot = $("#themeSlot");
  if (slot) slot.appendChild(themeBtn);

  /* ---------- Login gate ----------------------------------------------------- */
  const gate = $("#gate");
  const dash = $("#dash");
  const SESSION_KEY = "noor-signed-in";
  function enterStudio() {
    gate.classList.add("is-out");
    dash.hidden = false;
    try { sessionStorage.setItem(SESSION_KEY, "1"); } catch (e) {}
  }
  $("#gateForm").addEventListener("submit", (e) => { e.preventDefault(); enterStudio(); });
  try { if (sessionStorage.getItem(SESSION_KEY)) { gate.classList.add("is-out"); dash.hidden = false; } } catch (e) {}
  $("#signOut").addEventListener("click", () => {
    try { sessionStorage.removeItem(SESSION_KEY); } catch (e) {}
    dash.hidden = true; gate.classList.remove("is-out");
  });

  /* ---------- Today's date --------------------------------------------------- */
  const now = new Date();
  $("#todayDate").textContent = now.toLocaleDateString("en-GB", { day: "numeric", month: "short", year: "numeric" });

  /* ---------- Sidebar counts ------------------------------------------------- */
  const pending = BOOKINGS.filter((b) => b.status === "new" || b.status === "in-review").length;
  $("#cBookings").textContent = BOOKINGS.length;
  $("#cMsgs").textContent = MESSAGES.filter((m) => m.unread).length;

  /* ---------- View switching ------------------------------------------------- */
  const TITLES = {
    overview: ["Overview", "Welcome back, Noor — here's your studio at a glance."],
    bookings: ["Bookings", "Every collaboration request in one place."],
    portfolio: ["Portfolio", "Manage the photos and films behind each discipline."],
    categories: ["Categories", "Show, hide and reorder the seven disciplines."],
    messages: ["Messages", "Conversations with brands and companies."],
    settings: ["Settings", "Your public profile and studio preferences."],
  };
  function showView(name) {
    $$(".view").forEach((v) => v.classList.toggle("is-active", v.id === "view-" + name));
    $$(".nav-item[data-view]").forEach((n) => n.classList.toggle("is-active", n.dataset.view === name));
    const t = TITLES[name] || TITLES.overview;
    $("#viewTitle").textContent = t[0];
    $("#viewSub").textContent = t[1];
    document.body.classList.remove("side-open");
    if (name === "overview") { animateChart(); animateDisciplineBars(); }
    location.hash = name;
  }
  $$(".nav-item[data-view]").forEach((n) => n.addEventListener("click", () => showView(n.dataset.view)));
  $$("[data-jump]").forEach((a) => a.addEventListener("click", (e) => { e.preventDefault(); showView(a.dataset.jump); }));

  /* ---------- Mobile sidebar ------------------------------------------------- */
  $("#sideToggle").addEventListener("click", () => document.body.classList.toggle("side-open"));

  /* ---------- KPI cards ------------------------------------------------------ */
  const confirmedThisMonth = BOOKINGS.filter((b) => b.status === "confirmed").length;
  const brands = new Set(BOOKINGS.map((b) => b.brand)).size;
  const KPIS = [
    { k: "Total requests", v: BOOKINGS.length, d: "▲ 20% vs last month", up: true },
    { k: "Pending", v: pending, d: "Needs your reply", up: null },
    { k: "Confirmed", v: confirmedThisMonth, d: "▲ 2 this month", up: true },
    { k: "Brands", v: brands, d: "Unique companies", up: null },
  ];
  const spark = `<svg class="kpi__spark" viewBox="0 0 120 40" preserveAspectRatio="none"><path d="M0 32 L20 26 L40 30 L60 18 L80 22 L100 8 L120 12" fill="none" stroke="var(--champagne)" stroke-width="2"/></svg>`;
  $("#kpis").innerHTML = KPIS.map((k) => `
    <div class="kpi">
      <div class="kpi__k">${k.k}</div>
      <div class="kpi__v" data-count="${k.v}">0</div>
      <div class="kpi__d">${k.up === true ? '<span class="up">' + k.d + "</span>" : k.d}</div>
      ${spark}
    </div>`).join("");

  // count-up KPIs
  function countUp(el, target) {
    if (reduce) { el.textContent = target; return; }
    const dur = 1100, t0 = performance.now();
    const tick = (n) => { const p = Math.min((n - t0) / dur, 1); el.textContent = Math.round(target * (1 - Math.pow(1 - p, 3))); if (p < 1) requestAnimationFrame(tick); };
    requestAnimationFrame(tick);
  }
  $$(".kpi__v").forEach((el) => countUp(el, parseInt(el.dataset.count, 10)));

  /* ---------- Bookings chart ------------------------------------------------- */
  const maxV = Math.max(...MONTHLY.map((m) => m.v));
  $("#chart").innerHTML = MONTHLY.map((m) => `
    <div class="bar">
      <span class="bar__v">${m.v}</span>
      <div class="bar__fill" data-h="${Math.round((m.v / maxV) * 100)}"></div>
      <span class="bar__l">${m.m}</span>
    </div>`).join("");
  function animateChart() {
    $$("#chart .bar__fill").forEach((f, i) => {
      const h = f.dataset.h + "%";
      if (reduce) { f.style.height = h; return; }
      f.style.height = "0"; setTimeout(() => (f.style.height = h), 60 + i * 90);
    });
  }

  /* ---------- By discipline -------------------------------------------------- */
  const byDiscipline = {};
  BOOKINGS.forEach((b) => (byDiscipline[b.type] = (byDiscipline[b.type] || 0) + 1));
  const discEntries = Object.entries(byDiscipline).sort((a, b) => b[1] - a[1]);
  const maxD = Math.max(...discEntries.map((d) => d[1]));
  $("#disciplineBars").innerHTML = discEntries.map(([name, c]) => `
    <div class="dbar">
      <div class="dbar__top"><span class="n">${name}</span><span class="c">${c}</span></div>
      <div class="dbar__track"><div class="dbar__fill" data-w="${Math.round((c / maxD) * 100)}"></div></div>
    </div>`).join("");
  function animateDisciplineBars() {
    $$("#disciplineBars .dbar__fill").forEach((f, i) => {
      const w = f.dataset.w + "%";
      if (reduce) { f.style.width = w; return; }
      f.style.width = "0"; setTimeout(() => (f.style.width = w), 60 + i * 80);
    });
  }

  /* ---------- Booking rows --------------------------------------------------- */
  const chip = (s) => `<span class="chip chip--${s}">${STATUS_LABELS[s] || s}</span>`;
  const rowHTML = (b, full) => `
    <tr data-id="${b.id}">
      <td class="id">${b.id}</td>
      <td class="brand">${b.brand}</td>
      <td>${b.type}</td>
      <td>${new Date(b.date).toLocaleDateString("en-GB", { day: "numeric", month: "short" })}</td>
      ${full ? `<td>${b.budget}</td>` : ""}
      <td>${chip(b.status)}</td>
    </tr>`;
  $("#recentRows").innerHTML = BOOKINGS.slice(0, 5).map((b) => rowHTML(b, false)).join("");

  let activeFilter = "all";
  function renderBookings() {
    const q = ($("#globalSearch").value || "").toLowerCase();
    const rows = BOOKINGS.filter((b) => {
      const okStatus = activeFilter === "all" || b.status === activeFilter;
      const okQ = !q || (b.brand + b.type + b.id + b.contact).toLowerCase().includes(q);
      return okStatus && okQ;
    });
    $("#bookingRows").innerHTML = rows.length
      ? rows.map((b) => rowHTML(b, true)).join("")
      : `<tr><td colspan="6" style="text-align:center;padding:2.5rem;color:var(--mist-2)">No requests match this filter yet.</td></tr>`;
  }
  const FILTERS = [["all", "All"], ["new", "New"], ["in-review", "In review"], ["confirmed", "Confirmed"], ["declined", "Declined"]];
  $("#bookingTabs").innerHTML = FILTERS.map(([v, l], i) =>
    `<button class="tab ${i === 0 ? "is-active" : ""}" data-filter="${v}">${l}${v !== "all" ? ` · ${BOOKINGS.filter((b) => b.status === v).length}` : ""}</button>`
  ).join("");
  $$("#bookingTabs .tab").forEach((t) => t.addEventListener("click", () => {
    $$("#bookingTabs .tab").forEach((x) => x.classList.remove("is-active"));
    t.classList.add("is-active"); activeFilter = t.dataset.filter; renderBookings();
  }));
  $("#globalSearch").addEventListener("input", renderBookings);
  renderBookings();

  /* ---------- Drawer (booking detail) --------------------------------------- */
  const drawer = $("#drawer"), scrim = $("#drawerScrim");
  function openDrawer(id) {
    const b = BOOKINGS.find((x) => x.id === id);
    if (!b) return;
    drawer.innerHTML = `
      <button class="drawer__close" aria-label="Close">✕</button>
      <p class="eyebrow" style="color:var(--champagne)">${b.id}</p>
      <h2>${b.brand}</h2>
      <p class="sub">${b.contact} · <a href="mailto:${b.email}" style="color:var(--mist)">${b.email}</a></p>
      <dl>
        <div><dt>Discipline</dt><dd>${b.type}</dd></div>
        <div><dt>Preferred date</dt><dd>${new Date(b.date).toLocaleDateString("en-GB", { day: "numeric", month: "long", year: "numeric" })}</dd></div>
        <div><dt>Budget</dt><dd>${b.budget}</dd></div>
        <div><dt>Status</dt><dd>${chip(b.status)}</dd></div>
      </dl>
      <div class="quote">${b.message}</div>
      <div class="drawer__actions">
        <button class="btn btn--gold">Confirm</button>
        <button class="btn">Reply</button>
        <button class="btn" style="border-color:var(--blush);color:var(--blush)">Decline</button>
      </div>`;
    $(".drawer__close", drawer).addEventListener("click", closeDrawer);
    drawer.classList.add("is-on"); scrim.classList.add("is-on"); drawer.setAttribute("aria-hidden", "false");
  }
  function closeDrawer() { drawer.classList.remove("is-on"); scrim.classList.remove("is-on"); drawer.setAttribute("aria-hidden", "true"); }
  scrim.addEventListener("click", closeDrawer);
  document.addEventListener("keydown", (e) => { if (e.key === "Escape") closeDrawer(); });
  document.addEventListener("click", (e) => {
    const tr = e.target.closest("tr[data-id]");
    if (tr) openDrawer(tr.dataset.id);
  });

  /* ---------- Portfolio media ------------------------------------------------ */
  let activeCat = CATEGORIES[0].slug;
  $("#portfolioCats").innerHTML = CATEGORIES.map((c, i) =>
    `<button class="tab ${i === 0 ? "is-active" : ""}" data-cat="${c.slug}">${c.title}</button>`
  ).join("");
  function renderMedia() {
    const cat = getCategory(activeCat);
    const items = cat.works.map((w) => `
      <div class="media">
        <span class="media__tag">${w.type === "video" ? "● Film" : "Photo"}</span>
        <img src="${w.img}" alt="${w.title}" loading="lazy" />
        <div class="media__cap"><h4>${w.title}</h4><p>${w.tag || w.brand}</p></div>
      </div>`).join("");
    $("#mediaGrid").innerHTML = items + `<button class="media media--add"><span style="text-align:center"><span class="plus">+</span><br><span style="font-size:.66rem;letter-spacing:.2em;text-transform:uppercase">Add media</span></span></button>`;
  }
  $$("#portfolioCats .tab").forEach((t) => t.addEventListener("click", () => {
    $$("#portfolioCats .tab").forEach((x) => x.classList.remove("is-active"));
    t.classList.add("is-active"); activeCat = t.dataset.cat; renderMedia();
  }));
  renderMedia();

  /* ---------- Category manager ----------------------------------------------- */
  $("#catManager").innerHTML = CATEGORIES.map((c) => `
    <div class="catrow">
      <span class="catrow__n">${c.n}</span>
      <span class="catrow__title">${c.title}</span>
      <span class="catrow__meta">${c.works.length} items · ${c.works.filter((w) => w.type === "video").length} films</span>
      <div class="switch on" role="switch" aria-checked="true" tabindex="0"></div>
    </div>`).join("");
  $$("#catManager .switch").forEach((sw) => {
    const toggle = () => { const on = sw.classList.toggle("on"); sw.setAttribute("aria-checked", on); };
    sw.addEventListener("click", toggle);
    sw.addEventListener("keydown", (e) => { if (e.key === "Enter" || e.key === " ") { e.preventDefault(); toggle(); } });
  });

  /* ---------- Messages ------------------------------------------------------- */
  $("#msgList").innerHTML = MESSAGES.map((m) => `
    <div class="msg ${m.unread ? "unread" : ""}">
      <div class="msg__av">${m.from.charAt(0)}</div>
      <div class="msg__body">
        <div class="msg__top">
          <span class="who"><b>${m.from}</b> <span>· ${m.brand}</span></span>
          <span class="time">${m.time}</span>
        </div>
        <p class="msg__text">${m.text}</p>
      </div>
    </div>`).join("");

  /* ---------- Settings form -------------------------------------------------- */
  const P = typeof PROFILE !== "undefined" ? PROFILE : {};
  const fields = [
    ["Name", P.nameFull || "Noor Al-Rashid"],
    ["Role", P.role || ""],
    ["Email", P.email || ""],
    ["Phone", P.phone || ""],
    ["Based in", P.base || ""],
    ["Instagram", P.instagram || ""],
  ];
  $("#settingsForm").innerHTML = fields.map(([l, v], i) => `
    <div class="field ${i >= 4 ? "" : ""}">
      <label>${l}</label>
      <input type="text" value="${v}" />
    </div>`).join("") + `<div class="form__actions" style="grid-column:1/-1;justify-content:flex-end"><button class="btn btn--solid">Save changes</button></div>`;

  /* ---------- Initial view from hash ---------------------------------------- */
  const initial = (location.hash || "#overview").replace("#", "");
  showView(TITLES[initial] ? initial : "overview");
})();
