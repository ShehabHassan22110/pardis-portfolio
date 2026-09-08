/* =============================================================================
   motion.js — the "wow" layer: preloader, custom cursor, parallax, card tilt,
   marquee velocity. Loaded after main.js on public pages.
   Everything is gated behind prefers-reduced-motion and pointer capability.
   ============================================================================= */
(function () {
  "use strict";
  const $ = (s, c = document) => c.querySelector(s);
  const $$ = (s, c = document) => Array.from(c.querySelectorAll(s));
  const reduce = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
  const fine = window.matchMedia("(hover: hover) and (pointer: fine)").matches;

  /* ---------- Hero swiper (3-slide cinematic carousel) ---------------------- */
  (function heroSwiper() {
    const root = $("#heroSlider");
    if (!root) return;
    const slides = $$(".hero__slide", root);
    if (slides.length < 2) return;
    const dots = $$("#heroDots .hero__dot");
    const idxEl = $("#heroIndex"), labelEl = $("#heroLabel");
    const prev = $("#heroPrev"), next = $("#heroNext");
    let i = 0, timer = null;
    const DELAY = 5600;
    const pad = (n) => String(n + 1).padStart(2, "0");

    function go(n) {
      i = (n + slides.length) % slides.length;
      slides.forEach((s, k) => s.classList.toggle("is-active", k === i));
      dots.forEach((d, k) => d.classList.toggle("is-active", k === i));
      if (idxEl) idxEl.textContent = pad(i);
      if (labelEl) labelEl.textContent = slides[i].dataset.label || "";
    }
    const nextSlide = () => go(i + 1);
    const prevSlide = () => go(i - 1);

    function start() { if (reduce) return; stop(); timer = setInterval(nextSlide, DELAY); }
    function stop() { if (timer) { clearInterval(timer); timer = null; } }
    function restart() { stop(); start(); }

    dots.forEach((d, k) => d.addEventListener("click", () => { go(k); restart(); }));
    if (next) next.addEventListener("click", () => { nextSlide(); restart(); });
    if (prev) prev.addEventListener("click", () => { prevSlide(); restart(); });

    const hero = root.closest(".hero");
    hero.addEventListener("mouseenter", stop);
    hero.addEventListener("mouseleave", start);
    document.addEventListener("visibilitychange", () => (document.hidden ? stop() : start()));

    // swipe (touch / pointer)
    let sx = 0, sw = false;
    root.addEventListener("pointerdown", (e) => { sx = e.clientX; sw = true; }, { passive: true });
    root.addEventListener("pointerup", (e) => {
      if (!sw) return; sw = false;
      const dx = e.clientX - sx;
      if (Math.abs(dx) > 44) { dx < 0 ? nextSlide() : prevSlide(); restart(); }
    }, { passive: true });

    // keyboard when a hero control is focused
    [prev, next, ...dots].filter(Boolean).forEach((el) =>
      el.addEventListener("keydown", (e) => {
        if (e.key === "ArrowRight") { nextSlide(); restart(); }
        else if (e.key === "ArrowLeft") { prevSlide(); restart(); }
      })
    );

    go(0);
    start();
  })();

  /* ---------- Intro preloader (index only) ---------------------------------- */
  const pre = $(".preloader");
  if (pre) {
    const done = () => {
      pre.classList.add("is-done");
      try { sessionStorage.setItem("noor-pre", "1"); } catch (e) {}
      setTimeout(() => { pre.remove(); document.documentElement.classList.add("preloaded"); }, 1000);
    };
    if (reduce || document.documentElement.classList.contains("preloaded")) {
      pre.remove(); document.documentElement.classList.add("preloaded");
      try { sessionStorage.setItem("noor-pre", "1"); } catch (e) {}
    } else {
      const bar = $(".preloader__bar i", pre);
      const pct = $(".preloader__pct", pre);
      const t0 = performance.now(), dur = 1150;
      let finished = false;
      const finish = () => { if (finished) return; finished = true; done(); };
      const tick = (n) => {
        const p = Math.min((n - t0) / dur, 1);
        const eased = 1 - Math.pow(1 - p, 2);
        if (bar) bar.style.transform = `scaleX(${eased})`;
        if (pct) pct.textContent = Math.round(eased * 100);
        if (p < 1) requestAnimationFrame(tick); else finish();
      };
      requestAnimationFrame(tick);
      setTimeout(finish, 2600); // safety net if rAF stalls (backgrounded tab, etc.)
    }
  }

  /* ---------- Custom cursor -------------------------------------------------- */
  if (fine && !reduce) {
    const dot = document.createElement("div"); dot.className = "cursor-dot";
    const ring = document.createElement("div"); ring.className = "cursor-ring";
    ring.innerHTML = '<span class="cursor-ring__t"></span>';
    const label = ring.firstChild;
    document.body.append(dot, ring);
    document.body.classList.add("has-cursor");
    let mx = innerWidth / 2, my = innerHeight / 2, rx = mx, ry = my;
    addEventListener("mousemove", (e) => {
      mx = e.clientX; my = e.clientY;
      dot.style.transform = `translate(${mx}px, ${my}px)`;
    }, { passive: true });
    const loop = () => { rx += (mx - rx) * 0.18; ry += (my - ry) * 0.18; ring.style.transform = `translate(${rx}px, ${ry}px)`; requestAnimationFrame(loop); };
    loop();
    const setState = (e) => {
      const w = e.target.closest(".work");
      const c = e.target.closest(".icard");
      const a = e.target.closest("a, button, .nav__link, input, select, textarea, .switch, .tab, .idx");
      ring.classList.remove("grow", "label");
      if (w) { ring.classList.add("grow", "label"); label.textContent = (w.dataset.video !== undefined || w.dataset.embed) ? "Play" : "View"; }
      else if (c) { ring.classList.add("grow", "label"); label.textContent = "Open"; }
      else if (a) { ring.classList.add("grow"); label.textContent = ""; }
      else { label.textContent = ""; }
    };
    addEventListener("mouseover", setState, { passive: true });
    addEventListener("mousedown", () => ring.classList.add("down"));
    addEventListener("mouseup", () => ring.classList.remove("down"));
    document.addEventListener("mouseleave", () => { dot.style.opacity = "0"; ring.style.opacity = "0"; });
    document.addEventListener("mouseenter", () => { dot.style.opacity = "1"; ring.style.opacity = "1"; });
  }

  /* ---------- Hero / category parallax on scroll ---------------------------- */
  if (!reduce) {
    const heroBg = $(".hero__bg");
    const heroInner = $(".hero__inner");
    const catBg = $(".cathero__bg");
    let ticking = false;
    const onScroll = () => {
      if (ticking) return; ticking = true;
      requestAnimationFrame(() => {
        const y = window.scrollY;
        if (heroBg && y < innerHeight * 1.2) heroBg.style.transform = `translate3d(0, ${y * 0.22}px, 0)`;
        if (heroInner && y < innerHeight * 1.2) { heroInner.style.transform = `translate3d(0, ${y * -0.06}px, 0)`; heroInner.style.opacity = String(Math.max(0, 1 - y / (innerHeight * 0.85))); }
        if (catBg && y < innerHeight * 1.2) catBg.style.transform = `translate3d(0, ${y * 0.18}px, 0)`;
        ticking = false;
      });
    };
    addEventListener("scroll", onScroll, { passive: true });
  }

  /* ---------- Index card 3D tilt + inner image parallax --------------------- */
  function initTilt() {
    if (!fine || reduce) return;
    $$(".icard").forEach((card) => {
      if (card.dataset.tilt) return;
      card.dataset.tilt = "1";
      const img = $(".icard__media img", card);
      card.addEventListener("mousemove", (e) => {
        const r = card.getBoundingClientRect();
        const px = (e.clientX - r.left) / r.width - 0.5;
        const py = (e.clientY - r.top) / r.height - 0.5;
        card.style.transition = "none";
        card.style.transform = `perspective(900px) rotateX(${py * -4}deg) rotateY(${px * 4}deg)`;
        if (img) img.style.transform = `scale(1.09) translate(${px * -14}px, ${py * -14}px)`;
      });
      card.addEventListener("mouseleave", () => {
        card.style.transition = "transform 0.6s var(--ease)";
        card.style.transform = "";
        if (img) img.style.transform = "";
      });
    });
  }
  initTilt();
  // cards are re-rendered on language switch → re-attach
  window.addEventListener("langchange", () => setTimeout(initTilt, 60));

  /* ---------- Marquee scroll-velocity skew ---------------------------------- */
  const marq = $(".marquee");
  if (marq && !reduce) {
    let last = window.scrollY, resetT = null;
    addEventListener("scroll", () => {
      const v = window.scrollY - last; last = window.scrollY;
      const sk = Math.max(-7, Math.min(7, v * 0.35));
      marq.style.transform = `skewX(${sk}deg)`;
      clearTimeout(resetT);
      resetT = setTimeout(() => { marq.style.transform = "skewX(0deg)"; }, 130);
    }, { passive: true });
  }
})();
