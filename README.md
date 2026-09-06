# NOOR — Talent Portfolio (Frontend)

A luxury, editorial portfolio & booking site for a professional model, brand
ambassador and corporate representative. Built as a **static frontend** (HTML +
CSS + vanilla JS) — no build step. Double-click `index.html` to view.

## Handoff / deploy
It's fully static — no server or build. To publish, drop the whole folder on any
static host (Netlify: drag-and-drop the folder · Vercel · GitHub Pages · Cloudflare
Pages · any web host's `public_html`). Keep the file/folder structure intact.

### Before you send it live — replace the placeholders
1. **Name** — `PROFILE` in `js/data.js` (name, full name, role, base, email, phone, Instagram, facts)
   and the visible "Noor" wordmark in the HTML nav/footers (search & replace).
2. **Photos & videos** — the `img`/`video` URLs in `js/data.js` (currently Unsplash + sample clips)
   and the hero, about, showreel and Instagram images in `index.html`. Swap for her real shoots.
3. **WhatsApp number** — `WA_NUMBER` in `js/main.js` (digits only, incl. country code).
4. **Instagram/social links** — the `href="#"` on the Instagram section + footer.
5. **Press / testimonials / reach numbers** — real publications, quotes and follower stats.
6. **Contact email/phone** — in `booking.html` and footers.
7. Optional: connect the **booking form** and **dashboard** to a backend (currently mock).

## Design direction — "Oud Noir"
Warm cinematic near-black editorial. Chosen deliberately to avoid the generic
"cream + terracotta serif" look and read like a couture invitation.

| Token | Value | Use |
|-------|-------|-----|
| `--noir` | `#16110D` | Base background (warm oud-black) |
| `--porcelain` | `#F3ECE0` | Primary text (pearl) |
| `--champagne` | `#C9A17A` | Restrained gold accent / hairlines |
| `--blush` | `#C98F7E` | Feminine secondary accent (italic display) |
| `--mist` | `#B3A794` | Muted body text |

**Type:** Bodoni Moda (display / masthead) · Jost (body & labels) · El Messiri (Arabic).
**Signature element:** *The Index* — the 7 disciplines as an editorial **image-card grid**
(a large feature tile, two stacked, a row of three, and a wide banner) with cover photos,
outlined numbers and hover zoom. Rendered from `js/data.js` and fully RTL-mirrored.

## Pages
- `index.html` — preloader, hero, trusted-by, marquee, **The Index** (card grid), selected work, **showreel**, about + facts, stats, **press ("as featured in")**, testimonials, **Instagram + reach**, process, ways to collaborate, **FAQ**, booking CTA, footer.
- `category.html?cat=<slug>` — per-discipline gallery (driven by `js/data.js`). Slugs: `beauty-fashion`, `commercial`, `ambassador`, `food`, `medical`, `acting-tv`, `corporate`.
- `booking.html` — booking / collaboration request form (front-end only for now; `?type=<Discipline>` pre-selects the dropdown). Premium form UI: a panel card with boxed, icon-led fields (`.field__control` + `.field__icon`), hover + focus-glow states, and a check-mark success screen. Shared `.field` styles also power the dashboard login + settings.
- `dashboard.html` — **private studio dashboard** (admin). Login gate (demo: any credentials), Overview (KPIs, charts, recent requests), Bookings (filter + search + detail drawer), Portfolio (manage media per discipline), Categories (show/hide), Messages, Settings. Frontend + mock data in `js/dashboard-data.js`; reached via the discreet "Studio login" link in the site footer.

## Dark / light theme
A warm **light "Atelier Daylight"** palette complements the dark "Oud Noir". Toggle via the
sun/moon button in the nav (and dashboard sidebar). The choice is saved to `localStorage`
and respects the visitor's system preference on first visit. A no-flash inline script in each
`<head>` sets the theme before paint.

## Photos + videos
Video works (`type: "video"` in `js/data.js`) play in a modal. Each carries a `video` (mp4)
or `embed` (YouTube ID) field — currently sample clips; replace with the real ad films
(e.g. the National Day commercial, marble ambassador film). Photos open in a lightbox.

## Motion (`js/motion.js` + `js/main.js`)
- **Intro preloader** (index, once per session): Bodoni wordmark, a fill line + counter, then a
  curtain lift. Skipped via `sessionStorage`; `<noscript>` + a 2.6s safety timeout guarantee it lifts.
- **Custom cursor** (desktop): a lagging ring that grows and shows *View / Play / Open* over media.
- **Parallax**: hero background/content and category hero move on scroll.
- **Index card 3D tilt** + inner-image parallax on hover.
- **Marquee** skews with scroll velocity.
- Plus: scroll-progress bar, hero Ken-Burns, scroll-triggered fades, image "curtain" reveals,
  magnetic buttons, stat/KPI count-ups, animated charts.
- **All** of the above are disabled under `prefers-reduced-motion`, and cursor/tilt are desktop-only.

## Bilingual — English / العربية (RTL)
Language toggle (EN ⇄ عربي) sits in the nav; choice saved to `localStorage`, applied before
paint. Switching sets `dir="rtl"`, swaps every string, and re-renders the JS-built sections.
- All UI strings live in `js/i18n.js` (`I18N.en` / `I18N.ar`). Add `data-i18n="key"` to a new
  element (sets innerHTML) or `data-i18n-ph="key"` (placeholder).
- Category content (tagline / description / sub-disciplines) has Arabic fields
  (`arTagline`, `arDesc`, `arSub`) in `js/data.js`.
- Arabic type: **El Messiri** (display) + **Tajawal** (body), wired as font fallbacks so Latin
  stays Bodoni/Jost and Arabic glyphs pick up the Arabic faces automatically.

## New sections
- **Trusted by** — brand wordmark strip.
- **Testimonials** — three brand quotes.
- **How a collaboration works** — 4-step process (choose → brief → proposal → create).
The **Index** and **Ways to collaborate** grids now render from `js/data.js` (single source),
so they stay in sync and translate automatically.

## WhatsApp
A floating WhatsApp button (bottom corner, RTL-aware) with a pulse ring and expand-on-hover
label. Set the number in `js/main.js` → `WA_NUMBER` (digits only, incl. country code); the
prefilled message is the `wa.msg` i18n string.

## The 7 disciplines (from the client brief)
1. Beauty & Fashion — Hand/Watches, Makeup, Hair, Fashion, Abaya
2. Commercial — Advertising, Brand Campaigns, National Day films
3. Brand Ambassador — incl. Gulf Marble House
4. Food & Beverage — Desserts, Restaurants, Cafés
5. Medical & Wellness — Clinics, Medical Centers, Healthcare
6. Acting & TV — TV Guest, Series, Theatre, Acting
7. Corporate Representative — Meetings, Events, Exhibitions, Activations (quoted per assignment)

## What to replace before launch
1. **Name & contact** — edit `PROFILE` at the top of `js/data.js` (name, email, phone, Instagram, facts).
   The visible wordmark "Noor." also appears in the HTML nav/footer — search & replace.
2. **Photos & videos** — every work item in `js/data.js` has an `img` URL. Swap the
   placeholder (Unsplash) links for the model's real shoots. If an image fails to load,
   an elegant duotone plate is shown automatically, so the layout never breaks.
   Mark a video item with `type: "video"` to get the "Film" badge.
3. **Hero & portrait images** — in `index.html` (`.hero__bg` and `.about__portrait`).

## Wiring the backend later
- The booking form (`#bookingForm` in `booking.html`) currently shows a success state
  on submit. To connect it, POST the form fields in `js/main.js` → "Booking form" block.
- The site is fully static, so it deploys to any host (Netlify, Vercel, GitHub Pages, S3).

## Quality baseline
Responsive to mobile · keyboard focus states · `prefers-reduced-motion` respected ·
image fallbacks · semantic headings.
