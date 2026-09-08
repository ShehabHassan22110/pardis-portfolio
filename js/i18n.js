/* =============================================================================
   i18n.js — bilingual EN / عربي engine (+ RTL)
   Loaded before main.js / dashboard.js on every page.
   Apply translations with data-i18n="key" (sets innerHTML) and
   data-i18n-ph="key" (sets placeholder). Fires "langchange" on switch so
   JS-rendered content can re-render.
   ============================================================================= */

const I18N = {
  en: {
    dir: "ltr",
    /* nav / shared */
    "nav.index": "Index", "nav.work": "Work", "nav.about": "About",
    "nav.collaborate": "Collaborate", "nav.contact": "Contact", "nav.book": "Book",
    "common.viewAll": "Browse all disciplines", "common.back": "← Back to the Index",
    "lang.toggle": "عربي",
    "wa.tooltip": "Book on WhatsApp", "wa.msg": "Hello Pardis, I'd like to discuss a collaboration.",

    /* hero */
    "hero.eyebrow": "Model · Brand Ambassador · Corporate Representative",
    "hero.role": "One face across fashion and commerce — from <strong>luxury watch and fashion campaigns</strong> to <strong>modest couture</strong>, beauty and brand ambassadorship across the Gulf.",
    "hero.scroll": "The Index",

    /* trusted by */
    "trusted.eyebrow": "Featured with brands across the Gulf",

    /* the index */
    "index.eyebrow": "Contents — Select a discipline",
    "index.title": "Choose the kind of<br><em>collaboration</em> you need.",
    "index.note": "Every brand enters differently. Pick a category to see only the work that speaks to your campaign.",

    /* selected work */
    "work.title": "Selected <em>work</em>",

    /* showreel */
    "showreel.eyebrow": "Showreel",
    "showreel.title": "See her <em>in motion</em>.",
    "showreel.note": "A short cut across campaigns, film and stage.",
    "showreel.play": "Play showreel",

    /* press */
    "press.eyebrow": "As featured in",

    /* social */
    "social.eyebrow": "Follow the journey",
    "social.title": "Beauty, fashion &amp; <em>moments</em>.",
    "social.note": "Behind the scenes, campaigns and moments — as they happen.",
    "social.follow": "Follow",

    /* faq */
    "faq.eyebrow": "Good to know",
    "faq.title": "Questions, <em>answered</em>.",
    "faq.q1": "How do rates work?",
    "faq.a1": "Photo and film shoots are quoted by scope and usage. Corporate representation is priced per assignment — by the nature of the event and the duration of attendance.",
    "faq.q2": "Do you travel for bookings?",
    "faq.a2": "Yes — available across the GCC and internationally. Travel and accommodation are arranged per booking.",
    "faq.q3": "How are usage rights handled?",
    "faq.a3": "Usage, territory and duration are agreed up front and reflected in the quote. Buyouts and exclusivity are available on request.",
    "faq.q4": "Do you work with abaya & modest fashion?",
    "faq.a4": "Absolutely — abaya, modest and couture fashion are a core part of the portfolio.",
    "faq.q5": "How far ahead should we book?",
    "faq.a5": "Two to three weeks is ideal, though rush bookings are considered subject to availability.",

    /* about */
    "about.eyebrow": "About",
    "about.lead": "A single, dependable face for brands across the Gulf — <em>elegant on camera,</em> credible in the room.",
    "about.body": "Pardis works fluidly between editorial and commercial worlds: the close-up precision of a luxury watch and hand campaign, the movement of modest couture and abaya, the polish of a beauty shoot, and the poise of representing a house on set and at events. Bilingual and GCC-based, she brings one consistent, professional presence to every collaboration.",
    "facts.based": "Based in", "facts.basedV": "Riyadh, KSA",
    "facts.lang": "Languages", "facts.langV": "Arabic · English",
    "facts.focus": "Focus", "facts.focusV": "Fashion · Beauty · Watches",
    "facts.avail": "Availability", "facts.availV": "GCC & Intl.",
    "stats.disc": "Disciplines", "stats.watch": "Watch houses", "stats.lang": "Languages", "stats.avail": "Available",

    /* testimonials */
    "testi.eyebrow": "In their words",
    "testi.title": "What brands <em>say</em>.",
    "testi.q1": "Noor gave our marble campaign a face people remembered — effortless on set, magnetic on camera.",
    "testi.a1": "Marketing Director, Marmara Marble",
    "testi.q2": "The abaya lookbook sold out in a week. She understands exactly how fabric moves.",
    "testi.a2": "Creative Lead, Maison Abaya",
    "testi.q3": "Professional, punctual, and completely trusted by our patients. The campaign overdelivered.",
    "testi.a3": "Dr. Sara Fahad, Lumière Aesthetics",

    /* process */
    "process.eyebrow": "How a collaboration works",
    "process.title": "Four steps, <em>zero friction</em>.",
    "process.s1t": "Choose a discipline", "process.s1d": "Pick the category that fits your campaign — from hand model to corporate representation.",
    "process.s2t": "Share the brief", "process.s2d": "Dates, deliverables, usage rights and budget. The more detail, the sharper the proposal.",
    "process.s3t": "Receive a proposal", "process.s3d": "Availability and a tailored quote within two business days — per assignment for corporate work.",
    "process.s4t": "Create", "process.s4d": "On set, on stage, or in the room — one consistent, professional presence.",

    /* services */
    "services.eyebrow": "Ways to collaborate",
    "services.title": "Engagements, <em>tailored</em><br>to the brief.",
    "services.note": "Choose a discipline, share your campaign, and receive a proposal. Corporate representation is quoted per assignment — by the nature of the event and the duration of attendance.",
    "services.unsureT": "Not sure which fits?",
    "services.unsureD": "Tell us about the campaign and we'll recommend the right discipline.",
    "services.unsureCta": "Start a request",

    /* services page */
    "svcpage.eyebrow": "Collaborate",
    "svcpage.title": "Turn an idea into<br>something <em>people remember.</em>",
    "svcpage.sub": "Fashion. Beauty. Watches. Modest couture. Campaigns & ambassadorship — across the Gulf and beyond.",
    "svcpage.listEyebrow": "What I offer",
    "svcpage.listTitle": "Ways to <em>collaborate</em>.",
    "svcpage.listNote": "Choose a discipline to see selected work. Corporate representation is quoted per assignment — by the event and duration of attendance.",

    /* about / profile page */
    "aboutpage.eyebrow": "The Profile",
    "aboutpage.r1": "Model", "aboutpage.r2": "Creator", "aboutpage.r3": "Brand Ambassador",
    "aboutpage.edEyebrow": "The woman behind the work",
    "aboutpage.viewServices": "Discover services",
    "aboutpage.specEyebrow": "Specialties",
    "aboutpage.specTitle": "What she <em>does</em>.",
    "aboutpage.expEyebrow": "Selected campaigns & clients",
    "aboutpage.expTitle": "Trusted <em>work</em>.",
    "home.aboutMore": "Read the full profile",
    "home.servicesMore": "View all services",

    /* cta */
    "cta.title": "Let's make<br>something <em>unforgettable</em>.",
    "cta.sub": "Share your brief and receive a tailored proposal — bookings, campaigns, ambassadorships and corporate representation across the Gulf and beyond.",
    "cta.button": "Request a booking",

    /* footer */
    "footer.tagline": "Model · Brand Ambassador · Corporate Representative. Riyadh, Kingdom of Saudi Arabia.",
    "footer.eyebrow": "Start a conversation",
    "footer.lead": "Let's create<br>something <em>iconic</em>.",
    "footer.start": "Start a project",
    "footer.explore": "Explore", "footer.contact": "Contact",
    "footer.book": "Request a booking", "footer.rights": "All rights reserved.",
    "footer.studio": "Studio login",

    /* category page */
    "cat.within": "Within this discipline", "cat.book": "Book this discipline",
    "cat.selected": "Selected work — tap an image to enlarge",
    "cat.prev": "← Previous", "cat.next": "Next →",
    "cat.ctaTitle": "Ready to <em>collaborate?</em>", "cat.ctaSub": "Share your campaign and receive a tailored proposal.",

    /* booking page */
    "book.eyebrow": "Booking & Collaboration",
    "book.title": "Tell us about<br>the <em>brief.</em>",
    "book.note": "Share a few details and you'll receive a tailored proposal — including availability and, for corporate representation, a quote based on the event and duration of attendance.",
    "book.email": "Email", "book.phone": "Phone", "book.social": "Social", "book.based": "Based",
    "book.fBrand": "Brand / Company", "book.fBrandPh": "Your brand name",
    "book.fContact": "Contact name", "book.fContactPh": "Who should we reach?",
    "book.fEmail": "Email", "book.fEmailPh": "name@company.com",
    "book.fPhone": "Phone",
    "book.fType": "Type of collaboration", "book.fTypePh": "Select a discipline",
    "book.fDate": "Preferred date(s)", "book.fDatePh": "e.g. 12–14 Oct, or flexible",
    "book.fBudget": "Budget range", "book.fBudgetPh": "Select a range",
    "book.fLocation": "Location", "book.fLocationPh": "City / studio / on-site",
    "book.fMessage": "About the project", "book.fMessagePh": "Tell us about the campaign, deliverables, usage and timeline.",
    "book.hint": "Frontend preview — this form isn't connected to a backend yet.",
    "book.send": "Send request",
    "book.typeNotSure": "Not sure yet — advise me",
    "book.successT": "Request received.",
    "book.successP": "Thank you. Your request has been noted — you'll hear back within two business days with availability and a tailored proposal.",
    "book.successCta": "Back to portfolio",
    "connect.eyebrow": "Connect instantly",
    "connect.note": "Scan to open the portfolio, or share it directly.",
    "connect.copy": "Copy link", "connect.share": "Share profile", "connect.copied": "Copied ✓",
  },

  ar: {
    dir: "rtl",
    "nav.index": "الفهرس", "nav.work": "الأعمال", "nav.about": "نبذة",
    "nav.collaborate": "التعاون", "nav.contact": "تواصل", "nav.book": "احجز",
    "common.viewAll": "تصفّح كل التخصصات", "common.back": "← العودة إلى الفهرس",
    "lang.toggle": "EN",
    "wa.tooltip": "احجز عبر واتساب", "wa.msg": "مرحبًا بارديس، أودّ مناقشة تعاون.",

    "hero.eyebrow": "عارضة أزياء · سفيرة علامات · ممثلة رسمية للشركات",
    "hero.role": "وجهٌ واحد بين الأزياء والتجارة — من <strong>حملات الساعات الفاخرة والأزياء</strong> إلى <strong>الأزياء المحتشمة الراقية</strong> والجمال وسفارة العلامات في الخليج.",
    "hero.scroll": "الفهرس",

    "trusted.eyebrow": "أعمال مع علامات تجارية في الخليج",

    "index.eyebrow": "المحتويات — اختر التخصص",
    "index.title": "اختر نوع<br><em>التعاون</em> الذي تحتاجه.",
    "index.note": "كل علامة تجارية لها احتياج مختلف. اختر التصنيف لترى الأعمال المناسبة لحملتك فقط.",

    "work.title": "أعمال <em>مختارة</em>",

    "showreel.eyebrow": "الرِّيل",
    "showreel.title": "شاهدها <em>في الحركة</em>.",
    "showreel.note": "مقطعٌ قصير عبر الحملات والأفلام والمسرح.",
    "showreel.play": "تشغيل الرِّيل",

    "press.eyebrow": "ظهرت في",

    "social.eyebrow": "تابع الرحلة",
    "social.title": "جمال، وأزياء، <em>ولحظات</em>.",
    "social.note": "كواليس، وحملات، ولحظات — أولًا بأول.",
    "social.follow": "تابع",

    "faq.eyebrow": "معلومات مفيدة",
    "faq.title": "أسئلة <em>وإجابات</em>.",
    "faq.q1": "كيف تُحتسب الأسعار؟",
    "faq.a1": "تُسعَّر جلسات التصوير والأفلام حسب النطاق والاستخدام. أما تمثيل الشركات فيُسعَّر بحسب المهمة — وفق طبيعة الحدث ومدة الحضور.",
    "faq.q2": "هل تسافرين للحجوزات؟",
    "faq.a2": "نعم — متاحة في دول الخليج ودوليًا. تُرتَّب تكاليف السفر والإقامة لكل حجز.",
    "faq.q3": "كيف تُدار حقوق الاستخدام؟",
    "faq.a3": "يُتفق على الاستخدام والنطاق الجغرافي والمدة مسبقًا وتُدرَج في العرض. تتوفر حقوق الاستحواذ والحصرية عند الطلب.",
    "faq.q4": "هل تعملين مع العباية والأزياء المحتشمة؟",
    "faq.a4": "بالتأكيد — العباية والأزياء المحتشمة والراقية جزءٌ أساسي من الأعمال.",
    "faq.q5": "قبل كم يُفضَّل الحجز؟",
    "faq.a5": "من أسبوعين إلى ثلاثة مثالي، مع إمكانية النظر في الحجوزات العاجلة حسب التوفر.",

    "about.eyebrow": "نبذة",
    "about.lead": "وجهٌ واحد يُعتمد عليه لعلامات الخليج — <em>أناقة أمام الكاميرا،</em> ومصداقية في القاعة.",
    "about.body": "تنتقل بارديس بسلاسة بين العالم التحريري والتجاري: دقة حملات الساعات الفاخرة واليد، وحركة الأزياء المحتشمة والعبايات الراقية، ورقيّ جلسات الجمال، وثبات تمثيل العلامات على المواقع وفي الفعاليات. تتحدث العربية والإنجليزية ومقرها دول الخليج، وتقدّم حضورًا مهنيًا ثابتًا في كل تعاون.",
    "facts.based": "المقر", "facts.basedV": "الرياض، السعودية",
    "facts.lang": "اللغات", "facts.langV": "العربية · الإنجليزية",
    "facts.focus": "التخصص", "facts.focusV": "أزياء · جمال · ساعات",
    "facts.avail": "التوفر", "facts.availV": "الخليج ودوليًا",
    "stats.disc": "تخصصات", "stats.watch": "بيوت ساعات", "stats.lang": "لغتان", "stats.avail": "التوفر",

    "testi.eyebrow": "بكلماتهم",
    "testi.title": "ماذا تقول <em>العلامات</em>.",
    "testi.q1": "منحت نور حملتنا للرخام وجهًا لا يُنسى — سهلة في التصوير، وآسرة أمام الكاميرا.",
    "testi.a1": "مدير التسويق، رخام مرمرة",
    "testi.q2": "نفد كتالوج العبايات خلال أسبوع. إنها تفهم تمامًا كيف يتحرك القماش.",
    "testi.a2": "المدير الإبداعي، ميزون عباية",
    "testi.q3": "احترافية، ودقيقة في المواعيد، وموضع ثقة كاملة من مرضانا. الحملة فاقت التوقعات.",
    "testi.a3": "د. سارة فهد، لوميير للتجميل",

    "process.eyebrow": "كيف يسير التعاون",
    "process.title": "أربع خطوات، <em>بلا تعقيد</em>.",
    "process.s1t": "اختر التخصص", "process.s1d": "اختر التصنيف المناسب لحملتك — من عارضة اليد إلى تمثيل الشركات.",
    "process.s2t": "شارك التفاصيل", "process.s2d": "التواريخ، والمخرجات، وحقوق الاستخدام، والميزانية. كلما زادت التفاصيل، كان العرض أدق.",
    "process.s3t": "استلم العرض", "process.s3d": "التوفر وعرض سعر مخصص خلال يومي عمل — وبحسب المهمة لأعمال تمثيل الشركات.",
    "process.s4t": "الإبداع", "process.s4d": "في الاستوديو، على المسرح، أو في القاعة — حضور مهني ثابت.",

    "services.eyebrow": "طرق التعاون",
    "services.title": "تعاونات <em>مصمّمة</em><br>حسب الطلب.",
    "services.note": "اختر التخصص، شارك حملتك، واستلم عرضًا. تمثيل الشركات يُسعَّر بحسب المهمة — وفق طبيعة الحدث ومدة الحضور.",
    "services.unsureT": "غير متأكد أيها يناسبك؟",
    "services.unsureD": "أخبرنا عن الحملة وسنرشّح لك التخصص المناسب.",
    "services.unsureCta": "ابدأ الطلب",

    "svcpage.eyebrow": "تعاون",
    "svcpage.title": "حوّل الفكرة إلى<br>شيءٍ <em>يبقى في الذاكرة.</em>",
    "svcpage.sub": "أزياء. جمال. ساعات. أزياء محتشمة راقية. حملات وسفارة علامات — في الخليج وخارجه.",
    "svcpage.listEyebrow": "ما أقدّمه",
    "svcpage.listTitle": "طرق <em>التعاون</em>.",
    "svcpage.listNote": "اختر التخصص لرؤية أعمال مختارة. تمثيل الشركات يُسعَّر بحسب المهمة — وفق الحدث ومدة الحضور.",

    "aboutpage.eyebrow": "الملف",
    "aboutpage.r1": "عارضة", "aboutpage.r2": "صانعة محتوى", "aboutpage.r3": "سفيرة علامات",
    "aboutpage.edEyebrow": "المرأة خلف العمل",
    "aboutpage.viewServices": "استكشف الخدمات",
    "aboutpage.specEyebrow": "التخصصات",
    "aboutpage.specTitle": "ما <em>تتقنه</em>.",
    "aboutpage.expEyebrow": "حملات وعملاء مختارون",
    "aboutpage.expTitle": "أعمال <em>موثوقة</em>.",
    "home.aboutMore": "اقرأ الملف كاملًا",
    "home.servicesMore": "كل الخدمات",

    "cta.title": "لنصنع معًا<br>شيئًا <em>لا يُنسى</em>.",
    "cta.sub": "شارك التفاصيل واستلم عرضًا مخصصًا — حجوزات، وحملات، وسفارة علامات، وتمثيل شركات في الخليج وخارجه.",
    "cta.button": "اطلب حجزًا",

    "footer.tagline": "عارضة أزياء · سفيرة علامات · ممثلة رسمية للشركات. الرياض، المملكة العربية السعودية.",
    "footer.eyebrow": "لنبدأ الحديث",
    "footer.lead": "لنصنع<br>شيئًا <em>استثنائيًا</em>.",
    "footer.start": "ابدأ مشروعًا",
    "footer.explore": "استكشف", "footer.contact": "تواصل",
    "footer.book": "اطلب حجزًا", "footer.rights": "جميع الحقوق محفوظة.",
    "footer.studio": "دخول الاستوديو",

    "cat.within": "ضمن هذا التخصص", "cat.book": "احجز هذا التخصص",
    "cat.selected": "أعمال مختارة — انقر الصورة للتكبير",
    "cat.prev": "← السابق", "cat.next": "التالي →",
    "cat.ctaTitle": "جاهز <em>للتعاون؟</em>", "cat.ctaSub": "شارك حملتك واستلم عرضًا مخصصًا.",

    "book.eyebrow": "الحجز والتعاون",
    "book.title": "أخبرنا عن<br><em>التفاصيل.</em>",
    "book.note": "شارك بعض التفاصيل وستستلم عرضًا مخصصًا — يشمل التوفر، ولأعمال تمثيل الشركات عرض سعر بحسب الحدث ومدة الحضور.",
    "book.email": "البريد", "book.phone": "الهاتف", "book.social": "التواصل", "book.based": "المقر",
    "book.fBrand": "العلامة / الشركة", "book.fBrandPh": "اسم علامتك التجارية",
    "book.fContact": "اسم المسؤول", "book.fContactPh": "بمن نتواصل؟",
    "book.fEmail": "البريد الإلكتروني", "book.fEmailPh": "name@company.com",
    "book.fPhone": "الهاتف",
    "book.fType": "نوع التعاون", "book.fTypePh": "اختر التخصص",
    "book.fDate": "التاريخ المفضّل", "book.fDatePh": "مثال: ١٢–١٤ أكتوبر، أو مرن",
    "book.fBudget": "نطاق الميزانية", "book.fBudgetPh": "اختر نطاقًا",
    "book.fLocation": "الموقع", "book.fLocationPh": "المدينة / الاستوديو / الموقع",
    "book.fMessage": "عن المشروع", "book.fMessagePh": "أخبرنا عن الحملة والمخرجات والاستخدام والجدول الزمني.",
    "book.hint": "معاينة واجهة — هذا النموذج غير مرتبط بخادم بعد.",
    "book.send": "أرسل الطلب",
    "book.typeNotSure": "غير متأكد — انصحني",
    "book.successT": "تم استلام الطلب.",
    "book.successP": "شكرًا لك. تم تسجيل طلبك — ستصلك ردّنا خلال يومي عمل بالتوفر وعرض مخصص.",
    "book.successCta": "العودة إلى الأعمال",
    "connect.eyebrow": "تواصل فوري",
    "connect.note": "امسح الرمز لفتح الأعمال، أو شاركها مباشرة.",
    "connect.copy": "نسخ الرابط", "connect.share": "مشاركة الملف", "connect.copied": "تم النسخ ✓",
  },
};

(function () {
  const LANG_KEY = "noor-lang";
  function getLang() {
    try { return localStorage.getItem(LANG_KEY) || "en"; } catch (e) { return "en"; }
  }
  function dict() { return I18N[getLang()] || I18N.en; }

  window.t = function (key) { return (dict()[key] != null ? dict()[key] : (I18N.en[key] != null ? I18N.en[key] : key)); };
  window.currentLang = getLang;

  function apply(lang) {
    const d = I18N[lang] || I18N.en;
    const html = document.documentElement;
    html.classList.add("theming"); // reuse the smooth cross-fade
    html.setAttribute("lang", lang);
    html.setAttribute("dir", d.dir);
    document.querySelectorAll("[data-i18n]").forEach((el) => {
      const k = el.getAttribute("data-i18n");
      if (d[k] != null) el.innerHTML = d[k];
    });
    document.querySelectorAll("[data-i18n-ph]").forEach((el) => {
      const k = el.getAttribute("data-i18n-ph");
      if (d[k] != null) el.setAttribute("placeholder", d[k]);
    });
    document.querySelectorAll(".lang-toggle").forEach((b) => (b.textContent = d["lang.toggle"]));
    setTimeout(() => html.classList.remove("theming"), 650);
    window.dispatchEvent(new CustomEvent("langchange", { detail: { lang } }));
  }

  window.setLang = function (lang) {
    try { localStorage.setItem(LANG_KEY, lang); } catch (e) {}
    apply(lang);
  };

  function injectToggle() {
    const nav = document.querySelector(".nav");
    if (nav && !nav.querySelector(".lang-toggle")) {
      const btn = document.createElement("button");
      btn.className = "lang-toggle";
      btn.setAttribute("aria-label", "Switch language / تغيير اللغة");
      btn.textContent = dict()["lang.toggle"];
      btn.addEventListener("click", () => window.setLang(getLang() === "en" ? "ar" : "en"));
      const burger = nav.querySelector(":scope > .nav__burger");
      if (burger) nav.insertBefore(btn, burger); else nav.appendChild(btn);
    }
    // dashboard sidebar slot
    const slot = document.querySelector("#langSlot");
    if (slot && !slot.querySelector(".lang-toggle")) {
      const btn2 = document.createElement("button");
      btn2.className = "lang-toggle";
      btn2.textContent = dict()["lang.toggle"];
      btn2.addEventListener("click", () => window.setLang(getLang() === "en" ? "ar" : "en"));
      slot.appendChild(btn2);
    }
  }

  function init() { injectToggle(); apply(getLang()); }
  if (document.readyState === "loading") document.addEventListener("DOMContentLoaded", init);
  else init();
})();
