# BARDEES REFAAT — Portfolio Website

A bilingual (English / العربية), dark & light, luxury portfolio for **BARDEES REFAAT** —
Saudi Market Model & Brand Ambassador. Built on **Bootstrap 5.3** (Poseify base), rethemed
to a champagne-gold-on-dark identity. Static frontend — no server required to view.

## Stack
- **Bootstrap 5.3** (+ RTL build) · vanilla JS · Bootstrap Icons · Google Fonts
  (Cormorant Garamond · Jost · El Messiri · Tajawal).
- **sharp** (Node) image pipeline for processing the client's photos.
- No bundler / build step for the site itself — open the HTML or serve the folder.

## Run locally
```bash
npm install           # once (for the image pipeline only)
npm run serve         # → http://localhost:8099
```
Or open `index.html` directly in a browser.

## Pages
`index.html` · `about.html` · `services.html` · `portfolio.html` (filterable gallery) ·
`category.html?cat=<slug>` (per-discipline) · `contact.html` (booking form) · `404.html`.

## Content — edit in one place
All copy, disciplines, services and image mappings live in **`assets/js/data.js`**
(English + `…Ar` Arabic fields). UI chrome strings live in **`assets/js/i18n.js`**.
The whole site re-renders from these — no need to touch the HTML for content changes.

## Theme & language
- **Dark** is the default; a navbar toggle switches to **light**. Choice persists (localStorage).
- **EN ⇄ AR** toggle flips `dir`, swaps to the Bootstrap RTL stylesheet, and re-renders content.

## Images — the "photoshop" pipeline
Client photos in `CLient Data/` are processed by **`scripts/images.js`** (mapping in
`scripts/imgmap.json`): unified champagne color-grade, face-aware crops, sharpening, and
responsive **WebP + JPG** export (portrait/square + hero renditions). Re-run anytime:
```bash
npm run images                 # grade + crop + optimize all
DO_CUTOUT=1 npm run images     # also attempt AI background-removal cutouts (needs @imgly)
```
Templates are **cutout-ready**: drop `assets/img/<name>-cutout.png` (a proper Photoshop/AI
cutout) and it can be used for floating-figure treatments.

## ⚑ Before going live — replace placeholders (all in `assets/js/data.js`)
- `PROFILE.email`, `PROFILE.phone`, `PROFILE.whatsapp` — real contact details.
- `SOCIAL[].url` — real Instagram / TikTok / Snapchat links (currently `#`).
- `STATS` values — real Followers / Monthly Reach / % Saudi (currently `XXK+` / `XX%`).
- Media for the 4 "coming soon" disciplines (Food, Medical, Acting & TV, Corporate) — add
  works to those `CATEGORIES` entries and set `status: "live"`.
- Optional: a branded 1200×630 `assets/brand/bardees-og.png` for social sharing.

## Deploy
Any static host — Vercel, Netlify, GitHub Pages, or plain hosting. Upload the repo root
(everything except `node_modules/`, `CLient Data/`, `template/`).

## Notes
- The contact form is a **frontend preview** (validates client-side, shows a success state) —
  wire it to a backend/service (Formspree, a serverless function, etc.) to receive submissions.
- `template/` holds the original Poseify template for reference; not used at runtime.
