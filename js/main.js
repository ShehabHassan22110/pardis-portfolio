/* =============================================================================
   main.js — interactions + rendering (no framework, no build step)
   Bilingual-aware: re-renders dynamic content on "langchange".
   ============================================================================= */
(function () {
  "use strict";
  const $ = (s, c = document) => c.querySelector(s);
  const $$ = (s, c = document) => Array.from(c.querySelectorAll(s));
  const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
  const lang = () => (window.currentLang ? window.currentLang() : "en");
  const isAr = () => lang() === "ar";

  const WA_NUMBER = (typeof PROFILE !== "undefined" && PROFILE.whatsapp) || "966500000000"; // set in js/data.js

  /* ---------- Image fallback plate ------------------------------------------ */
  function plateFor(el) {
    const wrap = document.createElement("div");
    wrap.className = "work__plate";
    wrap.style.background = `linear-gradient(140deg, ${el.dataset.g1 || "#2A2622"}, ${el.dataset.g2 || "#C9A17A"})`;
    wrap.style.color = "rgba(255,255,255,.92)";
    wrap.innerHTML = `<span class="pl">${el.dataset.label || ""}</span>`;
    return wrap;
  }
  function guardImages(scope = document) {
    $$("img[data-guard]", scope).forEach((img) => {
      img.addEventListener("error", () => img.replaceWith(plateFor(img)));
    });
  }

  /* ---------- Nav: solid on scroll + mobile menu ---------------------------- */
  const nav = $(".nav");
  if (nav) {
    const onScroll = () => nav.classList.toggle("is-solid", window.scrollY > 40);
    onScroll();
    window.addEventListener("scroll", onScroll, { passive: true });
  }
  const burger = $(".nav__burger");
  if (burger) {
    burger.addEventListener("click", () => document.body.classList.toggle("menu-open"));
    document.addEventListener("click", (e) => { if (e.target.closest(".nav__link")) document.body.classList.remove("menu-open"); });
  }

  /* ---------- Theme toggle -------------------------------------------------- */
  const THEME_KEY = "noor-theme";
  function currentTheme() { return document.documentElement.getAttribute("data-theme") || "dark"; }
  function setTheme(t) {
    const html = document.documentElement;
    html.classList.add("theming");
    html.setAttribute("data-theme", t);
    try { localStorage.setItem(THEME_KEY, t); } catch (e) {}
    setTimeout(() => html.classList.remove("theming"), 650);
  }
  function injectThemeToggle() {
    if (!nav || $(".theme-toggle", nav)) return;
    const btn = document.createElement("button");
    btn.className = "theme-toggle";
    btn.setAttribute("aria-label", "Toggle light and dark theme");
    btn.innerHTML = `
      <svg class="i-sun" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.4" stroke-linecap="round"><circle cx="12" cy="12" r="4.2"/><path d="M12 2v2.6M12 19.4V22M2 12h2.6M19.4 12H22M4.9 4.9l1.8 1.8M17.3 17.3l1.8 1.8M19.1 4.9l-1.8 1.8M6.7 17.3l-1.8 1.8"/></svg>
      <svg class="i-moon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.4" stroke-linecap="round" stroke-linejoin="round"><path d="M20 14.5A8 8 0 0 1 9.5 4a8 8 0 1 0 10.5 10.5z"/></svg>`;
    btn.addEventListener("click", () => setTheme(currentTheme() === "dark" ? "light" : "dark"));
    const burgerEl = $(":scope > .nav__burger", nav);
    if (burgerEl) nav.insertBefore(btn, burgerEl); else nav.appendChild(btn);
  }
  injectThemeToggle();

  /* ---------- Scroll progress ----------------------------------------------- */
  const progress = document.createElement("div");
  progress.className = "progress";
  document.body.appendChild(progress);
  const updateProgress = () => {
    const h = document.documentElement;
    const max = h.scrollHeight - h.clientHeight;
    progress.style.width = (max > 0 ? (h.scrollTop / max) * 100 : 0) + "%";
  };
  updateProgress();
  window.addEventListener("scroll", updateProgress, { passive: true });

  /* ---------- WhatsApp floating button -------------------------------------- */
  function buildWhatsApp() {
    const msg = encodeURIComponent(window.t ? window.t("wa.msg") : "Hello Noor");
    const a = document.createElement("a");
    a.className = "wa-float";
    a.href = `https://wa.me/${WA_NUMBER}?text=${msg}`;
    a.target = "_blank";
    a.rel = "noopener";
    a.setAttribute("aria-label", window.t ? window.t("wa.tooltip") : "WhatsApp");
    a.innerHTML = `
      <span class="wa-float__pulse" aria-hidden="true"></span>
      <svg viewBox="0 0 32 32" aria-hidden="true"><path fill="currentColor" d="M16 3C9.4 3 4 8.4 4 15c0 2.1.6 4.2 1.6 6L4 29l8.2-1.6c1.7.9 3.6 1.4 5.6 1.4h.2c6.6 0 12-5.4 12-12S22.6 3 16 3zm0 21.8c-1.8 0-3.5-.5-5-1.3l-.4-.2-4.9 1 1-4.7-.2-.4c-1-1.6-1.5-3.4-1.5-5.2C5 9.5 9.9 4.9 16 4.9c2.9 0 5.7 1.1 7.8 3.2A10.9 10.9 0 0 1 27 15.9c0 6.1-4.9 8.9-11 8.9zm6.1-8c-.3-.2-2-1-2.3-1.1-.3-.1-.5-.2-.8.2s-.9 1.1-1.1 1.3c-.2.2-.4.2-.7.1-1.8-.9-3-1.6-4.2-3.6-.3-.5.3-.5.9-1.6.1-.2 0-.4 0-.6s-.8-1.9-1.1-2.6c-.3-.7-.6-.6-.8-.6h-.7c-.2 0-.6.1-.9.4-.3.4-1.2 1.2-1.2 2.9s1.2 3.4 1.4 3.6c.2.2 2.5 3.8 6 5.3 2.2.9 3 1 4.1.9.7-.1 2-.8 2.3-1.6.3-.8.3-1.5.2-1.6-.1-.2-.3-.3-.6-.4z"/></svg>
      <span class="wa-float__label"></span>`;
    return a;
  }
  let waEl = null;
  if (nav) { // only on public site pages
    waEl = buildWhatsApp();
    document.body.appendChild(waEl);
  }
  function refreshWhatsApp() {
    if (!waEl) return;
    const msg = encodeURIComponent(window.t ? window.t("wa.msg") : "Hello");
    waEl.href = `https://wa.me/${WA_NUMBER}?text=${msg}`;
    const label = waEl.querySelector(".wa-float__label");
    if (label) label.textContent = window.t ? window.t("wa.tooltip") : "WhatsApp";
    waEl.setAttribute("aria-label", window.t ? window.t("wa.tooltip") : "WhatsApp");
  }

  /* ---------- Reveal + count-up observers ----------------------------------- */
  const io = new IntersectionObserver(
    (entries) => entries.forEach((e) => { if (e.isIntersecting) { e.target.classList.add("in"); io.unobserve(e.target); } }),
    { threshold: 0.12, rootMargin: "0px 0px -8% 0px" }
  );
  function observeReveals(scope = document) { $$(".reveal, .curtain, .goldline", scope).forEach((el) => io.observe(el)); }

  const countObserver = new IntersectionObserver((entries) => {
    entries.forEach((e) => {
      if (!e.isIntersecting) return;
      const el = e.target, numNode = el.firstChild;
      const target = parseInt((numNode.textContent || "").replace(/\D/g, ""), 10);
      if (!isNaN(target) && target > 0) {
        const dur = 1200, t0 = performance.now();
        const tick = (now) => { const p = Math.min((now - t0) / dur, 1); numNode.textContent = Math.round(target * (1 - Math.pow(1 - p, 3))); if (p < 1) requestAnimationFrame(tick); };
        requestAnimationFrame(tick);
      }
      countObserver.unobserve(el);
    });
  }, { threshold: 0.6 });
  if (!reduce) $$(".stat__v").forEach((el) => countObserver.observe(el));

  /* ---------- Magnetic buttons (idempotent) --------------------------------- */
  const canMagnet = window.matchMedia("(hover: hover)").matches && !reduce;
  function attachMagnetic(scope = document) {
    if (!canMagnet) return;
    $$(".btn--solid, .nav__cta, .theme-toggle", scope).forEach((el) => {
      if (el.dataset.mag) return;
      el.dataset.mag = "1"; el.classList.add("magnetic");
      el.addEventListener("mousemove", (e) => {
        const r = el.getBoundingClientRect();
        el.style.transform = `translate(${(e.clientX - r.left - r.width / 2) * 0.35}px, ${(e.clientY - r.top - r.height / 2) * 0.35}px)`;
      });
      el.addEventListener("mouseleave", () => { el.style.transform = ""; });
    });
  }
  attachMagnetic();

  /* ---------- Hero load sequence -------------------------------------------- */
  const hero = $(".hero");
  if (hero) requestAnimationFrame(() => setTimeout(() => hero.classList.add("in"), 80));

  /* ---------- Footer year + profile bindings -------------------------------- */
  $$("[data-year]").forEach((el) => (el.textContent = new Date().getFullYear()));
  if (typeof PROFILE !== "undefined") {
    $$("[data-profile]").forEach((el) => { if (PROFILE[el.dataset.profile] != null) el.textContent = PROFILE[el.dataset.profile]; });
    // email links
    $$("[data-profile-email]").forEach((el) => { el.href = "mailto:" + PROFILE.email; el.textContent = PROFILE.email; });
    // phone links
    $$("[data-profile-phone]").forEach((el) => {
      const k = el.querySelector(".k");
      el.href = "tel:" + PROFILE.phone.replace(/[^+\d]/g, "");
      el.innerHTML = (k ? k.outerHTML + " " : "") + PROFILE.phone;
    });
  }

  /* ---------- Connect: copy link + native share ----------------------------- */
  const shareUrl = location.protocol.indexOf("http") === 0 ? location.origin + "/" : "https://fashion-design-eta.vercel.app/";
  const copyBtn = $("#copyLink");
  if (copyBtn) {
    copyBtn.addEventListener("click", async () => {
      const label = copyBtn.textContent;
      try { await navigator.clipboard.writeText(shareUrl); } catch (e) {}
      copyBtn.textContent = window.t ? window.t("connect.copied") : "Copied ✓";
      setTimeout(() => (copyBtn.textContent = label), 1800);
    });
  }
  const shareBtn = $("#shareLink");
  if (shareBtn) {
    shareBtn.addEventListener("click", async () => {
      const data = { title: "PARDIS", text: "PARDIS — Model & Brand Ambassador", url: shareUrl };
      if (navigator.share) { try { await navigator.share(data); } catch (e) {} }
      else { try { await navigator.clipboard.writeText(shareUrl); shareBtn.textContent = window.t ? window.t("connect.copied") : "Copied ✓"; } catch (e) {} }
    });
  }

  /* ---------- Brands (Trusted by) ------------------------------------------- */
  const brandsRow = $("#brandsRow");
  if (brandsRow && typeof BRANDS !== "undefined") {
    const logos = BRANDS.map((b) =>
      `<span class="trusted__logo">${b.name}${b.em ? ` <em>${b.em}</em>` : ""}</span>`
    ).join('<span class="trusted__dot" aria-hidden="true">✦</span>');
    // duplicated track → seamless marquee loop (no messy wrapping at any width)
    brandsRow.innerHTML = `<div class="trusted__track">${logos}<span class="trusted__dot" aria-hidden="true">✦</span>${logos}<span class="trusted__dot" aria-hidden="true">✦</span></div>`;
    observeReveals(brandsRow);
  }

  /* ---------- Social links (data-driven, no invented URLs) ------------------ */
  if (typeof SOCIAL !== "undefined") {
    const byLabel = (name) => SOCIAL.find((s) => s.label.toLowerCase() === String(name).toLowerCase());
    const igUrl = (byLabel("Instagram") || {}).url || "#";
    // header social pills
    const socialLinks = $("#socialLinks");
    if (socialLinks) {
      socialLinks.innerHTML = SOCIAL.filter((s) => s.label !== "Email").map((s) =>
        `<a class="btn btn--gold btn--sm" href="${s.url}"${s.url.startsWith("#") ? "" : ' target="_blank" rel="noopener"'}>${s.label} <span class="btn__arrow">↗</span></a>`
      ).join("");
      attachMagnetic(socialLinks);
    }
    // footer social pills
    const footerSocial = $("#footerSocial");
    if (footerSocial) {
      footerSocial.innerHTML = SOCIAL.map((s) =>
        `<a href="${s.url}"${s.url.startsWith("#") ? "" : ' target="_blank" rel="noopener"'}>${s.label}</a>`
      ).join("");
    }
    // footer named links
    $$("[data-social-link]").forEach((el) => {
      const s = byLabel(el.dataset.socialLink);
      if (s) { el.href = s.url; if (s.url.startsWith("#")) { el.removeAttribute("target"); el.removeAttribute("rel"); } }
    });
    // instagram photo tiles
    $$("[data-social-tile]").forEach((el) => { el.href = igUrl; if (igUrl.startsWith("#")) { el.removeAttribute("target"); el.removeAttribute("rel"); } });
  }

  /* ---------- Bilingual field pickers --------------------------------------- */
  const cTitle = (c) => (isAr() && c.ar ? c.ar : c.title);
  const cTagline = (c) => (isAr() && c.arTagline ? c.arTagline : c.tagline);
  const cDesc = (c) => (isAr() && c.arDesc ? c.arDesc : c.desc);
  const cSub = (c) => (isAr() && c.arSub ? c.arSub : c.sub);

  /* ---------- Work card ----------------------------------------------------- */
  function workCard(w, i) {
    const isVideo = w.type === "video";
    const a = document.createElement("button");
    a.className = "work reveal";
    a.type = "button";
    a.setAttribute("aria-label", `${isVideo ? "Play" : "View"} ${w.title} — ${w.brand}`);
    a.dataset.caption = `${w.title} — ${w.brand}`;
    if (isVideo) { a.dataset.video = w.video || ""; a.dataset.embed = w.embed || ""; }
    else { a.dataset.full = w.img; }
    const badge = isVideo ? `<span class="work__badge"><span class="play"></span>Film</span>` : "";
    const playBtn = isVideo ? `<span class="work__play"><span></span></span>` : "";
    a.innerHTML = `
      ${badge}
      <div class="work__media curtain">
        ${playBtn}
        <img data-guard src="${w.img}" alt="${w.title} — ${w.brand}" loading="lazy"
             data-g1="${w._g1 || "#2A2622"}" data-g2="${w._g2 || "#C9A17A"}" data-label="${w.title}">
      </div>
      <div class="work__cap"><h4>${w.title}</h4><p>${w.tag || w.brand}</p></div>`;
    a.dataset.d = String((i % 3) + 1);
    return a;
  }

  /* ---------- HOME renders --------------------------------------------------- */
  const indexList = $("#indexList");
  const servicesGrid = $("#servicesGrid");
  const worksGrid = $("#worksGrid");
  const hasCats = typeof CATEGORIES !== "undefined";

  /* Hero cover swiper — slides + thumbnail filmstrip from data.js HERO_SLIDES.
     Rendered before motion.js so its swiper picks up the generated nodes. */
  function renderHero() {
    const slider = $("#heroSlider");
    if (!slider || typeof HERO_SLIDES === "undefined") return;
    slider.innerHTML = HERO_SLIDES.map((s, i) => `
      <figure class="hero__slide${i === 0 ? " is-active" : ""}" data-label="${s.label}">
        <img src="${s.img}" alt="Pardis — ${s.label}" ${i === 0 ? 'fetchpriority="high"' : 'loading="lazy"'} style="object-position:${s.pos}">
      </figure>`).join("");
    const thumbs = $("#heroThumbs");
    if (thumbs) thumbs.innerHTML = HERO_SLIDES.map((s, i) => `
      <button type="button" class="cover__thumb${i === 0 ? " is-active" : ""}" role="tab" aria-selected="${i === 0}" aria-label="${s.label}">
        <img src="${s.img}" alt="" loading="lazy" style="object-position:${s.pos}">
      </button>`).join("");
    const total = $("#heroTotal");
    if (total) total.textContent = "/" + String(HERO_SLIDES.length).padStart(2, "0");
  }
  renderHero();

  function renderIndex() {
    if (!indexList || !hasCats) return;
    indexList.innerHTML = CATEGORIES.map((c) => `
      <a class="icard reveal" href="category.html?cat=${c.slug}" aria-label="${cTitle(c)}">
        <div class="icard__media" style="background:linear-gradient(140deg, ${c.grad[0]}, ${c.grad[1]})">
          <img src="${c.cover}" alt="${cTitle(c)}" loading="lazy" onerror="this.remove()" style="object-position:${c.pos || "50% 22%"}">
        </div>
        <div class="icard__grad"></div>
        <span class="icard__n">${c.n}</span>
        <span class="icard__go" aria-hidden="true">↗</span>
        <div class="icard__body">
          <h3 class="icard__title display">${cTitle(c)}</h3>
          <p class="icard__sub">${cSub(c).slice(0, 3).join(" · ")}</p>
        </div>
      </a>`).join("");
    observeReveals(indexList);
  }

  function renderServices() {
    if (!servicesGrid || !hasCats) return;
    const cards = CATEGORIES.map((c, i) => `
      <div class="svc reveal" data-d="${i % 3}">
        <div class="svc__n">${c.n}</div>
        <div class="svc__title display">${cTitle(c)}</div>
        <p class="svc__desc">${cDesc(c)}</p>
        <div class="svc__tags">${cSub(c).slice(0, 3).map((s) => `<span class="tag">${s}</span>`).join("")}</div>
      </div>`).join("");
    const unsure = `
      <div class="svc svc--cta reveal" data-d="1">
        <div class="svc__title display" style="color:var(--champagne)">${window.t("services.unsureT")}</div>
        <p class="svc__desc">${window.t("services.unsureD")}</p>
        <a class="btn btn--gold" href="booking.html" style="margin-top:auto">${window.t("services.unsureCta")} <span class="btn__arrow">↗</span></a>
      </div>`;
    servicesGrid.innerHTML = cards + unsure;
    observeReveals(servicesGrid);
    attachMagnetic(servicesGrid);
  }

  function renderServiceRows() {
    const host = $("#serviceRows");
    if (!host || !hasCats) return;
    host.innerHTML = CATEGORIES.map((c, i) => `
      <a class="srow reveal" data-d="${i % 4}" href="category.html?cat=${c.slug}" aria-label="${cTitle(c)}">
        <span class="srow__n">${c.n}</span>
        <span class="srow__main">
          <span class="srow__title display">${cTitle(c)}</span>
          <span class="srow__desc">${cDesc(c)}</span>
          <span class="srow__tags">${cSub(c).slice(0, 4).map((s) => `<span class="tag">${s}</span>`).join("")}</span>
        </span>
        <span class="srow__media"><img src="${c.cover}" alt="${cTitle(c)}" loading="lazy" style="object-position:${c.pos || "50% 22%"}"></span>
        <span class="srow__go" aria-hidden="true">↗</span>
      </a>`).join("");
    observeReveals(host);
  }

  function renderWorks() {
    if (!worksGrid || !hasCats) return;
    const picks = [];
    CATEGORIES.forEach((c) => picks.push({ ...c.works[0], _g1: c.grad[0], _g2: c.grad[1] }));
    [[0, 2], [3, 1], [5, 2]].forEach(([ci, wi]) => {
      const c = CATEGORIES[ci]; if (c && c.works[wi]) picks.push({ ...c.works[wi], _g1: c.grad[0], _g2: c.grad[1] });
    });
    worksGrid.innerHTML = "";
    picks.forEach((w, i) => worksGrid.appendChild(workCard(w, i)));
    guardImages(worksGrid);
    observeReveals(worksGrid);
  }

  /* ---------- Index hover preview ------------------------------------------- */
  let previewInited = false;
  function initIndexPreview() {
    const previewEl = $("#idxPreview");
    if (!previewEl || !window.matchMedia("(hover: hover)").matches) return;
    const imgEl = $("img", previewEl), capEl = $(".cap", previewEl);
    let raf = null, tx = 0, ty = 0, cx = 0, cy = 0;
    const loop = () => { cx += (tx - cx) * 0.16; cy += (ty - cy) * 0.16; previewEl.style.left = cx + "px"; previewEl.style.top = cy + "px"; raf = requestAnimationFrame(loop); };
    $$(".idx").forEach((row) => {
      row.addEventListener("mouseenter", () => { imgEl.src = row.dataset.cover; capEl.textContent = row.dataset.title; previewEl.classList.add("is-on"); if (!raf) loop(); });
      row.addEventListener("mouseleave", () => previewEl.classList.remove("is-on"));
    });
    if (!previewInited) { window.addEventListener("mousemove", (e) => { tx = e.clientX; ty = e.clientY; }, { passive: true }); previewInited = true; }
  }

  renderIndex(); renderServices(); renderServiceRows(); renderWorks();

  /* ---------- CATEGORY page ------------------------------------------------- */
  const catRoot = $("#catRoot");
  function renderCategory() {
    if (!catRoot || !hasCats) return;
    const slug = new URLSearchParams(location.search).get("cat") || CATEGORIES[0].slug;
    const cat = getCategory(slug) || CATEGORIES[0];
    document.title = `${cTitle(cat)} — ${PROFILE.nameFull}`;
    const catCover = $("#catCover"); catCover.src = cat.cover; catCover.style.objectPosition = cat.pos || "50% 22%";
    $("#catN").textContent = cat.n + " / " + String(CATEGORIES.length).padStart(2, "0");
    $("#catTitle").innerHTML = `${cTitle(cat)}<span class="ar">${isAr() ? cat.title : cat.ar}</span>`;
    $("#catTagline").textContent = cTagline(cat);
    $("#catDesc").textContent = cDesc(cat);
    const subUl = $("#catSub"); subUl.innerHTML = "";
    cSub(cat).forEach((s) => { const li = document.createElement("li"); li.textContent = s; subUl.appendChild(li); });
    const gal = $("#catGallery"); gal.innerHTML = "";
    cat.works.forEach((w, i) => gal.appendChild(workCard({ ...w, _g1: cat.grad[0], _g2: cat.grad[1] }, i)));
    guardImages(gal); observeReveals(gal);
    const bookBtn = $("#catBook"); if (bookBtn) bookBtn.href = `booking.html?type=${encodeURIComponent(cat.title)}`;
    const idx = CATEGORIES.findIndex((c) => c.slug === cat.slug);
    const prev = CATEGORIES[(idx - 1 + CATEGORIES.length) % CATEGORIES.length];
    const next = CATEGORIES[(idx + 1) % CATEGORIES.length];
    $("#catPrev").href = `category.html?cat=${prev.slug}`; $("#catPrevT").textContent = cTitle(prev);
    $("#catNext").href = `category.html?cat=${next.slug}`; $("#catNextT").textContent = cTitle(next);
  }
  renderCategory();

  /* ---------- Lightbox (image + video) -------------------------------------- */
  let lb = $("#lightbox");
  if (!lb && ($(".work") || worksGrid || catRoot)) {
    lb = document.createElement("div"); lb.id = "lightbox"; lb.className = "lightbox"; lb.setAttribute("aria-hidden", "true");
    document.body.appendChild(lb);
  }
  if (lb) {
    lb.innerHTML = `
      <button class="lightbox__close" aria-label="Close">✕</button>
      <img class="lightbox__img hidden" src="" alt="" />
      <div class="lightbox__video hidden"></div>
      <p class="lightbox__cap"></p>`;
    const lbImg = $(".lightbox__img", lb), lbVid = $(".lightbox__video", lb), lbCap = $(".lightbox__cap", lb);
    const show = () => { lb.classList.add("is-on"); document.body.style.overflow = "hidden"; };
    const close = () => { lb.classList.remove("is-on"); document.body.style.overflow = ""; lbVid.innerHTML = ""; lbImg.src = ""; };
    const openImage = (src, cap) => { lbVid.classList.add("hidden"); lbVid.innerHTML = ""; lbImg.src = src; lbImg.classList.remove("hidden"); lbCap.textContent = cap || ""; show(); };
    const openVideo = ({ video, embed }, cap) => {
      lbImg.classList.add("hidden"); lbImg.src = "";
      if (embed) lbVid.innerHTML = `<iframe src="https://www.youtube.com/embed/${embed}?autoplay=1&rel=0" title="${cap}" allow="autoplay; encrypted-media; picture-in-picture" allowfullscreen></iframe>`;
      else if (video) lbVid.innerHTML = `<video src="${video}" controls autoplay playsinline></video>`;
      else lbVid.innerHTML = `<div style="display:grid;place-items:center;height:100%;color:var(--mist)">Video coming soon</div>`;
      lbVid.classList.remove("hidden"); lbCap.textContent = cap || ""; show();
    };
    document.addEventListener("click", (e) => {
      const card = e.target.closest(".work, [data-play]");
      if (card) {
        if (card.dataset.video !== undefined || card.dataset.embed) { e.preventDefault(); openVideo({ video: card.dataset.video, embed: card.dataset.embed }, card.dataset.caption); }
        else if (card.dataset.full) { e.preventDefault(); openImage(card.dataset.full, card.dataset.caption); }
      }
      if (e.target.closest(".lightbox__close") || e.target === lb) close();
    });
    document.addEventListener("keydown", (e) => { if (e.key === "Escape") close(); });
  }

  /* ---------- Booking form -------------------------------------------------- */
  const form = $("#bookingForm");
  if (form) {
    const type = new URLSearchParams(location.search).get("type");
    const select = $("#collabType", form);
    if (type && select) { const m = Array.from(select.options).find((o) => o.value === type); if (m) select.value = type; }
    form.addEventListener("submit", (e) => {
      e.preventDefault();
      if (!form.reportValidity()) return;
      form.classList.add("hidden");
      const ok = $("#formSuccess"); ok.classList.remove("hidden");
      ok.scrollIntoView({ behavior: "smooth", block: "center" });
    });
  }

  /* ---------- FAQ accordion ------------------------------------------------- */
  const syncFaq = () => $$(".faq__item.open .faq__a").forEach((a) => (a.style.maxHeight = a.scrollHeight + "px"));
  $$(".faq__q").forEach((q) => {
    q.addEventListener("click", () => {
      const item = q.parentElement, ans = q.nextElementSibling;
      const open = item.classList.toggle("open");
      ans.style.maxHeight = open ? ans.scrollHeight + "px" : "0";
    });
  });
  window.addEventListener("resize", syncFaq);

  /* ---------- Re-render on language change ---------------------------------- */
  window.addEventListener("langchange", () => {
    renderIndex(); renderServices(); renderServiceRows(); renderWorks(); renderCategory(); refreshWhatsApp();
    setTimeout(syncFaq, 40);
  });
  refreshWhatsApp();

  observeReveals();
  guardImages();
})();
