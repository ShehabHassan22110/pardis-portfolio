/* =============================================================================
   app.js — BARDEES REFAAT  (Bootstrap build, vanilla JS)
   Renders every dynamic section from data.js (localised by I18N.lang),
   re-renders on language change, and wires all interactions:
   theme + language toggles, sticky nav, hero swiper, reveal-on-scroll,
   lightbox, reels, FAQ, contact form, back-to-top.
   ============================================================================= */
(function () {
  "use strict";
  const $ = (s, r = document) => r.querySelector(s);
  const $$ = (s, r = document) => Array.from(r.querySelectorAll(s));
  const lang = () => (window.I18N ? I18N.lang : "en");
  const isAr = () => lang() === "ar";
  const L = (o, k) => (!o ? "" : isAr() && o[k + "Ar"] != null ? o[k + "Ar"] : o[k]);
  const esc = (s) => String(s == null ? "" : s).replace(/[&<>"]/g, (c) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" }[c]));

  /* <picture> with webp + jpg fallback from a base name + rendition */
  function pic(base, rend, { alt = "", imgcls = "", loading = "lazy", cls = "" } = {}) {
    return `<picture class="${cls}"><source type="image/webp" srcset="assets/img/${base}-${rend}.webp"><img src="assets/img/${base}-${rend}.jpg" alt="${esc(alt)}" class="${imgcls}" loading="${loading}" decoding="async"></picture>`;
  }

  /* ------------------------------------------------------------- HERO ------ */
  let heroTimer = null;
  function renderHero() {
    const bg = $("#heroBg"), thumbs = $("#heroThumbs");
    if (!bg || typeof HERO_SLIDES === "undefined") return;
    bg.innerHTML = HERO_SLIDES.map((s, i) =>
      `<div class="slide${i === 0 ? " active" : ""}">${pic(s.img, "wide", { alt: L(s, "label"), imgcls: "", loading: i === 0 ? "eager" : "lazy" })}</div>`
    ).join("");
    if (thumbs) thumbs.innerHTML = HERO_SLIDES.map((s, i) =>
      `<button class="hero__thumb${i === 0 ? " active" : ""}" data-i="${i}" aria-label="${esc(L(s, "label"))}">${pic(s.img, "sq", { alt: "" })}</button>`
    ).join("");
    const total = HERO_SLIDES.length;
    if ($("#heroTotal")) $("#heroTotal").textContent = "/" + String(total).padStart(2, "0");
    let cur = 0;
    const paint = () => {
      $$(".slide", bg).forEach((sl, i) => sl.classList.toggle("active", i === cur));
      if (thumbs) $$(".hero__thumb", thumbs).forEach((b, i) => b.classList.toggle("active", i === cur));
      if ($("#heroLabel")) $("#heroLabel").textContent = L(HERO_SLIDES[cur], "label");
      if ($("#heroIndex")) $("#heroIndex").textContent = String(cur + 1).padStart(2, "0");
      const bar = $("#heroBar > i");
      if (bar) { bar.style.transition = "none"; bar.style.width = "0%"; void bar.offsetWidth; bar.style.transition = "width 6s linear"; bar.style.width = "100%"; }
    };
    const go = (i, user) => { cur = (i + total) % total; paint(); if (user) restart(); };
    const restart = () => { clearInterval(heroTimer); heroTimer = setInterval(() => go(cur + 1), 6000); };
    if (thumbs) $$(".hero__thumb", thumbs).forEach((b) => b.addEventListener("click", () => go(+b.dataset.i, true)));
    if ($("#heroPrev")) $("#heroPrev").onclick = () => go(cur - 1, true);
    if ($("#heroNext")) $("#heroNext").onclick = () => go(cur + 1, true);
    paint(); restart();
  }

  /* ----------------------------------------------------------- BRANDS ------ */
  function renderBrands() {
    const row = $("#brandsRow");
    if (!row || typeof BRANDS === "undefined") return;
    const one = BRANDS.map((b) => `<span class="d-inline-flex align-items-baseline gap-2" style="font-family:var(--font-display);font-size:1.5rem;color:var(--text-soft)">${esc(b.name)}${b.em ? `<em style="font-family:var(--font-body);font-style:normal;font-size:.6rem;letter-spacing:.14em;text-transform:uppercase;color:var(--muted)">${esc(b.em)}</em>` : ""}</span>`).join("");
    row.innerHTML = `<div class="marquee__track">${one}${one}</div>`;
  }

  /* ---------------------------------------------------------- CLIENTS ------ */
  /* Clients as a seamless, infinitely-repeating marquee of cards. The set is
     tripled so translateX can wrap by exactly one set-width with no visible
     jump. Pauses on hover/focus, supports drag, and arrows nudge by one card.
     Respects reduced-motion (no autoplay; drag/arrows still work). */
  function renderClients() {
    const grid = $("#clientsGrid");
    if (!grid || typeof CLIENTS === "undefined") return;
    const coverage = isAr() ? "الخليج" : "GCC";
    const card = (c, clone) => {
      const name = esc(L(c, "name")), kind = esc(L(c, "role"));
      return `<figure class="client-card cs-slide m-0"${clone ? ' aria-hidden="true"' : ""}>
        <div class="client-plate"><span class="client-logo" role="img" aria-label="${name}" style="-webkit-mask-image:url(assets/img/${c.logo}.png);mask-image:url(assets/img/${c.logo}.png)"></span></div>
        <figcaption class="client-meta"><span class="client-name">${name}</span><span class="client-role">${kind} · ${coverage}</span></figcaption>
      </figure>`;
    };
    const one = CLIENTS.map((c) => card(c, false)).join("");
    const clones = CLIENTS.map((c) => card(c, true)).join("");
    grid.innerHTML = one + clones + clones; // 3 sets → seamless wrap
    initClientsMarquee();
  }

  let clientsRaf = null;
  const CM = { pause: () => {}, resume: () => {}, x: () => 0, setX: () => {}, remeasure: () => {} };
  function initClientsMarquee() {
    const vp = $("#clientsViewport"), track = $("#clientsGrid"), wrap = vp && vp.closest(".clients-swiper");
    const prev = $("#clientsPrev"), next = $("#clientsNext");
    if (!vp || !track || !wrap) return;
    cancelAnimationFrame(clientsRaf); clientsRaf = null;
    const N = typeof CLIENTS !== "undefined" ? CLIENTS.length : 0;
    const slides = $$(".cs-slide", track);
    if (!slides.length || !N) return;
    const reduce = window.matchMedia && window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    let x = 0, setW = 0, step = 0, paused = false;
    const measure = () => {
      setW = slides[N] ? Math.abs(slides[N].offsetLeft - slides[0].offsetLeft) : track.scrollWidth / 3;
      step = slides[1] ? Math.abs(slides[1].offsetLeft - slides[0].offsetLeft) : setW / N;
    };
    measure();
    const scrollable = setW > 0 && track.scrollWidth > vp.clientWidth + 4;
    wrap.classList.toggle("is-static", !scrollable);
    if (!scrollable) { track.style.transform = ""; return; }

    const wrapX = () => { if (setW > 0) { while (x <= -setW) x += setW; while (x > 0) x -= setW; } };
    const apply = () => { track.style.transform = "translateX(" + x + "px)"; };
    const frame = () => { if (!paused) { x -= 0.4; wrapX(); apply(); } clientsRaf = requestAnimationFrame(frame); }; // track is LTR-locked → always advance left

    CM.pause = () => { paused = true; };
    CM.resume = () => { if (!reduce) paused = false; };
    CM.x = () => x;
    CM.setX = (v) => { x = v; wrapX(); apply(); };
    CM.remeasure = () => { measure(); wrapX(); apply(); };
    const nudge = (dir) => { x -= dir * step; wrapX(); apply(); };
    if (prev) prev.onclick = () => nudge(-1);
    if (next) next.onclick = () => nudge(1);

    if (!wrap.dataset.cmBound) {
      wrap.dataset.cmBound = "1";
      wrap.addEventListener("pointerenter", () => CM.pause());
      wrap.addEventListener("pointerleave", () => CM.resume());
      wrap.addEventListener("focusin", () => CM.pause());
      wrap.addEventListener("focusout", () => CM.resume());
      let down = false, sx = 0, sX = 0, moved = false;
      vp.addEventListener("pointerdown", (e) => { down = true; moved = false; sx = e.clientX; sX = CM.x(); try { vp.setPointerCapture(e.pointerId); } catch (_) {} vp.classList.add("is-grab"); CM.pause(); });
      vp.addEventListener("pointermove", (e) => { if (!down) return; const dx = e.clientX - sx; if (Math.abs(dx) > 3) moved = true; CM.setX(sX + dx); });
      const end = () => { if (!down) return; down = false; vp.classList.remove("is-grab"); CM.resume(); };
      vp.addEventListener("pointerup", end);
      vp.addEventListener("pointercancel", end);
      vp.addEventListener("click", (e) => { if (moved) { e.preventDefault(); e.stopPropagation(); } }, true);
      let rt = null;
      window.addEventListener("resize", () => { clearTimeout(rt); rt = setTimeout(() => CM.remeasure(), 150); });
    }
    apply();
    if (!reduce) { paused = false; clientsRaf = requestAnimationFrame(frame); }
  }

  /* -------------------------------------------------------------- WHY ------ */
  function renderWhy() {
    const g = $("#whyGrid");
    if (!g || typeof WHY === "undefined") return;
    g.innerHTML = WHY.map((w, i) =>
      `<div class="col-md-6 col-lg-4"><article class="card-lux reveal" data-d="${i % 3}"><div class="num mb-3">0${i + 1}</div><h3 class="h5 mb-2">${esc(L(w, "title"))}</h3><p class="mb-0 small">${esc(L(w, "body"))}</p></article></div>`
    ).join("");
  }

  /* ------------------------------------------------------ DISCIPLINES ------ */
  function renderDisciplines() {
    const wrap = $("#discPanels");
    if (!wrap || typeof CATEGORIES === "undefined") return;
    wrap.innerHTML = CATEGORIES.map((c, i) => {
      const soon = c.status === "soon";
      const subs = (isAr() ? c.arSub : c.sub) || [];
      return `<a href="category.html?cat=${c.slug}" class="panel reveal" data-d="${i % 4}">
        ${pic(c.cover, "portrait", { alt: L(c, "title") })}
        <span class="panel__scrim"></span>
        <span class="panel__n">${c.n}</span>
        ${soon ? `<span class="panel__soon">${esc(I18N.t("common.soon"))}</span>` : ""}
        <span class="panel__v">${esc(L(c, "title"))}</span>
        <span class="panel__full">
          <span class="panel__t">${esc(L(c, "title"))}</span>
          <span class="panel__ar">${esc(c.ar)}</span>
          <span class="panel__tags">${subs.slice(0, 3).map((s) => `<span>${esc(s)}</span>`).join("")}</span>
        </span>
      </a>`;
    }).join("");
  }

  /* --------------------------------------------------------- WORK ---------- */
  function featured() {
    const out = [];
    CATEGORIES.filter((c) => c.status === "live").forEach((c) => (c.works || []).slice(0, 4).forEach((w) => out.push({ ...w, cat: c })));
    return out.slice(0, 8);
  }
  /* editorial magazine plate — the .spread grid places each figure */
  function plate(w) {
    return `<a class="mtile reveal" data-lightbox data-full="assets/img/${w.img}-portrait.jpg" data-caption="${esc(w.title)} · ${esc(w.brand)}" role="button" tabindex="0" aria-label="${esc(w.title)}"${w.slug ? ` data-slug="${w.slug}"` : ""}>
      ${pic(w.img, "portrait", { alt: w.title })}
      <span class="mtile__ov"><span><span class="mtile__k">${esc(w.tag)}</span><span class="mtile__t">${esc(w.title)}</span></span></span>
    </a>`;
  }
  function renderWorks() {
    const g = $("#worksGrid");
    if (!g || typeof CATEGORIES === "undefined") return;
    g.innerHTML = featured().map((w, i) => plate(w, i)).join("");
  }
  function _renderWorksLegacy() {
    const g = $("#worksGrid");
    g.innerHTML = featured().map((w, i) => {
      const big = i % 6 === 0;
      const col = big ? "col-6 col-lg-6" : "col-6 col-lg-3";
      return `<div class="${col}"><figure class="work-tile reveal m-0" data-lightbox data-full="assets/img/${w.img}-portrait.jpg" data-caption="${esc(w.title)} — ${esc(w.brand)}" tabindex="0" role="button" aria-label="${esc(w.title)}" style="aspect-ratio:${big ? "16/10" : "3/4"}">
        ${pic(w.img, big ? "wide" : "portrait", { alt: w.title })}
        <figcaption class="work-tile__cap"><span><span class="t d-block">${esc(w.title)}</span><span class="k">${esc(w.tag)}</span></span><span>⤢</span></figcaption>
      </figure></div>`;
    }).join("");
  }

  /* ------------------------------------------------------ PORTFOLIO -------- */
  function renderPortfolio() {
    const grid = $("#portfolioGrid");
    if (!grid || typeof CATEGORIES === "undefined") return;
    const live = CATEGORIES.filter((c) => c.status === "live");
    const filters = $("#portfolioFilters");
    if (filters) {
      filters.innerHTML =
        `<button class="chip-btn active" data-filter="all">${isAr() ? "الكل" : "All"}</button>` +
        live.map((c) => `<button class="chip-btn" data-filter="${c.slug}">${esc(L(c, "title"))}</button>`).join("");
    }
    const items = [];
    live.forEach((c) => (c.works || []).forEach((w) => { if (w.type !== "video") items.push({ ...w, slug: c.slug }); }));
    grid.innerHTML = items.map((w, i) => plate(w, i)).join("");
    if (filters && !filters.dataset.bound) {
      filters.dataset.bound = "1";
      filters.addEventListener("click", (e) => {
        const b = e.target.closest("[data-filter]"); if (!b) return;
        $$("[data-filter]", filters).forEach((x) => x.classList.remove("active"));
        b.classList.add("active");
        const f = b.dataset.filter;
        $$(".mtile", grid).forEach((it) => (it.style.display = f === "all" || it.dataset.slug === f ? "" : "none"));
      });
    }
  }

  /* -------------------------------------------------------- REELS ---------- */
  function renderReels() {
    const g = $("#reelsGrid");
    if (!g || typeof REELS === "undefined") return;
    g.innerHTML = REELS.map((r) =>
      `<div class="col-sm-6"><article class="reel reveal" data-video="${r.video}" data-caption="${esc(r.caption)}" tabindex="0" role="button" aria-label="Play ${esc(L(r, "t"))}">
        <img src="${r.poster}" alt="${esc(L(r, "t"))}" loading="lazy">
        <span class="reel__dur">${esc(r.dur)}</span>
        <span class="reel__play"><i class="bi bi-play-fill fs-4"></i></span>
        <span class="reel__cap"><span class="k" style="font-size:.68rem;letter-spacing:.12em;text-transform:uppercase;color:var(--accent-2)">${esc(L(r, "k"))}</span><span class="display-serif fs-5">${esc(L(r, "t"))}</span></span>
      </article></div>`
    ).join("");
  }

  /* --------------------------------------------------- SHOWREEL ------------ */
  /* Home "In motion" section — a modern bento of YouTube films (1 large + 2
     stacked), cinematic hover, playing in the shared YouTube lightbox. Features
     the first three entries of VIDEOS; handles portrait (Shorts) gracefully. */
  function renderShowreel() {
    const g = $("#showreelGrid");
    if (!g || typeof VIDEOS === "undefined" || !VIDEOS.length) return;
    const catOf = (k) => (typeof VIDEO_CATS !== "undefined" ? VIDEO_CATS.find((c) => c.key === k) : null);
    const kLabel = (v) => { const c = catOf(v.cat); return c ? (isAr() ? c.ar : c.en) : (isAr() ? "فيلم" : "Film"); };
    const poster = (id) => `src="https://i.ytimg.com/vi/${esc(id)}/maxresdefault.jpg" onerror="this.onerror=null;this.src='https://i.ytimg.com/vi/${esc(id)}/hqdefault.jpg'"`;
    const item = (v, main) =>
      `<article class="sr-item${main ? " sr-item--main" : ""}${v.portrait ? " sr-item--portrait" : ""} reveal" data-youtube="${esc(v.id)}"${v.portrait ? ' data-portrait="1"' : ""} role="button" tabindex="0" aria-label="Play ${esc(L(v, "title"))}">
        ${v.portrait ? `<img class="sr-item__bg" ${poster(v.id)} alt="" aria-hidden="true" loading="lazy" decoding="async">` : ""}
        <img class="sr-item__img" ${poster(v.id)} alt="${esc(L(v, "title"))}" loading="lazy" decoding="async">
        <span class="sr-item__scrim" aria-hidden="true"></span>
        <span class="sr-item__frame" aria-hidden="true"></span>
        <span class="sr-item__play"><i class="bi bi-play-fill"></i></span>
        <span class="sr-item__meta"><span class="k">${esc(kLabel(v))}</span><span class="t">${esc(L(v, "title"))}</span></span>
      </article>`;
    const feat = VIDEOS.slice(0, 3);
    const main = feat[0] ? item(feat[0], true) : "";
    const side = feat.slice(1).map((v) => item(v, false)).join("");
    g.innerHTML = `${main}${side ? `<div class="showreel-feat__side">${side}</div>` : ""}`;
  }

  /* ----------------------------------------------------- PRESENCE ---------- */
  function renderPresence() {
    if ($("#statsRow") && typeof STATS !== "undefined")
      $("#statsRow").innerHTML = STATS.map((s) => `<div class="col-6 col-md-3"><div class="stat reveal"><div class="stat__v">${esc(s.value)}</div><div class="stat__l">${esc(isAr() ? s.ar : s.label)}</div></div></div>`).join("");
    if ($("#platformsRow") && typeof PLATFORMS !== "undefined")
      $("#platformsRow").innerHTML = PLATFORMS.map((p) => `<span class="pill-tag">${esc(p)}</span>`).join(" ");
    if ($("#styleRow") && typeof CONTENT_STYLE !== "undefined")
      $("#styleRow").innerHTML = CONTENT_STYLE.map((s) => `<span class="pill-tag pill-soft">${esc(isAr() ? s.ar : s.en)}</span>`).join(" ");
  }

  /* --------------------------------------------------- MARKETING ----------- */
  /* Cinematic partner showcase (About): title + two YouTube films that open in
     the lightbox. Posters come from img.ytimg.com with an hq fallback. */
  function renderMarketing() {
    const wrap = $("#marketingFeature");
    if (!wrap || typeof MARKETING === "undefined") return;
    const m = MARKETING;
    const vids = (m.videos || []).map((v, i) =>
      `<article class="mkt-video reveal" data-d="${i + 1}" data-youtube="${esc(v.id)}" role="button" tabindex="0" aria-label="Play ${esc(L(v, "title"))}">
        <img src="https://i.ytimg.com/vi/${esc(v.id)}/maxresdefault.jpg" onerror="this.onerror=null;this.src='https://i.ytimg.com/vi/${esc(v.id)}/hqdefault.jpg'" alt="${esc(L(v, "title"))}" loading="lazy" decoding="async">
        <span class="mkt-video__frame" aria-hidden="true"></span>
        <span class="mkt-video__scrim" aria-hidden="true"></span>
        <span class="mkt-video__play"><i class="bi bi-play-fill"></i></span>
        <span class="mkt-video__cap"><span class="k">${esc(L(v, "tag"))}</span><span class="t">${esc(L(v, "title"))}</span></span>
      </article>`
    ).join("");
    wrap.innerHTML =
      `<div class="title-c reveal">
        <span class="tc-eyebrow">${esc(L(m, "eyebrow"))}</span>
        <h2 class="d-1 tc-h">${L(m, "title")}</h2>
        <p class="tc-note">${esc(L(m, "note"))}</p>
      </div>
      <div class="mkt-partner reveal" data-d="1"><span class="mkt-partner__dot"></span><span class="mkt-partner__name">${esc(L(m, "client"))}</span><span class="mkt-partner__role">${esc(L(m, "role"))}</span></div>
      <div class="mkt-videos">${vids}</div>`;
  }

  /* --------------------------------------------------- VIDEOS PAGE --------- */
  /* Full video library with category tabs. Tabs are built from the categories
     that have videos; clicking a tab filters the grid with a pop animation.
     Cards open in the shared YouTube lightbox (data-youtube). */
  function renderVideos() {
    const grid = $("#videosGrid");
    if (!grid || typeof VIDEOS === "undefined") return;
    const tabsEl = $("#videosTabs");
    const catOf = (k) => (typeof VIDEO_CATS !== "undefined" ? VIDEO_CATS.find((c) => c.key === k) : null);
    const catLabel = (k) => { const c = catOf(k); return c ? (isAr() ? c.ar : c.en) : k; };

    if (tabsEl && typeof VIDEO_CATS !== "undefined") {
      const present = VIDEO_CATS.filter((c) => VIDEOS.some((v) => v.cat === c.key));
      tabsEl.innerHTML =
        `<button class="vtab active" data-filter="all">${isAr() ? "الكل" : "All"}<span class="vtab__n">${VIDEOS.length}</span></button>` +
        present.map((c) => `<button class="vtab" data-filter="${esc(c.key)}">${esc(isAr() ? c.ar : c.en)}<span class="vtab__n">${VIDEOS.filter((v) => v.cat === c.key).length}</span></button>`).join("");
    }

    const poster = (id) => `src="https://i.ytimg.com/vi/${esc(id)}/maxresdefault.jpg" onerror="this.onerror=null;this.src='https://i.ytimg.com/vi/${esc(id)}/hqdefault.jpg'"`;
    grid.innerHTML = VIDEOS.map((v, i) =>
      `<article class="vid-card reveal vf-pop${v.portrait ? " vid-card--portrait" : ""}" data-d="${i % 3}" data-cat="${esc(v.cat)}" data-youtube="${esc(v.id)}"${v.portrait ? ' data-portrait="1"' : ""} role="button" tabindex="0" aria-label="Play ${esc(L(v, "title"))}">
        <div class="vid-card__thumb">
          ${v.portrait ? `<img class="vid-card__bg" ${poster(v.id)} alt="" aria-hidden="true" loading="lazy" decoding="async">` : ""}
          <img class="vid-card__img" ${poster(v.id)} alt="${esc(L(v, "title"))}" loading="lazy" decoding="async">
          <span class="vid-card__scrim" aria-hidden="true"></span>
          <span class="vid-card__play"><i class="bi bi-play-fill"></i></span>
          <span class="vid-card__cat">${esc(catLabel(v.cat))}</span>
        </div>
        <div class="vid-card__body">
          <h3 class="vid-card__t">${esc(L(v, "title"))}</h3>
          ${L(v, "client") ? `<p class="vid-card__sub">${esc(L(v, "client"))}</p>` : ""}
        </div>
      </article>`
    ).join("");

    if (tabsEl && !tabsEl.dataset.bound) {
      tabsEl.dataset.bound = "1";
      tabsEl.addEventListener("click", (e) => {
        const b = e.target.closest("[data-filter]"); if (!b) return;
        $$("[data-filter]", tabsEl).forEach((x) => x.classList.remove("active"));
        b.classList.add("active");
        const f = b.dataset.filter;
        $$(".vid-card", grid).forEach((card) => {
          const show = f === "all" || card.dataset.cat === f;
          card.classList.toggle("is-hidden", !show);
          if (show) { card.classList.remove("vf-pop"); void card.offsetWidth; card.classList.add("vf-pop"); }
        });
      });
    }
  }

  /* ------------------------------------------------------ SERVICES --------- */
  const SVC_ICONS = ["person-badge", "camera-reels", "phone", "chat-quote-fill", "easel2", "geo-alt", "cup-hot", "graph-up-arrow", "megaphone", "code-slash"];
  function renderServices() {
    const g = $("#servicesIndex");
    if (!g || typeof SERVICES === "undefined") return;
    const limit = parseInt(g.dataset.limit || "0", 10);
    const list = limit > 0 ? SERVICES.slice(0, limit) : SERVICES;
    const col = g.dataset.cols || "col-md-6 col-lg-3";
    const per = /col-lg-4/.test(col) ? 3 : 4;
    g.innerHTML = list.map((s, i) =>
      `<div class="${col}">
        <article class="svc reveal" data-d="${i % per}">
          <span class="svc__ic"><i class="bi bi-${SVC_ICONS[i] || "star"}"></i></span>
          <span class="svc__n">${s.n}</span>
          <h3 class="svc__t">${esc(L(s, "title"))}</h3>
          <p class="svc__ar">${esc(s.titleAr)}</p>
          <p class="svc__d">${esc(L(s, "desc"))}</p>
        </article>
      </div>`
    ).join("");
  }

  /* ---------------------------------------------------------- misc --------- */
  function renderFacts() {
    const l = $("#factsList");
    if (!l || typeof PROFILE === "undefined") return;
    l.innerHTML = PROFILE.facts.map((f) => `<li><span class="k">${esc(isAr() ? f.ar : f.label)}</span><span class="v">${esc(isAr() ? f.arValue : f.value)}</span></li>`).join("");
  }
  function renderSocial() {
    const icon = { instagram: "instagram", tiktok: "tiktok", snapchat: "snapchat", whatsapp: "whatsapp", envelope: "envelope-fill" };
    $$("[data-social-links]").forEach((t) => {
      t.innerHTML = SOCIAL.map((s) => `<a href="${s.url}" ${s.url.startsWith("http") ? 'target="_blank" rel="noopener"' : ""} class="fs-5 me-3" aria-label="${esc(s.label)}"><i class="bi bi-${icon[s.icon] || "link-45deg"}"></i></a>`).join("");
    });
  }
  function renderProfile() {
    if (typeof PROFILE === "undefined") return;
    $$("[data-profile-email]").forEach((a) => { a.textContent = PROFILE.email; a.href = "mailto:" + PROFILE.email; });
    $$("[data-profile-phone]").forEach((a) => { a.textContent = PROFILE.phone; a.href = "tel:" + PROFILE.phone.replace(/\s+/g, ""); });
    $$("[data-profile-whatsapp]").forEach((a) => { a.href = "https://wa.me/" + PROFILE.whatsapp; });
    $$("[data-year]").forEach((e) => (e.textContent = new Date().getFullYear()));
  }

  /* --------------------------------------------------- CATEGORY page ------- */
  function renderCategory() {
    const gal = $("#catGallery");
    if (!gal || typeof CATEGORIES === "undefined") return;
    const slug = new URLSearchParams(location.search).get("cat") || CATEGORIES[0].slug;
    const idx = Math.max(0, CATEGORIES.findIndex((c) => c.slug === slug));
    const c = CATEGORIES[idx], total = CATEGORIES.length;
    const set = (s, v) => { const e = $(s); if (e) e.textContent = v; };
    set("#catN", `${c.n} / ${String(total).padStart(2, "0")}`);
    set("#catTitle", L(c, "title")); set("#catAr", c.ar); set("#catTagline", L(c, "tagline")); set("#catDesc", L(c, "desc"));
    document.title = `${L(c, "title")} — BARDEES REFAAT`;
    const cover = $("#catCover");
    if (cover) cover.innerHTML = pic(c.cover, "portrait", { alt: L(c, "title"), loading: "eager" });
    if ($("#catSub")) $("#catSub").innerHTML = ((isAr() ? c.arSub : c.sub) || []).map((s) => `<li class="facts-sub py-2" style="border-bottom:1px solid var(--border);color:var(--text-soft)">${esc(s)}</li>`).join("");
    if ($("#catBook")) $("#catBook").href = "contact.html?type=" + encodeURIComponent(c.title);
    if ((c.works || []).length) {
      gal.innerHTML = c.works.map((w, i) => {
        if (w.type === "video") return `<a class="mtile reveal" data-video="${w.video}" data-caption="${esc(w.title)}" role="button" tabindex="0" aria-label="Play ${esc(w.title)}">${pic(w.img, "portrait", { alt: w.title })}<span class="mtile__play"><i class="bi bi-play-fill fs-5"></i></span><span class="mtile__ov"><span><span class="mtile__k">${esc(w.tag)}</span><span class="mtile__t">${esc(w.title)}</span></span></span></a>`;
        return plate(w);
      }).join("");
    } else {
      gal.innerHTML = `<div class="col-12"><div class="text-center p-5" style="border:1px dashed var(--accent);border-radius:16px;background:var(--card)">
        <p class="eyebrow mb-3" style="color:var(--accent)">${esc(I18N.t("common.soon"))}</p>
        <p class="mx-auto mb-4" style="max-width:32rem">${esc(I18N.t("common.soonNote"))}</p>
        <a class="btn btn-gold" href="contact.html?type=${encodeURIComponent(c.title)}">${esc(I18N.t("common.enquire"))} <i class="bi bi-arrow-up-right"></i></a></div></div>`;
    }
    const prev = CATEGORIES[(idx - 1 + total) % total], next = CATEGORIES[(idx + 1) % total];
    if ($("#catPrev")) { $("#catPrev").href = "category.html?cat=" + prev.slug; if ($("#catPrevT")) $("#catPrevT").textContent = L(prev, "title"); }
    if ($("#catNext")) { $("#catNext").href = "category.html?cat=" + next.slug; if ($("#catNextT")) $("#catNextT").textContent = L(next, "title"); }
  }

  /* =============================== render all ============================= */
  function renderAll() {
    renderHero(); renderBrands(); renderClients(); renderWhy(); renderDisciplines(); renderWorks();
    renderReels(); renderShowreel(); renderPresence(); renderMarketing(); renderVideos(); renderServices(); renderFacts(); renderSocial();
    renderProfile(); renderCategory(); renderPortfolio(); observeReveals();
  }

  /* ============================= interactions ============================ */
  let io = null;
  function observeReveals() {
    const items = $$(".reveal:not(.in)");
    if (!("IntersectionObserver" in window)) { items.forEach((i) => i.classList.add("in")); return; }
    if (!io) io = new IntersectionObserver((es) => es.forEach((e) => { if (e.isIntersecting) { e.target.classList.add("in"); io.unobserve(e.target); } }), { rootMargin: "0px 0px -8% 0px", threshold: 0.12 });
    items.forEach((i) => io.observe(i));
  }
  function initNav() {
    const nav = $("#nav");
    if (nav) { const f = () => nav.classList.toggle("is-solid", scrollY > 20); f(); addEventListener("scroll", f, { passive: true }); }
    // close mobile collapse on link click
    $$("#navMenu .nav-link, #navMenu a.btn").forEach((a) => a.addEventListener("click", () => {
      const el = $("#navMenu"); if (el && el.classList.contains("show") && window.bootstrap) bootstrap.Collapse.getOrCreateInstance(el).hide();
    }));
  }
  function initToggles() {
    $$("[data-lang-toggle]").forEach((b) => b.addEventListener("click", () => window.I18N && I18N.toggleLang()));
    $$("[data-theme-toggle]").forEach((b) => b.addEventListener("click", () => {
      const next = document.documentElement.getAttribute("data-theme") === "light" ? "dark" : "light";
      I18N.setTheme(next); syncThemeIcon();
    }));
    document.addEventListener("langchange", () => { renderAll(); syncLangLabel(); });
  }
  function syncThemeIcon() {
    const light = document.documentElement.getAttribute("data-theme") === "light";
    $$("[data-theme-toggle] i").forEach((i) => (i.className = light ? "bi bi-moon-stars" : "bi bi-brightness-high"));
  }
  function syncLangLabel() { $$("[data-lang-toggle]").forEach((b) => (b.textContent = isAr() ? "EN" : "ع")); }

  function initFaq() {
    $$(".faq-item").forEach((it) => { const q = $(".faq-q", it); if (q) q.addEventListener("click", () => { const open = it.classList.contains("open"); $$(".faq-item").forEach((x) => x.classList.remove("open")); it.classList.toggle("open", !open); q.setAttribute("aria-expanded", !open); }); });
  }
  function initLightbox() {
    let box = $("#lb"); if (!box) { box = document.createElement("div"); box.id = "lb"; box.className = "lb-box"; document.body.appendChild(box); }
    const close = () => { box.classList.remove("open"); box.innerHTML = ""; document.body.style.overflow = ""; };
    box.addEventListener("click", (e) => { if (e.target === box || e.target.closest("[data-close]")) close(); });
    addEventListener("keydown", (e) => e.key === "Escape" && close());
    document.addEventListener("click", (e) => {
      const im = e.target.closest("[data-lightbox]"), vd = e.target.closest("[data-video]"), yt = e.target.closest("[data-youtube]");
      if (im) { box.innerHTML = `<button data-close class="lb-close" aria-label="Close">✕</button><figure class="text-center m-0"><img src="${im.getAttribute("data-full")}" alt=""><figcaption class="small mt-2" style="color:#cbb98f">${esc(im.getAttribute("data-caption") || "")}</figcaption></figure>`; box.classList.add("open"); document.body.style.overflow = "hidden"; }
      else if (yt) { const id = yt.getAttribute("data-youtube"), portrait = yt.getAttribute("data-portrait"); box.innerHTML = `<button data-close class="lb-close" aria-label="Close">✕</button><div class="lb-yt${portrait ? " lb-yt--portrait" : ""}"><iframe src="https://www.youtube.com/embed/${id}?autoplay=1&rel=0&playsinline=1" title="Video" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe></div>`; box.classList.add("open"); document.body.style.overflow = "hidden"; }
      else if (vd) { box.innerHTML = `<button data-close class="lb-close" aria-label="Close">✕</button><video src="${vd.getAttribute("data-video")}" controls autoplay playsinline></video>`; box.classList.add("open"); document.body.style.overflow = "hidden"; }
    });
    addEventListener("keydown", (e) => { if ((e.key === "Enter" || e.key === " ") && e.target.matches && e.target.matches("[data-lightbox],[data-video],[data-youtube]")) { e.preventDefault(); e.target.click(); } });
  }
  function initForm() {
    const f = $("#bookingForm"); if (!f) return;
    const t = $("#collabType");
    if (t && typeof CATEGORIES !== "undefined" && t.children.length <= 1) CATEGORIES.forEach((c) => { const o = document.createElement("option"); o.value = c.title; o.textContent = L(c, "title"); t.appendChild(o); });
    f.addEventListener("submit", (e) => { e.preventDefault(); if (!f.checkValidity()) { f.reportValidity(); return; } f.classList.add("d-none"); const ok = $("#formSuccess"); if (ok) ok.classList.remove("d-none"); });
  }
  function initTop() {
    const b = $(".back-to-top"); if (!b) return;
    addEventListener("scroll", () => (b.style.display = scrollY > 300 ? "inline-flex" : "none"), { passive: true });
    b.addEventListener("click", (e) => { e.preventDefault(); scrollTo({ top: 0, behavior: "smooth" }); });
  }
  function initShare() {
    const c = $("#copyLink"); if (c) c.addEventListener("click", async () => { try { await navigator.clipboard.writeText(location.href); const o = c.textContent; c.textContent = isAr() ? "✓ تم النسخ" : "✓ Copied"; setTimeout(() => (c.textContent = o), 1500); } catch (e) {} });
    const s = $("#shareLink"); if (s && navigator.share) s.addEventListener("click", () => navigator.share({ title: "BARDEES REFAAT", url: location.href }).catch(() => {}));
  }

  /* ------------------------------------------------------- MOTION ---------- */
  function initMotion() {
    // scroll progress bar
    let bar = $("#scrollbar");
    if (!bar) { bar = document.createElement("div"); bar.id = "scrollbar"; document.body.appendChild(bar); }
    const onScroll = () => {
      const h = document.documentElement.scrollHeight - innerHeight;
      bar.style.width = (h > 0 ? (scrollY / h) * 100 : 0) + "%";
      const hb = $("#heroBg");
      if (hb && scrollY < innerHeight) hb.style.transform = "translateY(" + scrollY * 0.28 + "px)"; // parallax
    };
    addEventListener("scroll", onScroll, { passive: true });
    onScroll();

    const fine = matchMedia("(hover: hover) and (min-width: 992px)").matches;
    if (fine && !matchMedia("(prefers-reduced-motion: reduce)").matches) {
      // cursor glow
      let glow = $("#cursor-glow");
      if (!glow) { glow = document.createElement("div"); glow.id = "cursor-glow"; document.body.appendChild(glow); }
      addEventListener("mousemove", (e) => { glow.style.left = e.clientX + "px"; glow.style.top = e.clientY + "px"; glow.classList.add("on"); }, { passive: true });
      addEventListener("mouseleave", () => glow.classList.remove("on"));
      // magnetic buttons
      $$(".btn-gold, .hero__nav, .chip-btn").forEach((b) => {
        b.addEventListener("mousemove", (e) => {
          const r = b.getBoundingClientRect();
          b.style.transform = "translate(" + (e.clientX - r.left - r.width / 2) * 0.25 + "px," + (e.clientY - r.top - r.height / 2) * 0.35 + "px)";
        });
        b.addEventListener("mouseleave", () => (b.style.transform = ""));
      });
    }
  }

  function initWhatsApp() {
    if ($(".wa-float") || typeof PROFILE === "undefined") return;
    const a = document.createElement("a");
    a.className = "wa-float";
    a.href = "https://wa.me/" + PROFILE.whatsapp;
    a.target = "_blank";
    a.rel = "noopener";
    a.setAttribute("aria-label", "WhatsApp");
    a.innerHTML = '<i class="bi bi-whatsapp"></i>';
    document.body.appendChild(a);
  }

  function initLoader() {
    const ldr = $("#loader");
    if (!ldr) return;
    let seen = null; try { seen = sessionStorage.getItem("bardees-loaded"); } catch (e) {}
    const delay = seen ? 300 : 2000; // full logo-draw on first visit, quick fade after
    setTimeout(() => {
      ldr.classList.add("done");
      try { sessionStorage.setItem("bardees-loaded", "1"); } catch (e) {}
      setTimeout(() => ldr.remove(), 700);
    }, delay);
  }

  function boot() {
    initLoader();
    if (window.I18N) I18N.apply();
    renderAll(); initNav(); initToggles(); initFaq(); initLightbox(); initForm(); initTop(); initShare(); initMotion(); initWhatsApp();
    syncThemeIcon(); syncLangLabel();
  }
  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", boot); else boot();
})();
