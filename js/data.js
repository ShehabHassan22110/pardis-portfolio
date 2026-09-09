/* =============================================================================
   PORTFOLIO DATA  —  single source of truth  (PARDIS)
   Everything the site shows comes from here. To update the portfolio, edit
   this file only — the whole site re-renders from it.

   ⚑ TO-EDIT LATER (client to confirm):
     • PROFILE.nameAr  — Arabic spelling of "Pardis" (placeholder below)
     • PROFILE.email / phone / whatsapp
     • SOCIAL[].url    — real Instagram / TikTok profile links
   All photography below is the client's real work. Brands shown are real
   collaborations / campaign product houses. No invented clients.
   ============================================================================= */

const IMG = "assets/img/";
const VID = "assets/video/";

const PROFILE = {
  name: "PARDIS",                 // public brand name
  nameFull: "Pardis",             // ← add full name if desired
  nameAr: "بارديس",               // ⚑ phonetic Arabic — client to confirm spelling
  role: "Model · Brand Ambassador · Corporate Representative",
  roleAr: "عارضة أزياء · سفيرة علامات تجارية · ممثلة رسمية للشركات",
  base: "Riyadh, Kingdom of Saudi Arabia",
  // ⚑ Editable contact placeholders — replace with the real details:
  email: "hello@pardis.studio",
  phone: "+966 5X XXX XXXX",
  whatsapp: "966500000000",       // digits only, incl. country code
  // Portfolio facts brands ask for — only verifiable ones are shown
  facts: [
    { label: "Based in", value: "Riyadh, KSA" },
    { label: "Languages", value: "Arabic · English" },
    { label: "Focus", value: "Fashion · Beauty · Watches" },
    { label: "Availability", value: "GCC & International" },
  ],
  // Honest, verifiable figures only (no inflated counts)
  stats: [
    { value: "5", label: "Disciplines" },
    { value: "4", label: "Watch houses" },
    { value: "2", label: "Languages" },
    { value: "GCC", label: "Available" },
  ],
};

/* Social links — labels only until real URLs are provided. */
const SOCIAL = [
  { label: "Instagram", handle: "@pardis", url: "#" }, // ⚑ add real profile URL
  { label: "TikTok",    handle: "@pardis", url: "#" }, // ⚑ add real profile URL
  { label: "Email",     handle: PROFILE.email, url: "mailto:" + PROFILE.email },
];

/* Real brands / houses the work features. Rendered in "Trusted by". */
const BRANDS = [
  { name: "Calvin Klein" },
  { name: "Coach", em: "New York" },
  { name: "Roberto Cavalli", em: "by Franck Muller" },
  { name: "Ferragamo" },
  { name: "Alhomaidhi", em: "Watches" },
  { name: "MecroLine" },
  { name: "Volux" },
];

/* Each category: number, slug, title, arabic label, tagline, description,
   sub-disciplines, an accent gradient (fallback plates), a cover and works.
   Every image below is the client's real photography. */
const CATEGORIES = [
  {
    n: "01",
    slug: "beauty",
    title: "Beauty & Glam",
    ar: "الجمال والإطلالات",
    tagline: "The face, in its clearest light.",
    desc: "Close-up beauty and glam — makeup, skin and hair, shot for campaigns and editorial. The reassuring, expressive face a beauty brand builds around.",
    sub: ["Makeup", "Beauty Campaigns", "Skin & Glow", "Hair & Accessories"],
    arTagline: "الوجه، في أوضح ضوء.",
    arDesc: "جمال مقرّب وإطلالات — مكياج وبشرة وشعر، للحملات والأعمال التحريرية. الوجه المعبّر الذي تبني عليه علامات الجمال.",
    arSub: ["مكياج", "حملات جمال", "بشرة وإشراق", "شعر وإكسسوار"],
    grad: ["#3A2A2A", "#C9A17A"],
    cover: IMG + "beauty-glam-01.jpg",
    pos: "50% 30%",   // crop focus — keep the face in frame
    works: [
      { title: "Gold Hour", brand: "Beauty Editorial", tag: "Makeup · Glam", img: IMG + "beauty-glam-01.jpg" },
      { title: "Soft Focus", brand: "Beauty Portrait", tag: "Beauty", img: IMG + "beauty-glam-03.jpg" },
      { title: "Poise", brand: "Beauty Story", tag: "Skin · Glow", img: IMG + "beauty-glam-02.jpg" },
      { title: "Rosette", brand: "Occasion Beauty", tag: "Hair · Accessories", img: IMG + "beauty-pink.jpg" },
    ],
  },
  {
    n: "02",
    slug: "fashion",
    title: "Fashion & Editorial",
    ar: "الأزياء والتحرير",
    tagline: "Tailoring, print and attitude.",
    desc: "Studio fashion and editorial — suiting, resort print and full looks. Composed, confident styling for lookbooks and campaigns.",
    sub: ["Editorial", "Lookbook", "Fashion Campaigns", "Suiting & Styling"],
    arTagline: "قصّات، ونقوش، وحضور.",
    arDesc: "أزياء استوديو وأعمال تحريرية — بدلات، ونقوش صيفية، وإطلالات كاملة. تنسيق واثق للكتالوجات والحملات.",
    arSub: ["تحريري", "كتالوج", "حملات أزياء", "بدلات وتنسيق"],
    grad: ["#2A2622", "#B9A88C"],
    cover: IMG + "fashion-suit-green.jpg",
    pos: "50% 12%",   // full-body — bias to the top so the head stays
    works: [
      { title: "Pinstripe", brand: "Editorial", tag: "Fashion · Suiting", img: IMG + "fashion-suit-green.jpg" },
      { title: "Tailored", brand: "Lookbook", tag: "Fashion", img: IMG + "fashion-suit-grey.jpg" },
      { title: "Off Duty", brand: "Editorial", tag: "Fashion · Attitude", img: IMG + "fashion-seated.jpg" },
      { title: "Bloom", brand: "Resort Story", tag: "Fashion · Print", img: IMG + "fashion-floral.jpg" },
    ],
  },
  {
    n: "03",
    slug: "couture",
    title: "Modest & Couture",
    ar: "المحتشم والراقي",
    tagline: "The way luxury fabric moves.",
    desc: "Abaya, kaftan and occasion couture — embellished, elegant and made for the Gulf. Modest fashion shot with an editorial eye.",
    sub: ["Abaya", "Kaftan", "Couture", "Occasion Wear"],
    arTagline: "كيف يتحرّك القماش الفاخر.",
    arDesc: "عبايات، وقفاطين، وأزياء المناسبات الراقية — مطرّزة وأنيقة وصُنعت للخليج. أزياء محتشمة بعينٍ تحريرية.",
    arSub: ["عباية", "قفطان", "أزياء راقية", "ملابس المناسبات"],
    grad: ["#26303A", "#AEB9C4"],
    cover: IMG + "couture-03.jpg",
    pos: "50% 8%",    // full-body — bias to the top so the head stays
    works: [
      { title: "Azure I", brand: "Couture Kaftan", tag: "Modest · Couture", img: IMG + "couture-01.jpg" },
      { title: "Azure II", brand: "Couture Kaftan", tag: "Modest · Couture", img: IMG + "couture-02.jpg" },
      { title: "Azure III", brand: "Couture Kaftan", tag: "Modest · Couture", img: IMG + "couture-03.jpg" },
      { title: "Azure IV", brand: "Couture Kaftan", tag: "Modest · Couture", img: IMG + "couture-04.jpg" },
    ],
  },
  {
    n: "04",
    slug: "watches",
    title: "Watches & Hand",
    ar: "الساعات واليد",
    tagline: "Where luxury meets the wrist.",
    desc: "Hand and watch modelling for luxury timepiece houses — Calvin Klein, Coach, Roberto Cavalli by Franck Muller and Ferragamo, shot in partnership with Alhomaidhi.",
    sub: ["Hand Model", "Watch Campaigns", "Jewelry", "Product"],
    arTagline: "حيث يلتقي الفخم بالمعصم.",
    arDesc: "عرض لليد والساعات لأرقى بيوت الساعات — كالفن كلاين، وكوتش، وروبرتو كافالي باي فرانك مولر، وفيراغامو، بالتعاون مع الحميضي للساعات.",
    arSub: ["عارضة يد", "حملات ساعات", "مجوهرات", "منتجات"],
    grad: ["#29231D", "#B89B62"],
    cover: IMG + "ambassador-cavalli.jpg",
    pos: "50% 46%",   // face sits mid-frame in this in-store shot
    works: [
      { title: "Calvin Klein", brand: "Watch Campaign", tag: "Hand · Watches", img: IMG + "watch-calvinklein.jpg" },
      { title: "Coach", brand: "New York — Watch Campaign", tag: "Hand · Watches", img: IMG + "watch-coach.jpg" },
      { title: "Roberto Cavalli", brand: "by Franck Muller", tag: "Hand · Watches", img: IMG + "watch-cavalli.jpg" },
      { title: "Ferragamo", brand: "Watch Campaign", tag: "Hand · Watches", img: IMG + "watch-ferragamo.jpg" },
      { title: "In-Store", brand: "Alhomaidhi × Roberto Cavalli", tag: "Ambassador", img: IMG + "ambassador-cavalli.jpg" },
    ],
  },
  {
    n: "05",
    slug: "ambassador",
    title: "Ambassador & Commercial",
    ar: "سفيرة العلامات والتجاري",
    tagline: "Your brand, with a face people trust.",
    desc: "Brand ambassadorship, commercial content and corporate representation — the recognizable presence of a house at campaigns, events and in the room.",
    sub: ["Brand Ambassador", "Commercial", "Events", "Corporate Representation"],
    arTagline: "علامتك، بوجهٍ يثق به الناس.",
    arDesc: "سفارة علامات، ومحتوى تجاري، وتمثيل رسمي للشركات — الحضور المميّز للعلامة في الحملات والفعاليات وداخل القاعة.",
    arSub: ["سفيرة علامة", "تجاري", "فعاليات", "تمثيل الشركات"],
    grad: ["#1C1A18", "#C9A17A"],
    cover: IMG + "fashion-seated.jpg",
    pos: "50% 16%",   // seated look — keep the face and sunglasses
    works: [
      { title: "Alhomaidhi", brand: "Watch House — Ambassador", tag: "Brand Ambassador", img: IMG + "ambassador-cavalli.jpg" },
      { title: "The Film", brand: "Brand Showreel", tag: "Commercial · Video", type: "video", video: VID + "reel-b.mp4", img: IMG + "beauty-glam-01.jpg" },
      { title: "MecroLine", brand: "General Supplies — Commercial", tag: "Commercial", img: IMG + "logo-mecroline.jpg" },
      { title: "In the Room", brand: "Corporate & Events", tag: "Representation", img: IMG + "fashion-suit-grey.jpg" },
    ],
  },
];

/* Hero swiper slides — real client photography. `pos` is a face-safe crop
   focal point (object-position) so PARDIS's face always reads in the
   full-bleed background, whatever the viewport shape. Edit freely. */
const HERO_SLIDES = [
  { img: IMG + "beauty-glam-02.jpg",     label: "Beauty & Glam",     pos: "50% 24%" },
  { img: IMG + "hero-couture.jpg",       label: "Modest Couture",    pos: "50% 15%" },
  { img: IMG + "fashion-suit-green.jpg", label: "Editorial Fashion", pos: "50% 9%"  },
  { img: IMG + "fashion-suit-grey.jpg",  label: "Signature Style",   pos: "50% 30%" },
  { img: IMG + "couture-01.jpg",         label: "Couture Campaign",  pos: "50% 16%" },
];

/* helper: find a category by slug */
function getCategory(slug) {
  return CATEGORIES.find((c) => c.slug === slug) || null;
}
