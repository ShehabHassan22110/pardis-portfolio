/* =============================================================================
   PORTFOLIO DATA — single source of truth (BARDEES REFAAT)
   Images are stored as BASE names; app.js builds <picture> with
   assets/img/<base>-<rendition>.webp (+ .jpg fallback).
   Renditions available: portrait (4:5), sq (1:1); hero shots also wide (16:9) + hero (3:4).

   ⚑ CLIENT TO PROVIDE (placeholders marked ⚑): email/phone/whatsapp · social URLs ·
     real audience numbers · media for the 4 "soon" disciplines.
   ============================================================================= */

const PROFILE = {
  name: "BARDEES REFAAT",
  nameShort: "Bardees",
  nameAr: "برديس رفعت",
  nameArShort: "برديس",
  role: "Saudi Market Model & Brand Ambassador",
  roleAr: "عارضة السوق السعودي وسفيرة علامات تجارية",
  tagline: "Representing Brands. Connecting Markets.",
  taglineAr: "أمثّل العلامات. أربط الأسواق.",
  email: "hello@bardeesrefaat.com",   // ⚑ placeholder
  phone: "+966 5X XXX XXXX",           // ⚑ placeholder
  whatsapp: "9665XXXXXXXX",            // ⚑ placeholder (digits only, incl. country code)
  facts: [
    { label: "Based in", value: "Saudi Arabia", ar: "الإقامة", arValue: "السعودية" },
    { label: "Languages", value: "Arabic · English", ar: "اللغات", arValue: "العربية · الإنجليزية" },
    { label: "Dialect", value: "Saudi Arabic", ar: "اللهجة", arValue: "اللهجة السعودية" },
    { label: "Availability", value: "GCC & International", ar: "التغطية", arValue: "الخليج ودولياً" },
  ],
};

const STATS = [
  { value: "XXK+", label: "Followers", ar: "متابع" },           // ⚑
  { value: "XXK+", label: "Monthly Reach", ar: "وصول شهري" },   // ⚑
  { value: "XX%", label: "Saudi Audience", ar: "جمهور سعودي" }, // ⚑
  { value: "GCC", label: "Available", ar: "متاحة" },
];

const SOCIAL = [
  { label: "Instagram", icon: "instagram", url: "#" }, // ⚑
  { label: "TikTok", icon: "tiktok", url: "#" },       // ⚑
  { label: "Snapchat", icon: "snapchat", url: "#" },   // ⚑
  { label: "WhatsApp", icon: "whatsapp", url: "https://wa.me/" + PROFILE.whatsapp },
  { label: "Email", icon: "envelope", url: "mailto:" + PROFILE.email },
];

const PLATFORMS = ["Instagram", "TikTok", "Snapchat"];
const CONTENT_STYLE = [
  { en: "Short-form video", ar: "فيديو قصير" },
  { en: "Reels", ar: "ريلز" },
  { en: "Stories", ar: "ستوريز" },
  { en: "Brand campaigns", ar: "حملات إعلانية" },
  { en: "Event coverage", ar: "تغطية فعاليات" },
  { en: "Lifestyle content", ar: "محتوى لايف ستايل" },
];

const BRANDS = [
  { name: "Calvin Klein" },
  { name: "Coach", em: "New York" },
  { name: "Roberto Cavalli", em: "by Franck Muller" },
  { name: "Ferragamo" },
  { name: "Alhomaidhi", em: "Watches" },
  { name: "MecroLine" },
];

/* Real client / ambassador relationships (logos = clean silhouettes painted via
   CSS mask, theme-aware + gold on hover). Add new clients here. */
const CLIENTS = [
  { name: "Alhomaidhi Watches", nameAr: "الحميضي للساعات", logo: "brand-alhomaidhi", role: "Brand Ambassador", roleAr: "سفيرة العلامة", sector: "Watches", sectorAr: "ساعات" },
  { name: "Alhomaidhi Group", nameAr: "مجموعة الحميضي", logo: "brand-alhomaidhi-group", role: "Brand Ambassador", roleAr: "سفيرة العلامة", sector: "Group", sectorAr: "مجموعة" },
  { name: "MecroLine", nameAr: "ميكرولاين", logo: "brand-mecroline", role: "General Supplies", roleAr: "توريدات عامة", sector: "General Supplies", sectorAr: "توريدات عامة" },
];

const WHY = [
  { title: "Saudi Market Understanding", titleAr: "فهم السوق السعودي", body: "I understand the Saudi audience and the right tone to reach them.", bodyAr: "أفهم طبيعة الجمهور السعودي وأسلوب التواصل المناسب معه." },
  { title: "Saudi Dialect", titleAr: "اللهجة السعودية", body: "I deliver advertising content in the Saudi dialect — naturally and professionally.", bodyAr: "أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية." },
  { title: "Professional Brand Representation", titleAr: "تمثيل احترافي للعلامة", body: "I represent brands with an elegant presence suited to companies, hotels, destinations and premium labels.", bodyAr: "أمثّل العلامة بصورة راقية تناسب الشركات والفنادق والوجهات والعلامات المميزة." },
  { title: "Cross-Market Communication", titleAr: "التواصل بين الأسواق", body: "I help international and Gulf brands introduce themselves to the Saudi audience with impact.", bodyAr: "أساعد الشركات الدولية والخليجية على تقديم نفسها للجمهور السعودي بتأثير أكبر." },
  { title: "On-Camera Presence", titleAr: "حضور أمام الكاميرا", body: "A confident on-camera presence built for advertising and commercial content.", bodyAr: "حضور واثق أمام الكاميرا مناسب للإعلانات والمحتوى التجاري." },
  { title: "Flexible Content Creation", titleAr: "إنتاج محتوى مرن", body: "Content tailored for Instagram, TikTok, Snapchat and beyond.", bodyAr: "محتوى يناسب إنستغرام وتيك توك وسناب شات وغيرها." },
];

const SERVICES = [
  { n: "01", title: "Brand Ambassador", titleAr: "سفيرة علامة تجارية", desc: "Representing your brand and speaking about it to the Saudi audience.", descAr: "تمثيل العلامة والتحدّث عنها أمام الجمهور السعودي." },
  { n: "02", title: "Advertising Model", titleAr: "عارضة إعلانات", desc: "Appearing in advertising campaigns, photography and video.", descAr: "الظهور في الحملات الإعلانية والصور والفيديوهات." },
  { n: "03", title: "Social Media Content", titleAr: "محتوى السوشيال ميديا", desc: "Producing short-form content built for social platforms.", descAr: "إنتاج محتوى قصير مناسب لمنصات التواصل." },
  { n: "04", title: "Saudi Arabic Advertising", titleAr: "إعلانات باللهجة السعودية", desc: "Delivering advertising in Arabic and in the Saudi dialect.", descAr: "تقديم الإعلانات بالعربية وباللهجة السعودية." },
  { n: "05", title: "Event & Exhibition Representation", titleAr: "تمثيل في الفعاليات والمعارض", desc: "Representing your company at exhibitions, events and conferences.", descAr: "تمثيل الشركة في المعارض والفعاليات والمؤتمرات." },
  { n: "06", title: "Tourism & Destination Promotion", titleAr: "الترويج السياحي والوجهات", desc: "Promoting hotels, resorts, destinations and experiences.", descAr: "الترويج للفنادق والمنتجعات والوجهات والتجارب." },
  { n: "07", title: "Lifestyle & Hospitality Content", titleAr: "محتوى لايف ستايل وضيافة", desc: "Professional content for restaurants, cafés, hotels and luxury hospitality.", descAr: "محتوى احترافي للمطاعم والكافيهات والفنادق والضيافة الفاخرة." },
  { n: "08", title: "Market Introduction", titleAr: "تقديم للسوق", desc: "Helping your brand enter the Saudi market and build a local presence.", descAr: "المساعدة في تقديم العلامة للجمهور السعودي وبناء حضور محلي." },
  { n: "09", title: "Marketing", titleAr: "التسويق", desc: "End-to-end marketing for brands — strategy, campaign concepts and content built to move the Saudi market.", descAr: "تسويق متكامل للعلامات — استراتيجية ومفاهيم حملات ومحتوى مصمّم ليحرّك السوق السعودي." },
  { n: "10", title: "Websites & Web", titleAr: "المواقع والويب", desc: "Websites, landing pages and digital presence — designed and built to convert and to look the part.", descAr: "مواقع وصفحات هبوط وحضور رقمي — تصميم وتطوير يليق بالعلامة ويحقّق النتائج." },
];

/* Marketing partnership feature (About page). Videos are YouTube IDs; posters are
   pulled from img.ytimg.com. Add / replace the client and videos here. */
const MARKETING = {
  client: "Marble Company", clientAr: "شركة الرخام",
  role: "Marketing partner", roleAr: "شريك تسويقي",
  eyebrow: "Marketing partnership", eyebrowAr: "شراكة تسويقية",
  title: "We market for <em>شركة الرخام</em>.", titleAr: "نُسوّق لـ<em>شركة الرخام</em>.",
  note: "We lead marketing and brand content for a Saudi marble house — from concept to camera. Two films from the collaboration:",
  noteAr: "نقود التسويق ومحتوى العلامة لشركة رخام سعودية — من الفكرة حتى الكاميرا. فيلمان من التعاون:",
  videos: [
    { id: "xrtEC_iYvek", title: "دانو 2", titleAr: "دانو 2", tag: "Marketing · Film", tagAr: "تسويق · فيلم" },
    { id: "0xwe9BzBvEI", title: "فولاكس", titleAr: "فولاكس", tag: "Marketing · Film", tagAr: "تسويق · فيلم" },
  ],
};

const CATEGORIES = [
  {
    n: "01", slug: "beauty-fashion", status: "live",
    title: "Beauty & Fashion Model", ar: "عارضة جمال وأزياء",
    tagline: "The face, the hand, the look.", arTagline: "الوجه، واليد، والإطلالة.",
    desc: "Beauty and fashion modelling — hand and watch campaigns, makeup and hair, editorial fashion, abayas and couture shoots. One versatile face across the looks a brand builds around.",
    arDesc: "عرض أزياء وجمال — حملات اليد والساعات، ومكياج وشعر، وأزياء تحريرية، وعبايات وتصوير كوتور. وجه متعدد الإطلالات تبني عليه العلامات.",
    sub: ["Hand Model — Watches", "Makeup", "Hairstyle & Hair", "Fashion", "Abaya & Couture"],
    arSub: ["عارضة يد — ساعات", "مكياج", "تسريحات وشعر", "أزياء", "عبايات وكوتور"],
    cover: "beauty-01",
    works: [
      { title: "Gold Hour", brand: "Beauty Editorial", tag: "Makeup · Glam", img: "beauty-01" },
      { title: "Soft Focus", brand: "Beauty Portrait", tag: "Beauty", img: "beauty-03" },
      { title: "Poise", brand: "Beauty Story", tag: "Skin · Glow", img: "beauty-02" },
      { title: "Pinstripe", brand: "Editorial", tag: "Fashion · Suiting", img: "fashion-blazer-stand" },
      { title: "Off Duty", brand: "Editorial", tag: "Fashion · Attitude", img: "fashion-blazer-seated" },
      { title: "Bloom", brand: "Resort Story", tag: "Fashion · Print", img: "fashion-floral" },
      { title: "Rosette", brand: "Occasion", tag: "Fashion", img: "fashion-pink" },
      { title: "Azure I", brand: "Couture Kaftan", tag: "Abaya · Couture", img: "couture-01" },
      { title: "Azure II", brand: "Couture Kaftan", tag: "Abaya · Couture", img: "couture-03" },
      { title: "Azure III", brand: "Couture Kaftan", tag: "Abaya · Couture", img: "couture-04" },
      { title: "Calvin Klein", brand: "Watch Campaign", tag: "Hand · Watches", img: "hand-calvinklein" },
      { title: "Coach", brand: "New York — Watch", tag: "Hand · Watches", img: "hand-coach" },
      { title: "Roberto Cavalli", brand: "by Franck Muller", tag: "Hand · Watches", img: "hand-cavalli" },
      { title: "Ferragamo", brand: "Watch Campaign", tag: "Hand · Watches", img: "hand-ferragamo" },
    ],
  },
  {
    n: "02", slug: "commercial", status: "live",
    title: "Commercial Model", ar: "عارضة تجارية",
    tagline: "Advertising that lands with the audience.", arTagline: "إعلانات تصل إلى الجمهور.",
    desc: "Commercial advertising and brand campaigns — including national-day and seasonal spots — delivered with a natural, persuasive on-camera presence.",
    arDesc: "إعلانات تجارية وحملات للعلامات — من بينها إعلانات اليوم الوطني والمواسم — بحضور طبيعي ومؤثّر أمام الكاميرا.",
    sub: ["Commercial Advertising", "Brand Campaigns", "National-Day Spots", "Product Films"],
    arSub: ["إعلانات تجارية", "حملات العلامات", "إعلانات اليوم الوطني", "أفلام المنتجات"],
    cover: "fashion-olive",
    works: [
      { title: "Olive Tailoring", brand: "Campaign Look", tag: "Commercial", img: "fashion-olive" },
      { title: "The Film", brand: "Brand Showreel", tag: "Commercial · Video", type: "video", video: "assets/video/reel-b.mp4", img: "fashion-pink" },
      { title: "In-Store", brand: "Retail Activation", tag: "Brand Campaign", img: "ambassador-instore" },
    ],
  },
  {
    n: "03", slug: "ambassador", status: "live",
    title: "Brand Ambassador", ar: "سفيرة علامات تجارية",
    tagline: "Your brand, with a face people trust.", arTagline: "علامتك، بوجهٍ يثق به الناس.",
    desc: "Brand ambassadorship for Gulf houses — the recognizable, credible face of a brand in campaigns, at retail and across social. Ambassador for a Gulf marble company and for Alhomaidhi Watches.",
    arDesc: "سفارة علامات تجارية خليجية — الوجه المميّز والموثوق للعلامة في الحملات وفي نقاط البيع وعلى السوشيال. سفيرة لشركة رخام خليجية ولساعات الحميضي.",
    sub: ["Brand Ambassador", "Retail & In-Store", "Campaign Face", "Social Ambassador"],
    arSub: ["سفيرة علامة", "التجزئة والمعارض", "وجه الحملة", "سفيرة سوشيال"],
    cover: "ambassador-instore",
    works: [
      { title: "In-Store", brand: "Alhomaidhi × Roberto Cavalli", tag: "Brand Ambassador", img: "ambassador-instore" },
      { title: "Cavalli", brand: "Watch House — Ambassador", tag: "Ambassador", img: "hand-cavalli" },
    ],
  },
  { n: "04", slug: "food", status: "soon", title: "Food Model", ar: "عارضة أطعمة", tagline: "Appetite, styled.", arTagline: "شهية، بأسلوب.", desc: "Food and beverage modelling for dessert brands, restaurants, cafés and F&B campaigns — appetizing, lifestyle-led content.", arDesc: "عرض للأطعمة والمشروبات لعلامات الحلويات والمطاعم والكافيهات وحملات الأطعمة — محتوى شهيّ بروح اللايف ستايل.", sub: ["Dessert Brands", "Restaurants", "Cafés", "F&B Campaigns"], arSub: ["الحلويات", "المطاعم", "الكافيهات", "حملات الأطعمة"], cover: "beauty-02", works: [] },
  { n: "05", slug: "medical", status: "soon", title: "Medical Brand Model", ar: "عارضة علامات طبية", tagline: "Trust, on camera.", arTagline: "الثقة، أمام الكاميرا.", desc: "Content and representation for medical brands, dental clinics, medical centers and healthcare campaigns — a clean, reassuring, professional presence.", arDesc: "محتوى وتمثيل للعلامات الطبية وعيادات الأسنان والمراكز الطبية والحملات الصحية — حضور نظيف ومطمئن واحترافي.", sub: ["Medical Brands", "Dental Clinics", "Medical Centers", "Healthcare"], arSub: ["علامات طبية", "عيادات أسنان", "مراكز طبية", "حملات صحية"], cover: "beauty-03", works: [] },
  { n: "06", slug: "acting", status: "soon", title: "Acting & TV", ar: "تمثيل وتلفزيون", tagline: "Presence that carries a scene.", arTagline: "حضور يحمل المشهد.", desc: "Acting and on-screen work — TV guest appearances, TV series, theatre and commercial acting.", arDesc: "تمثيل وأعمال أمام الشاشة — ضيافة تلفزيونية ومسلسلات ومسرح وتمثيل إعلاني.", sub: ["TV Guest", "TV Series", "Theatre", "Commercial Acting"], arSub: ["ضيافة تلفزيونية", "مسلسلات", "مسرح", "تمثيل إعلاني"], cover: "fashion-blazer-seated", works: [] },
  { n: "07", slug: "corporate", status: "soon", title: "Corporate Representative", ar: "ممثّلة رسمية للشركات", tagline: "Representing your company, formally.", arTagline: "أمثّل شركتك رسمياً.", desc: "Formal representation of a company or brand at meetings, events, exhibitions, brand activations, corporate events and client meetings. Engaged per assignment — by the nature of the event and duration of attendance.", arDesc: "تمثيل رسمي للشركة أو العلامة في الاجتماعات والفعاليات والمعارض وتفعيلات العلامة والمناسبات ولقاءات العملاء. التعاون بمقابل حسب نوع المهمة ومدة الحضور وطبيعة الحدث.", sub: ["Meetings", "Events & Exhibitions", "Brand Activations", "Client Meetings"], arSub: ["الاجتماعات", "الفعاليات والمعارض", "تفعيلات العلامة", "لقاءات العملاء"], cover: "fashion-blazer-stand", works: [] },
];

const HERO_SLIDES = [
  { img: "couture-01", label: "Modest Couture", labelAr: "أزياء محتشمة" },
  { img: "fashion-olive", label: "Editorial Fashion", labelAr: "أزياء تحريرية" },
  { img: "fashion-blazer-stand", label: "Signature Style", labelAr: "أسلوب مميّز" },
  { img: "fashion-floral", label: "Resort Print", labelAr: "نقوش صيفية" },
  { img: "couture-03", label: "Couture Campaign", labelAr: "حملة كوتور" },
];

const REELS = [
  { video: "assets/video/showreel.mp4", poster: "assets/img/ambassador-instore-portrait.jpg", dur: "0:28", k: "Showreel", kAr: "شوريل", t: "In Motion", tAr: "في حركة", caption: "BARDEES — Showreel" },
  { video: "assets/video/reel-b.mp4", poster: "assets/img/fashion-blazer-seated-portrait.jpg", dur: "0:05", k: "Beauty · Reel", kAr: "جمال · ريل", t: "Golden Glow", tAr: "توهّج ذهبي", caption: "BARDEES — Reel" },
];

/* =============================================================================
   VIDEOS — the Videos page (videos.html). Each entry is one YouTube video.
   • id   → the watch ID (the part after youtu.be/ or watch?v=)
   • cat  → one of the VIDEO_CATS keys below (drives the filter tabs)
   • title / titleAr, and optional client / clientAr shown as the card subtitle
   Filter tabs are built automatically from the categories that actually have
   videos, so adding a new video with a new cat just adds its tab. Posters come
   from img.ytimg.com. Add new videos to this list.
   ============================================================================= */
const VIDEO_CATS = [
  { key: "marketing", en: "Marketing", ar: "التسويق" },
  { key: "commercial", en: "Commercial", ar: "إعلانات تجارية" },
  { key: "beauty", en: "Beauty & Fashion", ar: "جمال وأزياء" },
  { key: "ambassador", en: "Brand Ambassador", ar: "سفارة علامات" },
  { key: "events", en: "Events", ar: "فعاليات ومعارض" },
  { key: "lifestyle", en: "Lifestyle", ar: "لايف ستايل" },
];

const VIDEOS = [
  { id: "ZJl6__WQnSs", cat: "marketing", title: "منشار", titleAr: "منشار", client: "شركة الرخام", clientAr: "شركة الرخام" },
  { id: "xrtEC_iYvek", cat: "marketing", title: "دانو 2", titleAr: "دانو 2", client: "شركة الرخام", clientAr: "شركة الرخام" },
  { id: "0xwe9BzBvEI", cat: "marketing", title: "فولاكس", titleAr: "فولاكس" },
  { id: "0O8uPsXV3eM", cat: "commercial", portrait: true, title: "Commercial Reel", titleAr: "ريل إعلاني" },
  { id: "LSlhsZDXD2E", cat: "beauty", portrait: true, title: "Beauty Reel", titleAr: "ريل جمال" },
];

function getCategory(slug) { return CATEGORIES.find((c) => c.slug === slug) || null; }
