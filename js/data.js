/* =============================================================================
   PORTFOLIO DATA  —  single source of truth
   Replace image URLs with the model's real shoots. Everything else (titles,
   brands, sub-categories) is editable here and re-renders the whole site.
   ============================================================================= */

const PROFILE = {
  name: "NOOR",                 // ← replace with the model's real name
  nameFull: "Noor Al-Rashid",   // ← replace
  nameAr: "نُور",
  role: "Model · Brand Ambassador · Corporate Representative",
  roleAr: "عارضة أزياء · سفيرة علامات تجارية · ممثلة رسمية للشركات",
  base: "Riyadh, Kingdom of Saudi Arabia",
  email: "hello@noor.studio",
  phone: "+966 5X XXX XXXX",
  instagram: "@noor",
  // Portfolio facts brands ask for — edit freely
  facts: [
    { label: "Based in", value: "Riyadh, KSA" },
    { label: "Languages", value: "Arabic · English" },
    { label: "Height", value: "174 cm" },
    { label: "Availability", value: "GCC & International" },
  ],
  stats: [
    { value: "7", label: "Disciplines" },
    { value: "40+", label: "Campaigns" },
    { value: "25+", label: "Brands" },
    { value: "5", label: "Years on set" },
  ],
};

/* Each category: number, slug, title, arabic label, an editorial tagline,
   a short brief-derived description, its sub-disciplines, an accent gradient
   used for fallback plates, and a set of works (image/video). */
const CATEGORIES = [
  {
    n: "01",
    slug: "beauty-fashion",
    title: "Beauty & Fashion",
    ar: "الأزياء والجمال",
    tagline: "The face, the hands, the silhouette.",
    desc: "Editorial and commercial beauty work — from close-up hand and watch campaigns to full fashion shoots, abayas and couture.",
    sub: ["Hand Model — Watches", "Makeup Model", "Hair & Hairstyle", "Fashion Model", "Abaya & Fashion Shoots"],
    arTagline: "الوجه، واليدان، والقوام.",
    arDesc: "أعمال جمال تحريرية وتجارية — من حملات اليد والساعات المقرّبة إلى جلسات الأزياء الكاملة والعبايات والأزياء الراقية.",
    arSub: ["عارضة يد — ساعات", "عارضة مكياج", "الشعر والتسريحات", "عارضة أزياء", "العبايات وجلسات الأزياء"],
    grad: ["#3A2530", "#C98F7E"],
    cover: "https://images.unsplash.com/photo-1469334031218-e382a71b716b?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "Hours", brand: "Timepiece Campaign", tag: "Hand · Watches", img: "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=900&q=70" },
      { title: "Kohl", brand: "Beauty Editorial", tag: "Makeup", img: "https://images.unsplash.com/photo-1512496015851-a90fb38ba796?auto=format&fit=crop&w=900&q=70" },
      { title: "Silk Movement", brand: "Abaya House", tag: "Fashion · Abaya", img: "https://images.unsplash.com/photo-1594633312681-425c7b97ccd1?auto=format&fit=crop&w=900&q=70" },
      { title: "Crown", brand: "Hair Story", tag: "Hair", img: "https://images.unsplash.com/photo-1560869713-7d0a29430803?auto=format&fit=crop&w=900&q=70" },
      { title: "Runway", brand: "Seasonal Show", tag: "Fashion", img: "https://images.unsplash.com/photo-1558769132-cb1aea458c5e?auto=format&fit=crop&w=900&q=70" },
      { title: "Adorn", brand: "Fine Jewelry", tag: "Hand · Jewelry", img: "https://images.unsplash.com/photo-1515562141207-7a88fb7ce338?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "02",
    slug: "commercial",
    title: "Commercial",
    ar: "الإعلانات التجارية",
    tagline: "Built to sell, made to remember.",
    desc: "National campaigns and brand films — the recognizable face of an advertisement, on screen and in print.",
    sub: ["Commercial Advertising", "Brand Campaigns", "National Day Films", "TVC & Digital"],
    arTagline: "صُنعت لتبيع، وتبقى في الذاكرة.",
    arDesc: "حملات وطنية وأفلام للعلامات — الوجه المميّز للإعلان، على الشاشة وفي المطبوعات.",
    arSub: ["إعلانات تجارية", "حملات العلامات", "أفلام اليوم الوطني", "إعلانات تلفزيونية ورقمية"],
    grad: ["#1C2A2A", "#C9A17A"],
    cover: "https://images.unsplash.com/photo-1578662996442-48f60103fc96?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "National Day", brand: "Seasonal Film", tag: "Campaign · Video", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerJoyrides.mp4", img: "https://images.unsplash.com/photo-1578321272176-b7bbc0679853?auto=format&fit=crop&w=900&q=70" },
      { title: "Skyline", brand: "Retail Brand", tag: "Advertising", img: "https://images.unsplash.com/photo-1516321318423-f06f85e504b3?auto=format&fit=crop&w=900&q=70" },
      { title: "Everyday", brand: "Lifestyle Brand", tag: "Commercial", img: "https://images.unsplash.com/photo-1483985988355-763728e1935b?auto=format&fit=crop&w=900&q=70" },
      { title: "The Spot", brand: "Telecom", tag: "TVC", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerBlazes.mp4", img: "https://images.unsplash.com/photo-1492691527719-9d1e07e534b4?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "03",
    slug: "ambassador",
    title: "Brand Ambassador",
    ar: "سفيرة العلامات",
    tagline: "The official face of the house.",
    desc: "Long-term partnerships as the voice and image of a brand — including ambassadorship for a leading Gulf marble house.",
    sub: ["Brand Ambassador", "Gulf Marble House", "Collaborations", "Long-term Partnerships"],
    arTagline: "الوجه الرسمي للعلامة.",
    arDesc: "شراكات طويلة الأمد كصوتٍ وصورة للعلامة — بما في ذلك سفارة إحدى كبرى شركات الرخام الخليجية.",
    arSub: ["سفيرة علامة", "شركة رخام خليجية", "تعاونات", "شراكات طويلة الأمد"],
    grad: ["#2A2622", "#B9A88C"],
    cover: "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "Veined Stone", brand: "Gulf Marble House", tag: "Ambassador", img: "https://images.unsplash.com/photo-1618221195710-dd6b41faaea6?auto=format&fit=crop&w=900&q=70" },
      { title: "Quarry Light", brand: "Marble Campaign", tag: "Ambassador · Video", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4", img: "https://images.unsplash.com/photo-1604014237800-1c9102c219da?auto=format&fit=crop&w=900&q=70" },
      { title: "Signature", brand: "Luxury Partner", tag: "Collaboration", img: "https://images.unsplash.com/photo-1441986300917-64674bd600d8?auto=format&fit=crop&w=900&q=70" },
      { title: "The Launch", brand: "Brand Activation", tag: "Ambassador", img: "https://images.unsplash.com/photo-1492707892479-7bc8d5a4ee93?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "04",
    slug: "food",
    title: "Food & Beverage",
    ar: "الأطعمة والمشروبات",
    tagline: "Crafted to crave.",
    desc: "The talent behind dessert, restaurant and café campaigns — food-and-beverage storytelling that makes an audience hungry.",
    sub: ["Dessert Brands", "Restaurants", "Cafés", "F&B Campaigns"],
    arTagline: "صُمّمت لتُشتهى.",
    arDesc: "الموهبة خلف حملات الحلويات والمطاعم والمقاهي — سردٌ للأطعمة والمشروبات يفتح الشهية.",
    arSub: ["علامات الحلويات", "المطاعم", "المقاهي", "حملات الأطعمة والمشروبات"],
    grad: ["#2B1E16", "#C99A5B"],
    cover: "https://images.unsplash.com/photo-1551024601-bec78aea704b?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "Sweet", brand: "Dessert House", tag: "Dessert", img: "https://images.unsplash.com/photo-1551024601-bec78aea704b?auto=format&fit=crop&w=900&q=70" },
      { title: "Pour", brand: "Specialty Café", tag: "Café · Video", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerFun.mp4", img: "https://images.unsplash.com/photo-1495474472287-4d71bcdd2085?auto=format&fit=crop&w=900&q=70" },
      { title: "The Table", brand: "Fine Dining", tag: "Restaurant", img: "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?auto=format&fit=crop&w=900&q=70" },
      { title: "Zest", brand: "Beverage Brand", tag: "F&B Campaign", img: "https://images.unsplash.com/photo-1544145945-f90425340c7e?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "05",
    slug: "medical",
    title: "Medical & Wellness",
    ar: "الطب والعناية",
    tagline: "Trust, made visible.",
    desc: "Healthcare and wellness campaigns for medical brands, dental clinics and medical centers — the reassuring, credible face of care.",
    sub: ["Medical Brands", "Dental Clinics", "Medical Centers", "Healthcare Campaigns"],
    arTagline: "ثقةٌ تُرى بالعين.",
    arDesc: "حملات صحية وعافية للعلامات الطبية وعيادات الأسنان والمراكز الطبية — الوجه الموثوق والدافئ للرعاية.",
    arSub: ["علامات طبية", "عيادات أسنان", "مراكز طبية", "حملات الرعاية الصحية"],
    grad: ["#1B2530", "#9FB8C4"],
    cover: "https://images.unsplash.com/photo-1588776814546-1ffcf47267a5?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "Bright", brand: "Dental Clinic", tag: "Dental", img: "https://images.unsplash.com/photo-1588776814546-1ffcf47267a5?auto=format&fit=crop&w=900&q=70" },
      { title: "Care", brand: "Medical Center", tag: "Healthcare", img: "https://images.unsplash.com/photo-1631217868264-e5b90bb7e133?auto=format&fit=crop&w=900&q=70" },
      { title: "Renew", brand: "Wellness Brand", tag: "Campaign", img: "https://images.unsplash.com/photo-1600334129128-685c5582fd35?auto=format&fit=crop&w=900&q=70" },
      { title: "Clinic Film", brand: "Aesthetic Clinic", tag: "Medical · Video", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerMeltdowns.mp4", img: "https://images.unsplash.com/photo-1579684385127-1ef15d508118?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "06",
    slug: "acting-tv",
    title: "Acting & TV",
    ar: "التمثيل والتلفزيون",
    tagline: "Lights. The other kind of camera.",
    desc: "On-screen and on-stage — television appearances, series, theatre and commercial acting.",
    sub: ["TV Guest", "TV Series", "Theatre", "Commercial Acting"],
    arTagline: "أضواء. من نوعٍ آخر من الكاميرات.",
    arDesc: "على الشاشة وعلى المسرح — ظهور تلفزيوني، ومسلسلات، ومسرح، وتمثيل إعلاني.",
    arSub: ["ضيفة تلفزيونية", "مسلسلات", "مسرح", "تمثيل إعلاني"],
    grad: ["#241826", "#B58BC4"],
    cover: "https://images.unsplash.com/photo-1503095396549-807759245b35?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "Green Room", brand: "TV Guest", tag: "Television", img: "https://images.unsplash.com/photo-1503095396549-807759245b35?auto=format&fit=crop&w=900&q=70" },
      { title: "Season One", brand: "Drama Series", tag: "TV Series · Video", type: "video", video: "https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4", img: "https://images.unsplash.com/photo-1478720568477-152d9b164e26?auto=format&fit=crop&w=900&q=70" },
      { title: "The Stage", brand: "Theatre", tag: "Theatre", img: "https://images.unsplash.com/photo-1507924538820-ede94a04019d?auto=format&fit=crop&w=900&q=70" },
      { title: "Take One", brand: "Commercial Acting", tag: "Acting", img: "https://images.unsplash.com/photo-1489599849927-2ee91cede3ba?auto=format&fit=crop&w=900&q=70" },
    ],
  },
  {
    n: "07",
    slug: "corporate",
    title: "Corporate Representative",
    ar: "الممثلة الرسمية للشركات",
    tagline: "Your brand, in the room.",
    desc: "Representing a company or brand in person — meetings, events, exhibitions and activations. Engaged per assignment, based on the nature of the event and duration of attendance.",
    sub: ["Meetings", "Events", "Exhibitions", "Brand Activations", "Corporate Events", "Client Meetings"],
    arTagline: "علامتك، حاضرةٌ في القاعة.",
    arDesc: "تمثيل شركة أو علامة شخصيًا — اجتماعات، وفعاليات، ومعارض، وتفعيلات. يُتفق عليه بحسب المهمة وطبيعة الحدث ومدة الحضور.",
    arSub: ["اجتماعات", "فعاليات", "معارض", "تفعيلات العلامات", "فعاليات الشركات", "اجتماعات العملاء"],
    grad: ["#161C28", "#C9A17A"],
    cover: "https://images.unsplash.com/photo-1540575467063-178a50c2df87?auto=format&fit=crop&w=1400&q=70",
    works: [
      { title: "The Booth", brand: "Trade Exhibition", tag: "Exhibition", img: "https://images.unsplash.com/photo-1540575467063-178a50c2df87?auto=format&fit=crop&w=900&q=70" },
      { title: "Keynote", brand: "Corporate Event", tag: "Event", img: "https://images.unsplash.com/photo-1511578314322-379afb476865?auto=format&fit=crop&w=900&q=70" },
      { title: "Activation", brand: "Brand Activation", tag: "Activation", img: "https://images.unsplash.com/photo-1505373877841-8d25f7d46678?auto=format&fit=crop&w=900&q=70" },
      { title: "The Meeting", brand: "Client Representation", tag: "Meetings", img: "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?auto=format&fit=crop&w=900&q=70" },
    ],
  },
];

/* helper: find a category by slug */
function getCategory(slug) {
  return CATEGORIES.find((c) => c.slug === slug) || null;
}
