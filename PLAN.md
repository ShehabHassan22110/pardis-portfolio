# BARDEES REFAAT — Master Build Plan (Bootstrap / Poseify base)

**Senior-developer plan, A → Z.** Rebuild the portfolio on the **Poseify Bootstrap template**,
rethemed to **champagne-gold on dark luxury**, bilingual **EN/AR (RTL)** with **dark/light**
modes, driven by the client brief (`CLient Data/text data.txt`) and the client's **own photos**
after a full image-processing ("photoshop") pipeline. Modern, editorial, fast, accessible.

Locked decisions: Accent = **champagne gold #C6A15B** on near-black `#0D0D0D` · Images = **grade +
crop + optimize + attempt AI background-removal** · Default theme = **dark** (toggle to light).

---

## 0. Objectives & principles
- **Reuse Poseify** layout/components (Bootstrap 5, jQuery, WOW.js, Owl Carousel, Lightbox) — do
  not reinvent; re-skin and extend.
- **One source of truth** for content: `assets/js/data.js` (already Bardees-accurate) → all
  dynamic sections render from it; **EN + AR** fields on every item.
- **Client's real photos only**, unified into one luxury look. No stock, no invented stats/press.
- **Modern & premium**: serif display + clean sans, gold hairlines, generous space, tasteful motion.
- **Accessible & responsive** (WCAG AA contrast, keyboard, reduced-motion), **fast** (WebP, lazy).

## 1. Teardown — remove Tailwind
- Delete `tailwind.config.js`, `css/app.css`, `css/tailwind.css`, Tailwind dep in `package.json`.
- Remove the Tailwind-class HTML I generated (index/about/services/category/contact bodies) — they
  will be replaced by Bootstrap markup. Keep & adapt `data.js` + `i18n.js` content.
- Keep `sharp` (image pipeline). `node_modules` stays git-ignored.

## 2. Tech stack & structure
- **Bootstrap 5.3.3** (upgrade from Poseify's 5.0 → native `data-bs-theme` dark mode + RTL build),
  jQuery 3.4, WOW.js + animate.css, Owl Carousel, Lightbox, Waypoints, Easing (copied from Poseify `lib/`).
- No bundler required (static). Optional tiny `npm` scripts only for the **image pipeline** & local serve.
```
/ (repo root)
├─ index.html  about.html  services.html  portfolio.html  category.html  contact.html  404.html
├─ assets/
│  ├─ css/  bootstrap.min.css  bootstrap.rtl.min.css  style.css (Poseify)  theme.css (our gold+dark/light)
│  ├─ js/   vendor…  data.js  i18n.js  app.js
│  ├─ img/  (processed responsive WebP/JPG + cutouts)  brand/  video/
│  └─ lib/  (wow, owlcarousel, lightbox, waypoints, easing, animate)
├─ scripts/ images.js   (sharp pipeline)   imgmap.json (raw→named mapping)
├─ CLient Data/ (git-ignored source media)
└─ PLAN.md
```

## 3. Design system (retheme Poseify)
- **Palette (semantic CSS vars, themed):**
  - Dark: `--bg:#0D0D0D  --surface:#141414  --text:#F3EFE8  --muted:#A0A0A0  --line:rgba(198,161,91,.25)`
  - Light: `--bg:#F6F2EA  --surface:#FFFFFF  --text:#1A1712  --muted:#6B6055  --line:rgba(198,161,91,.35)`
  - Accent both: `--accent:#C6A15B  --accent-2:#E7CFA3  --accent-deep:#A07A55`
  - Map Bootstrap: override `--bs-primary/-rgb`, `.btn-primary`, `.text-primary`, `.bg-primary`, links.
- **Typography:** display serif **Cormorant Garamond** (or Playfair) + body **Jost/Work Sans** +
  Arabic **Tajawal** (body) / **El Messiri** (display). Gold thin rules keep Poseify's `.title` motif.
- **Components restyled:** pill buttons (gold), navbar (transparent→dark-blur on scroll), service
  pill-cards, team hover-reveal → repurposed for **discipline/look cards**, testimonial carousel,
  animated footer (swap footer-bg tint to gold-on-dark).

## 4. Image pipeline — the "photoshop"  (`scripts/images.js`, sharp + AI matting)
Process the client's raw photos in `CLient Data/` into a unified, modern, web-optimized set.
1. **Ingest & map** raw files → semantic names via `scripts/imgmap.json` (couture 01–04; hand:
   Calvin Klein / Coach / Roberto Cavalli / Ferragamo; fashion: floral / blazer-seated /
   blazer-stand / pink / olive; beauty 01–03; ambassador display 01–02; brand logos MecroLine,
   Alhomaidhi). Pick the **highest-res source** for hero (e.g. 2774×4160 portrait).
2. **Normalize:** auto-orient, strip EXIF, convert to linear working space.
3. **Grade to one luxury look:** auto-levels (`normalise`), gentle contrast (`linear`), warm
   champagne tint + saturation/brightness (`modulate`, `tint`), `gamma` — a single consistent recipe
   so mismatched shoots read as one editorial story. Subtle per-image exposure correction.
4. **Retouch (automated):** light denoise (`median`), `sharpen`, optional vignette + bottom
   gradient (composite) for text legibility over photos.
5. **Face-aware smart crops** (`resize fit:cover, position: attention`) to the ratios each slot needs:
   hero 16:9 & 4:5, portrait 4:5, square 1:1 (grid), wide banners.
6. **AI background removal** (`@imgly/background-removal-node`) on select shots → transparent PNG
   cutouts for **floating-figure hero / about** treatments; composite over gold-dark gradients.
   *(Best-effort: results reviewed per image; any weak mattes flagged for a manual pass.)*
7. **Responsive export:** each image → WebP (q78) + JPG fallback (q82) at widths {480,768,1200,1920};
   write an `assets/img/manifest.json`. Everything `loading="lazy"` + width/height to avoid CLS.
8. **Video:** reuse the two short MP4s (`assets/video/`), generate poster stills; the 171 MB raw
   is excluded (no ffmpeg/transcode).

## 5. Content & IA mapping (from `text data.txt`)
- **Cover/Hero:** BARDEES REFAAT · "Saudi Market Model & Brand Ambassador" · "Representing Brands.
  Connecting Markets." · Travel·Tourism·Hospitality·Lifestyle·Luxury · Based in KSA / GCC.
- **About Bardees** (AR + EN) · **Why Bardees for the Saudi Market** (6 points) · **Services** (8) ·
  **7 disciplines** (Beauty&Fashion, Commercial, Brand Ambassador, Food, Medical, Acting&TV,
  Corporate — 3 live, 4 "coming soon/enquire") · **Digital presence** (platforms + stats
  placeholders) · **Contact** (WhatsApp/email/IG/TikTok/Snapchat). Note captured in FAQ/Process:
  **wardrobe is styled to suit each ad**.

## 6. Pages & sections (Poseify + NEW)
- **index.html:** Spinner → Navbar → **Hero carousel** (client photos, gold masthead) → **Trusted-by**
  (brand logos marquee) → **About** (portrait + lead) → **Why Saudi** (6 cards) → **Disciplines index**
  (7, team-card hover style) → **Selected Work** (lightbox gallery) → **In Motion** (reels) →
  **Digital Presence** (stats) → **Services** (8) → **Process** (4 steps) → **Testimonials** (carousel;
  only if real — else omit) → **FAQ** → **CTA** → **Footer**.
- **about.html:** full About + Why + facts + presence + CTA.
- **services.html:** 8 services (pill cards) + process + FAQ + CTA.
- **portfolio.html:** full filterable gallery across disciplines (lightbox).
- **category.html?cat=slug:** per-discipline hero + gallery or "coming soon/enquire" state + prev/next.
- **contact.html:** contact details + booking form (client-side validated) + socials + map/QR.
- **404.html:** rebranded.

## 7. Internationalization (EN/AR + RTL)
- `assets/js/i18n.js`: chrome dictionary (EN/AR) + `data-i18n`; dynamic content uses `…Ar` fields.
- On Arabic: set `html[lang=ar][dir=rtl]`, **swap `bootstrap.min.css` → `bootstrap.rtl.min.css`**,
  switch fonts to Tajawal/El Messiri, mirror icons/arrows. Persist in `localStorage(bardees-lang)`.

## 8. Theming (dark/light)
- Semantic vars flipped by `html[data-theme]` + Bootstrap `data-bs-theme`. Toggle in navbar; persist
  `localStorage(bardees-theme)`; no-flash inline script in `<head>`. Both themes AA-contrast checked.

## 9. Interactions / JS (`assets/js/app.js`)
- Keep Poseify: spinner, WOW reveals, sticky navbar, back-to-top, Owl testimonials.
- Add: language toggle (+re-render), theme toggle, hero carousel wiring, portfolio filter, Lightbox
  for gallery + video, FAQ accordion, mobile menu, contact-form validation/success, copy/share.

## 10. Accessibility · Performance · SEO
- Semantic landmarks, alt text (localised), focus-visible, keyboard for gallery/menu, reduced-motion.
- WebP + responsive `srcset`, lazy-load, preconnect fonts, deferred JS, width/height on media.
- Per-page `<title>`/description, Open Graph (+ generate a branded 1200×630 OG from a graded photo),
  favicon, `lang`/`dir`, JSON-LD `Person` schema.

## 11. QA & verification
- Headless-Chrome screenshots (existing tooling) for every page × {EN, AR} × {dark, light} ×
  {desktop, mobile}. Check: contrast, RTL mirroring, no overflow, image crops keep faces, links 200,
  no leftover pink/Poseify/placeholder text, forms behave. Fix, re-shoot.

## 12. Deployment / handoff
- Pure static → deploy to Vercel/Netlify/GitHub Pages. `README` with: how to run the image pipeline,
  where to swap real contact/stats, how to add media to "coming soon" categories.

## 13. Execution order (phases)
1. **Teardown** Tailwind + scaffold Bootstrap project (copy `lib/`, add BS 5.3 + RTL).
2. **Image pipeline** — build `scripts/images.js`, process all client photos, review mattes.
3. **Design system** — `theme.css` (gold + dark/light) + fonts + Bootstrap var overrides.
4. **Data/i18n** — port `data.js` + `i18n.js` to the new markup hooks (EN/AR complete).
5. **index.html** — build all sections; verify EN/AR/dark/light/mobile; iterate.
6. **Remaining pages** — about, services, portfolio, category, contact, 404.
7. **JS** — app.js interactions (toggles, filter, lightbox, form).
8. **QA sweep** — screenshots matrix, fixes.
9. **Docs + handoff** — README, PLAN updates, memory update.

## 14. Risks & mitigations
- **AI matting quality** varies per photo → review each; fall back to graded full-frame where weak;
  flag any needing manual cutout.
- **BS 5.0→5.3 upgrade** vs Poseify style.css → style.css uses stable vars/utilities; spot-check.
- **RTL edge cases** → use logical props + RTL Bootstrap; screenshot AR explicitly.
- **Real contact/stats/media** are client-supplied → clearly-marked placeholders, README checklist.

## 15. Deliverables
- Rethemed Bootstrap site (6+ pages), EN/AR + dark/light, built from client text.
- Processed, unified, responsive client imagery + reusable `scripts/images.js` pipeline.
- Verified via screenshot matrix; documented; ready to deploy (placeholders flagged).

### ⚑ Client still to provide (won't block build)
Real WhatsApp/email/IG/TikTok/Snapchat · real audience numbers · media for Food/Medical/Acting/Corporate.
