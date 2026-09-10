/* =============================================================================
   BARDEES image pipeline — the "photoshop"
   Grades the client's raw photos into one consistent champagne-luxury look,
   makes face-aware art-directed crops, and exports responsive WebP + JPG.
   Optional AI background-removal cutouts when run with DO_CUTOUT=1.

   Usage:
     node scripts/images.js               # grade + crop + optimize all
     node scripts/images.js sample NAME   # process a single photo → scratchpad preview
     DO_CUTOUT=1 node scripts/images.js    # also produce AI cutouts (slow, needs model)
   ============================================================================= */
const sharp = require("sharp");
const fs = require("fs");
const path = require("path");

const ROOT = path.resolve(__dirname, "..");
const SRC = path.join(ROOT, "CLient Data");
const OUT = path.join(ROOT, "assets", "img");
const MAP = JSON.parse(fs.readFileSync(path.join(__dirname, "imgmap.json"), "utf8"));
fs.mkdirSync(OUT, { recursive: true });

const POS = { attention: sharp.strategy.attention, entropy: sharp.strategy.entropy, centre: "centre" };

/* The luxury grade: gentle warm contrast + clarity, applied uniformly so the
   mismatched source shoots read as one editorial story. Returns a lossless
   full-res buffer we then crop from. */
async function grade(srcPath) {
  return sharp(srcPath)
    .rotate() // honour EXIF orientation
    .modulate({ brightness: 1.035, saturation: 1.06 }) // lift + a touch more colour
    .linear([1.07, 1.045, 1.0], [-8, -7, -3]) // per-channel: warm highlights, add contrast
    .gamma(1.02)
    .sharpen({ sigma: 0.8, m1: 0.5, m2: 0.4 })
    .png()
    .toBuffer();
}

async function emit(buf, name, w, h, position) {
  const base = sharp(buf).resize(w, h, { fit: "cover", position: position });
  await base.clone().webp({ quality: 78 }).toFile(path.join(OUT, `${name}.webp`));
  await base.clone().jpeg({ quality: 82, mozjpeg: true }).toFile(path.join(OUT, `${name}.jpg`));
}

const RENDITIONS = [
  { suf: "portrait", w: 1000, h: 1250 }, // 4:5 — cards / about / gallery
  { suf: "sq", w: 800, h: 800 }, // 1:1 — grid / discipline cards
];
const HERO_RENDITIONS = [
  { suf: "wide", w: 1920, h: 1080 }, // 16:9 — desktop hero / banner
  { suf: "hero", w: 1080, h: 1440 }, // 3:4 — mobile hero
];

async function processPhoto(p, sampleOut) {
  const srcPath = path.join(SRC, p.src);
  if (!fs.existsSync(srcPath)) { console.warn("MISSING src:", p.src); return null; }
  const buf = await grade(srcPath);
  const pos = POS[p.focus] || sharp.strategy.attention;
  const rends = p.hero ? RENDITIONS.concat(HERO_RENDITIONS) : RENDITIONS;
  if (sampleOut) {
    // preview only: one portrait + one wide to scratchpad
    await sharp(buf).resize(700, 875, { fit: "cover", position: pos }).jpeg({ quality: 82 }).toFile(path.join(sampleOut, `${p.name}-portrait.jpg`));
    await sharp(buf).resize(1200, 675, { fit: "cover", position: pos }).jpeg({ quality: 82 }).toFile(path.join(sampleOut, `${p.name}-wide.jpg`));
    return p.name;
  }
  const heroPos = POS[p.heroFocus] || "north"; // full-length hero: keep head + upper body, don't zoom to face
  for (const r of rends) {
    const isHero = r.suf === "wide" || r.suf === "hero";
    await emit(buf, `${p.name}-${r.suf}`, r.w, r.h, isHero ? heroPos : pos);
  }
  console.log("  graded", p.name, p.hero ? "(+hero)" : "");
  return p.name;
}

/* Extract a client logo into a clean single-colour SILHOUETTE on transparent
   PNG. The white mark is only a shape — the site paints it via CSS mask in a
   theme-aware colour (muted → gold on hover), so it reads as one unified set
   across dark/light regardless of the messy source backgrounds. */
async function processLogo(l) {
  const srcPath = path.join(SRC, l.src);
  if (!fs.existsSync(srcPath)) { console.warn("MISSING logo:", l.src); return; }
  const meta0 = await sharp(srcPath).rotate().metadata();
  let s = sharp(srcPath).rotate();
  if (l.crop) {
    const [cl, ct, cw, ch] = l.crop;
    s = s.extract({ left: Math.round(cl * meta0.width), top: Math.round(ct * meta0.height), width: Math.round(cw * meta0.width), height: Math.round(ch * meta0.height) });
  }
  let g = s.resize(820, 820, { fit: "inside", withoutEnlargement: true }).greyscale().normalise();
  if (l.invert) g = g.negate(); // dark mark on light bg → make the mark the bright channel
  const slope = l.slope || 11, tc = l.tc != null ? l.tc : 180; // soft alpha ramp centred on tc
  const { data, info } = await g.linear(slope, 128 - slope * tc).raw().toBuffer({ resolveWithObject: true });
  const white = sharp({ create: { width: info.width, height: info.height, channels: 3, background: "#ffffff" } })
    .joinChannel(data, { raw: { width: info.width, height: info.height, channels: 1 } })
    .png();
  const sil = await white.trim({ threshold: 25 }).toBuffer(); // drop transparent margins
  await sharp(sil).resize(640, 320, { fit: "inside", withoutEnlargement: true }).png().toFile(path.join(OUT, `${l.name}.png`));
  await sharp(sil).resize(640, 320, { fit: "inside", withoutEnlargement: true }).webp({ quality: 90 }).toFile(path.join(OUT, `${l.name}.webp`));
  console.log("  logo (silhouette)", l.name);
}

async function cutout(p) {
  let removeBackground;
  try { ({ removeBackground } = require("@imgly/background-removal-node")); }
  catch (e) { console.warn("  (AI matting lib unavailable — skipping cutout for " + p.name + ")"); return; }
  const srcPath = path.join(SRC, p.src);
  try {
    const blob = await removeBackground(srcPath);
    const ab = await blob.arrayBuffer();
    const cut = Buffer.from(ab);
    // trim transparent margins, cap size, save PNG (transparent)
    await sharp(cut).trim().resize(1200, 1600, { fit: "inside", withoutEnlargement: true }).png().toFile(path.join(OUT, `${p.name}-cutout.png`));
    console.log("  ✂ cutout", p.name);
  } catch (e) {
    console.warn("  cutout FAILED for " + p.name + ":", e.message);
  }
}

(async () => {
  const mode = process.argv[2];
  if (mode === "sample") {
    const SP = process.env.SP || ".";
    const name = process.argv[3];
    const p = MAP.photos.find((x) => x.name === name) || MAP.photos[0];
    console.log("sample →", p.name);
    await processPhoto(p, SP);
    return;
  }
  console.log("Grading photos →", OUT);
  const manifest = { photos: [], logos: [], cutouts: [] };
  for (const p of MAP.photos) {
    const n = await processPhoto(p);
    if (n) manifest.photos.push({ name: n, hero: !!p.hero, cutout: !!p.cutout });
  }
  console.log("Logos →");
  for (const l of MAP.logos) { await processLogo(l); manifest.logos.push(l.name); }
  if (process.env.DO_CUTOUT === "1") {
    console.log("AI cutouts →");
    for (const p of MAP.photos.filter((x) => x.cutout)) { await cutout(p); manifest.cutouts.push(p.name); }
  }
  fs.writeFileSync(path.join(OUT, "manifest.json"), JSON.stringify(manifest, null, 2));
  console.log("Done. Manifest written.");
})();
