/* =============================================================================
   i18n — bilingual UI chrome (EN / AR) + language & theme control
   Dynamic content (categories, services, works…) carries its own EN/AR fields
   in data.js and is rendered by main.js. This file translates the STATIC
   chrome (nav, section headings, buttons) and owns the language/theme state.
   ============================================================================= */

const LS_LANG = "bardees-lang";
const LS_THEME = "bardees-theme";

const STRINGS = {
  en: {
    "nav.index": "Index",
    "nav.work": "Work",
    "nav.about": "About",
    "nav.services": "Services",
    "nav.videos": "Videos",
    "nav.contact": "Contact",
    "nav.book": "Book",

    "videos.eyebrow": "Watch",
    "videos.title": "The <em>film</em> library.",
    "videos.note": "Campaigns, brand films and content — filter by category, tap any film to play.",
    "videos.viewAll": "Browse all films",

    "hero.issue": "The Index — Issue 01",
    "hero.eyebrow": "Saudi Market Model · Brand Ambassador",
    "hero.lead": "<em class=\"serif-em\" style=\"font-style:normal\">Representing brands. Connecting markets.</em> Advertising, ambassadorship, events and corporate representation — built for the Saudi audience, in the Saudi dialect.",
    "hero.sectors": "Travel · Tourism · Hospitality · Lifestyle · Luxury",
    "hero.ctaWork": "View the work",
    "hero.ctaBook": "Work with me",

    "trusted.eyebrow": "Trusted by brands across the Gulf",

    "clients.eyebrow": "Clients & ambassadorships",
    "clients.title": "Brands I've been the <em>face</em> of.",
    "clients.note": "A short register of the houses I've represented — as ambassador and advertising face — across the Kingdom and the Gulf.",

    "why.eyebrow": "Why Bardees for the Saudi market",
    "why.title": "Built for the <em>Saudi audience.</em>",
    "why.note": "Helping brands enter, connect with, and grow within the Saudi market.",

    "index.eyebrow": "Contents — select a discipline",
    "index.title": "Choose the kind of<br><em>collaboration</em> you need.",
    "index.note": "Every brand enters differently. Pick a category to see the work that speaks to your campaign.",

    "work.eyebrow": "Selected — Issue 01",
    "work.title": "Selected <em>work</em>",
    "work.viewAll": "Browse all disciplines",

    "reels.eyebrow": "Showreel",
    "reels.title": "See me <em>in motion</em>.",
    "reels.note": "A short cut across campaigns, content and events.",

    "about.eyebrow": "About Bardees",
    "about.secTitle": "The face brands <em>trust</em>.",
    "about.lead": "A model and advertising face specialised in content for brands targeting the <em>Saudi and Gulf market.</em>",
    "about.body": "I present brands with a natural, professional style — with command of the Saudi dialect to reach the Saudi audience more closely and effectively. I work across advertising photography and film, social-media content, events and exhibitions, and representing brands in the Saudi market.",
    "about.more": "Read the full profile",

    "presence.eyebrow": "Digital presence",
    "presence.title": "Audience &amp; <em>platforms</em>.",
    "presence.platforms": "Platforms",
    "presence.style": "Content style",
    "presence.audience": "Audience focus",
    "presence.audienceV": "Saudi Arabia · GCC · Arabic-speaking audience",

    "services.eyebrow": "What I offer",
    "services.title": "Services, <em>tailored</em><br>to the brief.",
    "services.more": "View all services",

    "process.eyebrow": "How a collaboration works",
    "process.title": "Four steps, <em>zero friction</em>.",
    "process.s1t": "Choose a discipline",
    "process.s1d": "Pick the category that fits your campaign — from hand model to corporate representation.",
    "process.s2t": "Share the brief",
    "process.s2d": "Dates, deliverables, usage rights and budget. Note: wardrobe is styled to suit each ad.",
    "process.s3t": "Receive a proposal",
    "process.s3d": "Availability and a tailored quote — priced per assignment for corporate work.",
    "process.s4t": "Create",
    "process.s4d": "On set, on stage, or in the room — one consistent, professional presence.",

    "faq.eyebrow": "Good to know",
    "faq.title": "Questions, <em>answered</em>.",
    "faq.q1": "How do rates work?",
    "faq.a1": "Advertising shoots are quoted by scope and usage. Corporate representation is priced per assignment — by the nature of the event and the duration of attendance.",
    "faq.q2": "Do you work in the Saudi dialect?",
    "faq.a2": "Yes — I deliver advertising content in the Saudi dialect, naturally and professionally, to reach the Saudi audience.",
    "faq.q3": "How is wardrobe handled?",
    "faq.a3": "Wardrobe is styled to suit each ad — plain and refined for a product like marble, or more expressive and fashion-led where the brand calls for it.",
    "faq.q4": "Do you travel across the GCC?",
    "faq.a4": "Yes — based in Saudi Arabia and available across the GCC. Travel is arranged per booking.",
    "faq.q5": "How far ahead should we book?",
    "faq.a5": "Two to three weeks is ideal, though rush bookings are considered subject to availability.",

    "cta.title": "Let's make<br>something <em>unforgettable</em>.",
    "cta.sub": "Share your brief and receive a tailored proposal — campaigns, ambassadorships, events and corporate representation across the Kingdom and the GCC.",
    "cta.button": "Work with me",

    "footer.eyebrow": "Let's work together",
    "footer.lead": "Representing brands.<br><em>Connecting markets.</em>",
    "footer.start": "Start a project",
    "footer.explore": "Explore",
    "footer.contact": "Contact",
    "footer.tagline": "Saudi Market Model & Brand Ambassador. Based in Saudi Arabia · available across the GCC.",
    "footer.rights": "All rights reserved.",

    "common.back": "← Back to the Index",
    "common.soon": "Portfolio in progress",
    "common.soonNote": "Selected work for this discipline is being added. Get in touch to discuss a collaboration.",
    "common.enquire": "Enquire about this",
    "common.within": "Within this discipline",
    "common.selected": "Selected work — tap an image to enlarge",
    "common.bookThis": "Enquire about this discipline",
    "common.prev": "← Previous",
    "common.next": "Next →",
    "common.lang": "العربية",
    "connect.copy": "Copy link",
    "connect.share": "Share",

    "book.eyebrow": "Contact & booking",
    "book.title": "Tell me about<br>the <em>brief.</em>",
    "book.note": "Share a few details and you'll receive a tailored proposal — including availability and, for corporate representation, a quote based on the event and duration of attendance.",
    "book.email": "Email",
    "book.phone": "Phone",
    "book.whatsapp": "WhatsApp",
    "book.based": "Based",
    "book.fBrand": "Brand / Company",
    "book.fContact": "Contact name",
    "book.fEmail": "Email",
    "book.fPhone": "Phone",
    "book.fType": "Type of collaboration",
    "book.fTypePh": "Select a discipline",
    "book.fDate": "Preferred date(s)",
    "book.fBudget": "Budget range",
    "book.fLocation": "Location",
    "book.fMessage": "About the project",
    "book.fMessagePh": "Tell me about the campaign, deliverables, usage and timeline.",
    "book.hint": "Frontend preview — this form isn't connected to a backend yet.",
    "book.send": "Send request",
    "book.successT": "Request received.",
    "book.successP": "Thank you. Your request has been noted — you'll hear back with availability and a tailored proposal.",
    "book.successCta": "Back to portfolio",
  },
  ar: {
    "nav.index": "الأقسام",
    "nav.work": "الأعمال",
    "nav.about": "عني",
    "nav.services": "الخدمات",
    "nav.videos": "الفيديوهات",
    "nav.contact": "تواصل",
    "nav.book": "احجز",

    "videos.eyebrow": "شاهد",
    "videos.title": "مكتبة <em>الأفلام</em>.",
    "videos.note": "حملات وأفلام علامات ومحتوى — رشّح حسب التصنيف، واضغط أي فيلم لتشغيله.",
    "videos.viewAll": "تصفّح كل الأفلام",

    "hero.issue": "الفهرس — العدد ٠١",
    "hero.eyebrow": "عارضة السوق السعودي · سفيرة علامات",
    "hero.lead": "<em class=\"serif-em\" style=\"font-style:normal\">أمثّل العلامات. أربط الأسواق.</em> إعلانات وسفارة علامات وفعاليات وتمثيل مؤسسي — مصمّمة للجمهور السعودي، وباللهجة السعودية.",
    "hero.sectors": "سفر · سياحة · ضيافة · لايف ستايل · فخامة",
    "hero.ctaWork": "شاهد الأعمال",
    "hero.ctaBook": "لنعمل معاً",

    "trusted.eyebrow": "علامات تجارية تعاونت معها في الخليج",

    "clients.eyebrow": "عملاء وسفارات علامات",
    "clients.title": "علامات كنت <em>الوجه</em> الإعلاني لها.",
    "clients.note": "سجلٌّ موجز للعلامات التي مثّلتها — كسفيرة ووجه إعلاني — في المملكة والخليج.",

    "why.eyebrow": "لماذا برديس للسوق السعودي",
    "why.title": "مصمّمة <em>للجمهور السعودي.</em>",
    "why.note": "أساعد العلامات على الدخول إلى السوق السعودي والتواصل معه والنمو فيه.",

    "index.eyebrow": "المحتوى — اختر التخصص",
    "index.title": "اختر نوع<br><em>التعاون</em> الذي تحتاجه.",
    "index.note": "كل علامة تدخل بطريقتها. اختر قسماً لترى الأعمال التي تناسب حملتك.",

    "work.eyebrow": "مختارات — العدد ٠١",
    "work.title": "أعمال <em>مختارة</em>",
    "work.viewAll": "تصفّح كل التخصصات",

    "reels.eyebrow": "شوريل",
    "reels.title": "شاهدني <em>في حركة</em>.",
    "reels.note": "لقطات سريعة من الحملات والمحتوى والفعاليات.",

    "about.eyebrow": "عن برديس",
    "about.secTitle": "الوجه الذي تثق به <em>العلامات</em>.",
    "about.lead": "مودل ووجه إعلاني متخصصة في المحتوى للعلامات التي تستهدف <em>السوق السعودي والخليجي.</em>",
    "about.body": "أقدّم العلامة التجارية بأسلوب طبيعي واحترافي، مع إتقان اللهجة السعودية للوصول إلى الجمهور السعودي بطريقة أقرب وأكثر تأثيراً. أعمل عبر التصوير والفيديو الإعلاني، ومحتوى السوشيال ميديا، والفعاليات والمعارض، وتمثيل العلامات في السوق السعودي.",
    "about.more": "اقرأ الملف الكامل",

    "presence.eyebrow": "الحضور الرقمي",
    "presence.title": "الجمهور <em>والمنصات</em>.",
    "presence.platforms": "المنصات",
    "presence.style": "أسلوب المحتوى",
    "presence.audience": "الجمهور المستهدف",
    "presence.audienceV": "السعودية · الخليج · الجمهور الناطق بالعربية",

    "services.eyebrow": "ما أقدّمه",
    "services.title": "خدمات <em>مصمّمة</em><br>حسب الطلب.",
    "services.more": "كل الخدمات",

    "process.eyebrow": "كيف يتم التعاون",
    "process.title": "أربع خطوات، <em>بلا تعقيد</em>.",
    "process.s1t": "اختر التخصص",
    "process.s1d": "اختر القسم المناسب لحملتك — من عارضة يد إلى تمثيل الشركات.",
    "process.s2t": "شارك التفاصيل",
    "process.s2d": "المواعيد والمخرجات وحقوق الاستخدام والميزانية. ملاحظة: يُنسّق اللبس حسب كل إعلان.",
    "process.s3t": "استلم العرض",
    "process.s3d": "الإتاحة وعرض سعر مخصّص — بمقابل لكل مهمة في الأعمال المؤسسية.",
    "process.s4t": "ننفّذ",
    "process.s4d": "على الموقع أو المسرح أو داخل القاعة — حضور احترافي ثابت.",

    "faq.eyebrow": "معلومات مفيدة",
    "faq.title": "أسئلة، <em>وإجابات</em>.",
    "faq.q1": "كيف تُحتسب الأسعار؟",
    "faq.a1": "تُسعّر جلسات التصوير حسب النطاق والاستخدام. أما تمثيل الشركات فبمقابل لكل مهمة — حسب طبيعة الحدث ومدة الحضور.",
    "faq.q2": "هل تقدّمين المحتوى باللهجة السعودية؟",
    "faq.a2": "نعم — أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية للوصول إلى الجمهور السعودي.",
    "faq.q3": "كيف يتم التعامل مع اللبس؟",
    "faq.a3": "يُنسّق اللبس حسب كل إعلان — سادة وراقٍ لمنتج مثل الرخام، وأكثر تعبيراً وأزياءً حين تتطلب العلامة ذلك.",
    "faq.q4": "هل تسافرين داخل الخليج؟",
    "faq.a4": "نعم — مقيمة في السعودية ومتاحة في دول الخليج. يُرتّب السفر حسب كل حجز.",
    "faq.q5": "ما المدة المناسبة للحجز مسبقاً؟",
    "faq.a5": "من أسبوعين إلى ثلاثة مثالي، مع إمكانية النظر في الحجوزات العاجلة حسب الإتاحة.",

    "cta.title": "لنصنع<br>شيئاً <em>لا يُنسى</em>.",
    "cta.sub": "شارك التفاصيل واحصل على عرض مخصّص — حملات وسفارة علامات وفعاليات وتمثيل مؤسسي في المملكة والخليج.",
    "cta.button": "لنعمل معاً",

    "footer.eyebrow": "لنعمل معاً",
    "footer.lead": "أمثّل العلامات.<br><em>أربط الأسواق.</em>",
    "footer.start": "ابدأ مشروعاً",
    "footer.explore": "تصفّح",
    "footer.contact": "تواصل",
    "footer.tagline": "عارضة السوق السعودي وسفيرة علامات تجارية. مقيمة في السعودية · متاحة في دول الخليج.",
    "footer.rights": "جميع الحقوق محفوظة.",

    "common.back": "→ العودة للأقسام",
    "common.soon": "المحتوى قيد الإضافة",
    "common.soonNote": "يجري إضافة أعمال مختارة لهذا التخصص. تواصل معي لمناقشة تعاون.",
    "common.enquire": "استفسر عن هذا",
    "common.within": "ضمن هذا التخصص",
    "common.selected": "أعمال مختارة — اضغط الصورة للتكبير",
    "common.bookThis": "استفسر عن هذا التخصص",
    "common.prev": "→ السابق",
    "common.next": "التالي ←",
    "common.lang": "English",
    "connect.copy": "نسخ الرابط",
    "connect.share": "مشاركة",

    "book.eyebrow": "تواصل وحجز",
    "book.title": "أخبرني عن<br><em>المشروع.</em>",
    "book.note": "شارك بعض التفاصيل وستصلك خطة مخصّصة — تشمل الإتاحة، ولتمثيل الشركات عرض سعر حسب الحدث ومدة الحضور.",
    "book.email": "البريد",
    "book.phone": "الهاتف",
    "book.whatsapp": "واتساب",
    "book.based": "الإقامة",
    "book.fBrand": "العلامة / الشركة",
    "book.fContact": "اسم المسؤول",
    "book.fEmail": "البريد الإلكتروني",
    "book.fPhone": "الهاتف",
    "book.fType": "نوع التعاون",
    "book.fTypePh": "اختر تخصصاً",
    "book.fDate": "التواريخ المفضّلة",
    "book.fBudget": "نطاق الميزانية",
    "book.fLocation": "الموقع",
    "book.fMessage": "عن المشروع",
    "book.fMessagePh": "أخبرني عن الحملة والمخرجات والاستخدام والجدول الزمني.",
    "book.hint": "معاينة واجهة — النموذج غير مربوط بخادم بعد.",
    "book.send": "إرسال الطلب",
    "book.successT": "تم استلام الطلب.",
    "book.successP": "شكراً لك. تم تسجيل طلبك — وسأعود إليك بالإتاحة وعرض مخصّص.",
    "book.successCta": "العودة للأعمال",
  },
};

const I18N = {
  get lang() {
    return document.documentElement.getAttribute("lang") === "ar" ? "ar" : "en";
  },
  t(key) {
    const l = this.lang;
    return (STRINGS[l] && STRINGS[l][key]) || STRINGS.en[key] || key;
  },
  /** Apply dir/lang to <html>, persist, translate chrome, and notify listeners. */
  setLang(lang) {
    const l = lang === "ar" ? "ar" : "en";
    const root = document.documentElement;
    root.setAttribute("lang", l);
    root.setAttribute("dir", l === "ar" ? "rtl" : "ltr");
    swapBootstrapDir(l);
    try {
      localStorage.setItem(LS_LANG, l);
    } catch (e) {}
    this.apply();
    document.dispatchEvent(new CustomEvent("langchange", { detail: { lang: l } }));
  },
  toggleLang() {
    this.setLang(this.lang === "ar" ? "en" : "ar");
  },
  setTheme(theme) {
    const t = theme === "dark" ? "dark" : "light";
    document.documentElement.setAttribute("data-theme", t);
    document.documentElement.setAttribute("data-bs-theme", t); // Bootstrap 5.3 components
    try {
      localStorage.setItem(LS_THEME, t);
    } catch (e) {}
  },
  /** Translate every [data-i18n] / [data-i18n-ph] element in the DOM. */
  apply() {
    document.querySelectorAll("[data-i18n]").forEach((el) => {
      el.innerHTML = this.t(el.getAttribute("data-i18n"));
    });
    document.querySelectorAll("[data-i18n-ph]").forEach((el) => {
      el.setAttribute("placeholder", this.t(el.getAttribute("data-i18n-ph")));
    });
    document.querySelectorAll("[data-i18n-aria]").forEach((el) => {
      el.setAttribute("aria-label", this.t(el.getAttribute("data-i18n-aria")));
    });
  },
};

/* Swap the Bootstrap stylesheet to the RTL build in Arabic (id="bs-css"). */
function swapBootstrapDir(l) {
  const link = document.getElementById("bs-css");
  if (!link) return;
  const rtl = l === "ar";
  const href = link.getAttribute("href");
  if (rtl && !/bootstrap\.rtl/.test(href)) link.setAttribute("href", href.replace("bootstrap.min.css", "bootstrap.rtl.min.css"));
  if (!rtl && /bootstrap\.rtl/.test(href)) link.setAttribute("href", href.replace("bootstrap.rtl.min.css", "bootstrap.min.css"));
}

/* Boot: restore saved language + theme before first paint of chrome. */
(function () {
  try {
    const root = document.documentElement;
    const savedLang = localStorage.getItem(LS_LANG);
    if (savedLang) {
      root.setAttribute("lang", savedLang);
      root.setAttribute("dir", savedLang === "ar" ? "rtl" : "ltr");
      swapBootstrapDir(savedLang);
    }
    const savedTheme = localStorage.getItem(LS_THEME) || "dark"; // default dark
    root.setAttribute("data-theme", savedTheme);
    root.setAttribute("data-bs-theme", savedTheme);
  } catch (e) {}
})();

window.I18N = I18N;
