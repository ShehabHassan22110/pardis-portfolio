/* =============================================================================
   site.js — BARDEES ISSA public site (MVC build)
   Content is server-rendered in the current language; this file only wires
   interactions on that DOM: loader, sticky nav, theme toggle, hero carousel,
   clients marquee, reveal-on-scroll, lightbox, FAQ, video/work filters,
   back-to-top, share, motion. No content rendering, no data.js dependency.
   ============================================================================= */
(function () {
  "use strict";
  const $ = (s, r = document) => r.querySelector(s);
  const $$ = (s, r = document) => Array.from(r.querySelectorAll(s));
  const isAr = () => document.documentElement.getAttribute("dir") === "rtl";
  const reduce = () => matchMedia("(prefers-reduced-motion: reduce)").matches;

  /* --------------------------------------------------------- Loader -------- */
  function initLoader() {
    const ldr = $("#loader"); if (!ldr) return;
    let seen = null; try { seen = sessionStorage.getItem("bardees-loaded"); } catch (e) {}
    setTimeout(() => {
      ldr.classList.add("done");
      try { sessionStorage.setItem("bardees-loaded", "1"); } catch (e) {}
      setTimeout(() => ldr.remove(), 700);
    }, seen ? 300 : 1600);
  }

  /* ----------------------------------------------------------- Nav --------- */
  function initNav() {
    const nav = $("#nav");
    if (nav) { const f = () => nav.classList.toggle("is-solid", scrollY > 20); f(); addEventListener("scroll", f, { passive: true }); }
    $$("#navMenu .nav-link, #navMenu a.btn").forEach((a) => a.addEventListener("click", () => {
      const el = $("#navMenu"); if (el && el.classList.contains("show") && window.bootstrap) bootstrap.Collapse.getOrCreateInstance(el).hide();
    }));
  }

  /* ---------------------------------------------------- Theme toggle ------- */
  function setCookie(k, v) { document.cookie = k + "=" + v + ";path=/;max-age=31536000;samesite=lax"; }
  function initTheme() {
    $$("[data-theme-toggle]").forEach((b) => b.addEventListener("click", () => {
      const light = document.documentElement.getAttribute("data-theme") === "light";
      const next = light ? "dark" : "light";
      document.documentElement.setAttribute("data-theme", next);
      document.documentElement.setAttribute("data-bs-theme", next);
      setCookie("bardees-theme", next);
      $$("[data-theme-toggle] i").forEach((i) => (i.className = next === "light" ? "bi bi-moon-stars" : "bi bi-brightness-high"));
    }));
  }

  /* --------------------------------------------------- Reveal on scroll ---- */
  let io = null;
  function observeReveals() {
    const items = $$(".reveal:not(.in)");
    if (!("IntersectionObserver" in window)) { items.forEach((i) => i.classList.add("in")); return; }
    if (!io) io = new IntersectionObserver((es) => es.forEach((e) => { if (e.isIntersecting) { e.target.classList.add("in"); io.unobserve(e.target); } }),
      { rootMargin: "0px 0px -8% 0px", threshold: 0.12 });
    items.forEach((i) => io.observe(i));
  }

  /* ----------------------------------------------------------- Hero -------- */
  let heroTimer = null;
  function initHero() {
    const bg = $("#heroBg"); if (!bg) return;
    const slides = $$(".slide", bg);
    const thumbs = $$(".hero__thumb");
    const total = slides.length; if (!total) return;
    if ($("#heroTotal")) $("#heroTotal").textContent = "/" + String(total).padStart(2, "0");
    let cur = 0;
    const paint = () => {
      slides.forEach((sl, i) => sl.classList.toggle("active", i === cur));
      thumbs.forEach((b, i) => b.classList.toggle("active", i === cur));
      const label = thumbs[cur] ? thumbs[cur].getAttribute("data-label") : slides[cur].getAttribute("data-label");
      if ($("#heroLabel") && label) $("#heroLabel").textContent = label;
      if ($("#heroIndex")) $("#heroIndex").textContent = String(cur + 1).padStart(2, "0");
      const bar = $("#heroBar > i");
      if (bar) { bar.style.transition = "none"; bar.style.width = "0%"; void bar.offsetWidth; bar.style.transition = "width 6s linear"; bar.style.width = "100%"; }
    };
    const go = (i, user) => { cur = (i + total) % total; paint(); if (user) restart(); };
    const restart = () => { clearInterval(heroTimer); if (!reduce()) heroTimer = setInterval(() => go(cur + 1), 6000); };
    thumbs.forEach((b) => b.addEventListener("click", () => go(+b.dataset.i, true)));
    if ($("#heroPrev")) $("#heroPrev").onclick = () => go(cur - 1, true);
    if ($("#heroNext")) $("#heroNext").onclick = () => go(cur + 1, true);
    paint(); restart();
  }

  /* --------------------------------------------------------- Clients ------- */
  let clientsRaf = null;
  function initClients() {
    const vp = $("#clientsViewport"), track = $("#clientsGrid"), wrap = vp && vp.closest(".clients-swiper");
    if (!vp || !track || !wrap) return;
    const originals = $$(".cs-slide", track);
    const N = originals.length; if (!N) return;
    // Triple the set for a seamless wrap (only once).
    if (!track.dataset.cloned) {
      const html = track.innerHTML;
      track.innerHTML = html + html + html;
      track.dataset.cloned = "1";
      $$(".cs-slide", track).forEach((s, i) => { if (i >= N) s.setAttribute("aria-hidden", "true"); });
    }
    const slides = $$(".cs-slide", track);
    const prev = $("#clientsPrev"), next = $("#clientsNext");
    cancelAnimationFrame(clientsRaf);
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
    const frame = () => { if (!paused) { x -= 0.4; wrapX(); apply(); } clientsRaf = requestAnimationFrame(frame); };
    const nudge = (dir) => { x -= dir * step; wrapX(); apply(); };
    if (prev) prev.onclick = () => nudge(-1);
    if (next) next.onclick = () => nudge(1);
    if (!wrap.dataset.bound) {
      wrap.dataset.bound = "1";
      wrap.addEventListener("pointerenter", () => (paused = true));
      wrap.addEventListener("pointerleave", () => (paused = reduce()));
      let down = false, sx = 0, sX = 0, moved = false;
      vp.addEventListener("pointerdown", (e) => { down = true; moved = false; sx = e.clientX; sX = x; try { vp.setPointerCapture(e.pointerId); } catch (_) {} vp.classList.add("is-grab"); paused = true; });
      vp.addEventListener("pointermove", (e) => { if (!down) return; const dx = e.clientX - sx; if (Math.abs(dx) > 3) moved = true; x = sX + dx; wrapX(); apply(); });
      const end = () => { if (!down) return; down = false; vp.classList.remove("is-grab"); paused = reduce(); };
      vp.addEventListener("pointerup", end); vp.addEventListener("pointercancel", end);
      vp.addEventListener("click", (e) => { if (moved) { e.preventDefault(); e.stopPropagation(); } }, true);
      let rt = null; addEventListener("resize", () => { clearTimeout(rt); rt = setTimeout(measure, 150); });
    }
    apply();
    if (!reduce()) { paused = false; clientsRaf = requestAnimationFrame(frame); }
  }

  /* ------------------------------------------------------------- FAQ ------- */
  function initFaq() {
    $$(".faq-item").forEach((it) => { const q = $(".faq-q", it); if (q) q.addEventListener("click", () => {
      const open = it.classList.contains("open");
      $$(".faq-item").forEach((x) => x.classList.remove("open"));
      it.classList.toggle("open", !open); q.setAttribute("aria-expanded", String(!open));
    }); });
  }

  /* --------------------------------------------------------- Filters ------- */
  function initFilters() {
    $$("[data-filterbar]").forEach((bar) => {
      const targetSel = bar.getAttribute("data-target");
      const grid = targetSel ? $(targetSel) : null; if (!grid) return;
      const itemSel = bar.getAttribute("data-item") || ".mtile";
      bar.addEventListener("click", (e) => {
        const b = e.target.closest("[data-filter]"); if (!b) return;
        $$("[data-filter]", bar).forEach((x) => x.classList.remove("active"));
        b.classList.add("active");
        const f = b.dataset.filter;
        $$(itemSel, grid).forEach((it) => {
          const show = f === "all" || it.dataset.cat === f;
          it.style.display = show ? "" : "none";
          if (show && it.classList.contains("vid-card")) { it.classList.remove("vf-pop"); void it.offsetWidth; it.classList.add("vf-pop"); }
        });
      });
    });
  }

  /* ---------------------------------------------------------- Lightbox ----- */
  function esc(s) { return String(s == null ? "" : s).replace(/[&<>"]/g, (c) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;" }[c])); }
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

  /* ------------------------------------------------------- Back to top ----- */
  function initTop() {
    const b = $(".back-to-top"); if (!b) return;
    const f = () => (b.style.display = scrollY > 300 ? "inline-flex" : "none"); f();
    addEventListener("scroll", f, { passive: true });
    b.addEventListener("click", (e) => { e.preventDefault(); scrollTo({ top: 0, behavior: "smooth" }); });
  }

  /* ----------------------------------------------------------- Motion ------ */
  function initMotion() {
    let bar = $("#scrollbar"); if (!bar) { bar = document.createElement("div"); bar.id = "scrollbar"; document.body.appendChild(bar); }
    const onScroll = () => {
      const h = document.documentElement.scrollHeight - innerHeight;
      bar.style.width = (h > 0 ? (scrollY / h) * 100 : 0) + "%";
      const hb = $("#heroBg");
      if (hb && scrollY < innerHeight) hb.style.transform = "translateY(" + scrollY * 0.28 + "px)";
    };
    addEventListener("scroll", onScroll, { passive: true }); onScroll();
    const fine = matchMedia("(hover: hover) and (min-width: 992px)").matches;
    if (fine && !reduce()) {
      let glow = $("#cursor-glow"); if (!glow) { glow = document.createElement("div"); glow.id = "cursor-glow"; document.body.appendChild(glow); }
      addEventListener("mousemove", (e) => { glow.style.left = e.clientX + "px"; glow.style.top = e.clientY + "px"; glow.classList.add("on"); }, { passive: true });
      addEventListener("mouseleave", () => glow.classList.remove("on"));
      $$(".btn-gold, .hero__nav, .chip-btn").forEach((b) => {
        b.addEventListener("mousemove", (e) => { const r = b.getBoundingClientRect(); b.style.transform = "translate(" + (e.clientX - r.left - r.width / 2) * 0.25 + "px," + (e.clientY - r.top - r.height / 2) * 0.35 + "px)"; });
        b.addEventListener("mouseleave", () => (b.style.transform = ""));
      });
    }
  }

  function boot() {
    initLoader(); initNav(); initTheme(); observeReveals(); initHero(); initClients();
    initFaq(); initFilters(); initLightbox(); initTop(); initMotion();
  }
  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", boot); else boot();
})();
