using BardeesCms.Web.Models.Entities;
using BardeesCms.Web.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Data;

/// <summary>
/// Seeds the CMS with the content of the original static site (data.js + i18n.js),
/// faithfully preserving EN/AR copy. Each block is idempotent — it only runs when
/// its table is empty, so re-running never duplicates data.
/// </summary>
public static class ContentSeed
{
    public static async Task SeedAsync(ApplicationDbContext db, ILogger logger)
    {
        var now = DateTime.UtcNow;

        await SeedSiteSettings(db, now);
        await SeedHero(db, now);
        await SeedAbout(db, now);
        await SeedPageSections(db, now);
        await SeedTrustedBrands(db);
        await SeedBrands(db, now);
        await SeedMarketPositions(db, now);
        await SeedDisciplinesAndProjects(db, now);
        await SeedServices(db, now);
        await SeedAbayas(db, now);
        await SeedCollaborationSteps(db, now);
        await SeedFaqs(db, now);
        await SeedVideos(db, now);
        await SeedContactSettings(db, now);
        await SeedNavigation(db);
        await SeedSeoPages(db, now);

        await db.SaveChangesAsync();
        logger.LogInformation("Content seed complete.");
    }

    private static async Task SeedSiteSettings(ApplicationDbContext db, DateTime now)
    {
        if (await db.SiteSettings.AnyAsync()) return;
        db.SiteSettings.Add(new SiteSettings
        {
            SiteName = "BARDEES ISSA",
            SiteNameAr = "برديس عيسى",
            BrandName = "BARDEES ISSA",
            BrandNameAr = "برديس عيسى",
            BrandNameShort = "Bardees",
            BrandNameShortAr = "برديس",
            Role = "Model · Actress · Brand Ambassador",
            RoleAr = "عارضة أزياء · ممثلة · وجه إعلاني",
            Tagline = "Based in Saudi Arabia — Available across the GCC.",
            TaglineAr = "مقيمة في السعودية — متاحة عبر الخليج.",
            Description = "BARDEES ISSA — model, actress and brand ambassador. Commercial campaigns, fashion, beauty, lifestyle and acting for brands across Saudi Arabia and the GCC.",
            DescriptionAr = "برديس عيسى — عارضة أزياء وممثلة ووجه إعلاني. حملات تجارية وأزياء وجمال ولايف ستايل وتمثيل للعلامات في السعودية والخليج.",
            Favicon = "assets/brand/favicon.svg",
            Email = "hello@bardeesissa.com",       // placeholder — client to provide
            Phone = "+966 56 576 8902",
            WhatsApp = "966565768902",                // digits only, incl. country code (for wa.me)
            Location = "Saudi Arabia",
            LocationAr = "السعودية",
            CopyrightText = "BARDEES ISSA",
            CopyrightTextAr = "برديس عيسى",
            DefaultMetaTitle = "BARDEES ISSA — Model · Actress · Brand Ambassador",
            DefaultMetaDescription = "Model, actress and brand ambassador based in Saudi Arabia, available across the GCC. Commercial campaigns, fashion, beauty, lifestyle, events, content and acting.",
            InstagramUrl = "#",                        // placeholder — add in dashboard when ready
            TikTokUrl = "#",                           // placeholder — add in dashboard when ready
            SnapchatUrl = "#",                         // placeholder — add in dashboard when ready
            FacebookUrl = "#",                         // placeholder — add in dashboard when ready
            YouTubeUrl = "https://www.youtube.com/@BardeesIssa",
            BookingEmail = "hello@bardeesissa.com",
            BookingWhatsApp = "966565768902",
            CreatedAt = now
        });
    }

    private static async Task SeedHero(ApplicationDbContext db, DateTime now)
    {
        if (await db.HeroSections.AnyAsync()) return;
        var hero = new HeroSection
        {
            Eyebrow = "Model • Actress • Brand Ambassador",
            EyebrowAr = "عارضة أزياء • ممثلة • وجه إعلاني",
            Title = "BARDEES",
            TitleAr = "برديس",
            Subtitle = "ISSA",
            SubtitleAr = "عيسى",
            Description = "Based in Saudi Arabia — <em class=\"serif-em\" style=\"font-style:normal\">available for brands & campaigns across the GCC.</em> Commercial, fashion, beauty, lifestyle, content and acting.",
            DescriptionAr = "مقيمة في السعودية — <em class=\"serif-em\" style=\"font-style:normal\">متاحة للعلامات والحملات عبر الخليج.</em> إعلانات وأزياء وجمال ولايف ستايل ومحتوى وتمثيل.",
            Sectors = "Commercial · Fashion · Beauty · Lifestyle · Acting",
            SectorsAr = "إعلانات · أزياء · جمال · لايف ستايل · تمثيل",
            PrimaryButtonText = "View my work",
            PrimaryButtonTextAr = "شاهد أعمالي",
            PrimaryButtonUrl = "#work",
            SecondaryButtonText = "Work with me",
            SecondaryButtonTextAr = "لنعمل معاً",
            SecondaryButtonUrl = "/contact",
            FeaturedLabel = "The Index — Issue 01",
            FeaturedLabelAr = "المحتوى — العدد 01",
            IssueNumber = "01",
            IsActive = true,
            CreatedAt = now
        };
        var slides = new (string img, string en, string ar)[]
        {
            ("couture-01", "Modest Couture", "أزياء محتشمة"),
            ("fashion-olive", "Editorial Fashion", "أزياء تحريرية"),
            ("fashion-blazer-stand", "Signature Style", "أسلوب مميّز"),
            ("fashion-floral", "Resort Print", "نقوش صيفية"),
            ("couture-03", "Couture Campaign", "حملة كوتور"),
        };
        var order = 0;
        foreach (var (img, en, ar) in slides)
            hero.Slides.Add(new HeroSlide { Image = img, Label = en, LabelAr = ar, DisplayOrder = order++, IsActive = true });
        db.HeroSections.Add(hero);
    }

    private static async Task SeedAbout(ApplicationDbContext db, DateTime now)
    {
        if (await db.AboutSections.AnyAsync()) return;
        var about = new AboutSection
        {
            Eyebrow = "About Bardees",
            EyebrowAr = "عن برديس",
            Title = "Model, actress &amp; <em>brand ambassador</em>.",
            TitleAr = "عارضة، ممثلة <em>ووجه إعلاني</em>.",
            Description = "A Saudi-based model, actress and brand ambassador working with brands across <em>Saudi Arabia and the GCC.</em>",
            DescriptionAr = "عارضة أزياء وممثلة ووجه إعلاني مقيمة في السعودية، تعمل مع العلامات في <em>السعودية والخليج.</em>",
            LongDescription = "With experience in commercial campaigns, fashion, beauty, lifestyle, food &amp; beverage and medical brands, Bardees brings a professional on-camera presence and a strong understanding of the Gulf market. Available for advertising campaigns, commercial shoots, brand partnerships, events, social-media content and acting projects.",
            LongDescriptionAr = "بخبرة في الحملات التجارية والأزياء والجمال واللايف ستايل والأطعمة والمشروبات والعلامات الطبية، تقدّم برديس حضوراً احترافياً أمام الكاميرا وفهماً قوياً للسوق الخليجي. متاحة للحملات الإعلانية والتصوير التجاري وشراكات العلامات والفعاليات ومحتوى السوشيال ميديا وأعمال التمثيل.",
            Image = "couture-01",
            Location = "Riyadh",
            LocationAr = "الرياض",
            ButtonText = "Read the full profile",
            ButtonTextAr = "اقرأ الملف الكامل",
            ButtonUrl = "/about",
            IsActive = true,
            CreatedAt = now
        };
        var facts = new (string l, string lav, string v, string vav)[]
        {
            ("Based in", "الإقامة", "Saudi Arabia", "السعودية"),
            ("Languages", "اللغات", "Arabic · English", "العربية · الإنجليزية"),
            ("Dialect", "اللهجة", "Saudi Arabic", "اللهجة السعودية"),
            ("Availability", "التغطية", "GCC & International", "الخليج ودولياً"),
        };
        var order = 0;
        foreach (var (l, lav, v, vav) in facts)
            about.Facts.Add(new AboutFact { Label = l, LabelAr = lav, Value = v, ValueAr = vav, DisplayOrder = order++, IsActive = true });
        db.AboutSections.Add(about);
    }

    private static async Task SeedPageSections(ApplicationDbContext db, DateTime now)
    {
        if (await db.PageSections.AnyAsync()) return;
        var sections = new (string key, string page, string eb, string ebAr, string t, string tAr, string? note, string? noteAr)[]
        {
            ("trusted", "home", "Trusted by brands across the Gulf", "علامات تثق بها عبر الخليج", "", "", null, null),
            ("clients", "home", "Clients & ambassadorships", "عملاء وسفارات", "Brands I've been the <em>face</em> of.", "علامات كنت <em>وجهها</em>.",
                "A short register of the houses I've represented — as ambassador and advertising face — across the Kingdom and the Gulf.",
                "سجل موجز للعلامات التي مثّلتها — كسفيرة ووجه إعلاني — في المملكة والخليج."),
            ("why", "home", "Why Bardees for the Saudi market", "لماذا برديس للسوق السعودي", "Built for the <em>Saudi audience.</em>", "مصمّمة <em>للجمهور السعودي.</em>",
                "Helping brands enter, connect with, and grow within the Saudi market.",
                "أساعد العلامات على الدخول إلى السوق السعودي والتواصل معه والنمو فيه."),
            ("disciplines", "home", "Contents — select a discipline", "المحتوى — اختر تخصّصاً", "Choose the kind of<br><em>collaboration</em> you need.", "اختر نوع <em>التعاون</em> الذي تحتاجه.",
                "Every brand enters differently. Pick a category to see the work that speaks to your campaign.",
                "كل علامة تدخل بطريقة مختلفة. اختر فئة لترى الأعمال التي تناسب حملتك."),
            ("work", "home", "Selected — Issue 01", "مختارات — العدد 01", "Selected <em>work</em>", "أعمال <em>مختارة</em>", null, null),
            ("showreel", "home", "Showreel", "شوريل", "See me <em>in motion</em>.", "شاهدني <em>في حركة</em>.",
                "A short cut across campaigns, content and events.", "لقطة قصيرة من الحملات والمحتوى والفعاليات."),
            ("services", "home", "What I offer", "ما أقدّمه", "Services, <em>tailored</em>.", "خدمات <em>مصمّمة</em>.", null, null),
            ("process", "home", "How a collaboration works", "كيف يتم التعاون", "Four steps, <em>zero friction</em>.", "أربع خطوات، <em>بلا تعقيد</em>.", null, null),
            ("faq", "home", "Good to know", "معلومات مفيدة", "Questions, <em>answered</em>.", "أسئلة، <em>وأجوبتها</em>.", null, null),
            ("abaya", "home", "Bardees Abaya Collection", "مجموعة عبايات برديس", "Designed &amp; tailored by <em>Bardees</em>.", "تصميم وتفصيل <em>برديس</em>.",
                "A personal line of abayas — ready-to-wear and made-to-measure. Ordered directly over WhatsApp.",
                "خط عبايات خاص — جاهزة وتفصيل حسب الطلب. تُطلب مباشرةً عبر واتساب."),
            ("abaya-page", "abayas", "Bardees Abaya Collection", "مجموعة عبايات برديس", "Designed &amp; tailored by <em>Bardees</em>.", "تصميم وتفصيل <em>برديس</em>.",
                "A personal line of abayas — from ready-to-wear pieces to made-to-measure tailoring. Order directly over WhatsApp.",
                "خط عبايات خاص — من القطع الجاهزة إلى التفصيل حسب الطلب. اطلبي مباشرةً عبر واتساب."),
            ("cta", "home", "Let's work together", "لنعمل معاً", "Let's create content that reflects<br>your <em>brand's identity</em>.", "لنصنع محتوى يعكس <em>هوية علامتك</em>.",
                "Book your collaboration with Bardees — campaigns, commercial shoots, ambassadorships, events, content and acting across Saudi Arabia and the GCC.",
                "احجز تعاونك مع برديس — حملات وتصوير تجاري وسفارات علامات وفعاليات ومحتوى وتمثيل في السعودية والخليج."),
        };
        var order = 0;
        foreach (var s in sections)
            db.PageSections.Add(new PageSection
            {
                Key = s.key, Page = s.page, Eyebrow = s.eb, EyebrowAr = s.ebAr,
                Title = s.t, TitleAr = s.tAr, Note = s.note, NoteAr = s.noteAr,
                DisplayOrder = order++, IsActive = true, CreatedAt = now
            });
    }

    private static async Task SeedTrustedBrands(ApplicationDbContext db)
    {
        if (await db.TrustedBrands.AnyAsync()) return;
        var brands = new (string name, string? em)[]
        {
            ("Calvin Klein", null),
            ("Coach", "New York"),
            ("Roberto Cavalli", "by Franck Muller"),
            ("Ferragamo", null),
            ("Alhomaidhi", "Watches"),
            ("MecroLine", null),
        };
        var order = 0;
        foreach (var (name, em) in brands)
            db.TrustedBrands.Add(new TrustedBrand { Name = name, Emphasis = em, DisplayOrder = order++, IsActive = true });
    }

    private static async Task SeedBrands(ApplicationDbContext db, DateTime now)
    {
        if (await db.Brands.AnyAsync()) return;
        var clients = new (string n, string nAr, string logo, string role, string roleAr, string sector, string sectorAr)[]
        {
            ("Alhomaidhi Watches", "الحميضي للساعات", "brand-alhomaidhi", "Brand Ambassador", "سفيرة العلامة", "Watches", "ساعات"),
            ("Alhomaidhi Group", "مجموعة الحميضي", "brand-alhomaidhi-group", "Brand Ambassador", "سفيرة العلامة", "Group", "مجموعة"),
            ("MecroLine", "ميكرولاين", "brand-mecroline", "General Supplies", "توريدات عامة", "General Supplies", "توريدات عامة"),
        };
        var order = 0;
        foreach (var c in clients)
            db.Brands.Add(new Brand
            {
                Name = c.n, NameAr = c.nAr, Logo = c.logo, Role = c.role, RoleAr = c.roleAr,
                Sector = c.sector, SectorAr = c.sectorAr, DisplayOrder = order++, IsFeatured = true,
                IsActive = true, CreatedAt = now
            });
    }

    private static async Task SeedMarketPositions(ApplicationDbContext db, DateTime now)
    {
        if (await db.MarketPositions.AnyAsync()) return;
        var why = new (string t, string tAr, string b, string bAr)[]
        {
            ("Saudi Market Understanding", "فهم السوق السعودي", "I understand the Saudi audience and the right tone to reach them.", "أفهم طبيعة الجمهور السعودي وأسلوب التواصل المناسب معه."),
            ("Saudi Dialect", "اللهجة السعودية", "I deliver advertising content in the Saudi dialect — naturally and professionally.", "أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية."),
            ("Professional Brand Representation", "تمثيل احترافي للعلامة", "I represent brands with an elegant presence suited to companies, hotels, destinations and premium labels.", "أمثّل العلامة بصورة راقية تناسب الشركات والفنادق والوجهات والعلامات المميزة."),
            ("Cross-Market Communication", "التواصل بين الأسواق", "I help international and Gulf brands introduce themselves to the Saudi audience with impact.", "أساعد الشركات الدولية والخليجية على تقديم نفسها للجمهور السعودي بتأثير أكبر."),
            ("On-Camera Presence", "حضور أمام الكاميرا", "A confident on-camera presence built for advertising and commercial content.", "حضور واثق أمام الكاميرا مناسب للإعلانات والمحتوى التجاري."),
            ("Flexible Content Creation", "إنتاج محتوى مرن", "Content tailored for Instagram, TikTok, Snapchat and beyond.", "محتوى يناسب إنستغرام وتيك توك وسناب شات وغيرها."),
        };
        var order = 0;
        foreach (var w in why)
            db.MarketPositions.Add(new MarketPosition { Title = w.t, TitleAr = w.tAr, Description = w.b, DescriptionAr = w.bAr, DisplayOrder = order++, IsActive = true, CreatedAt = now });
    }

    private static async Task SeedServices(ApplicationDbContext db, DateTime now)
    {
        if (await db.Services.AnyAsync()) return;
        var services = new (string n, string t, string tAr, string d, string dAr)[]
        {
            ("01", "Modeling & Commercial Photography", "المودلينج والتصوير الإعلاني", "Modelling for commercial ads, fashion, beauty, lifestyle, products and brand campaigns across Saudi Arabia and the Gulf.", "موديل للإعلانات التجارية والأزياء والجمال وأسلوب الحياة والمنتجات والحملات الإعلانية للبراندات في السعودية والخليج."),
            ("02", "Acting", "التمثيل", "Professional on-camera performance for commercials, digital campaigns, TV series, theatre and branded content.", "أداء احترافي أمام الكاميرا للإعلانات التجارية والحملات الرقمية والمسلسلات والمسرح والمحتوى الخاص بالبراندات."),
            ("03", "Brand Ambassador", "الوجه الإعلاني للبراندات", "Representing brands through advertising campaigns, media appearances, digital content, events and long-term partnerships.", "تمثيل البراندات من خلال الحملات الإعلانية والظهور الإعلامي والمحتوى الرقمي والفعاليات والشراكات طويلة المدى."),
            ("04", "Wardrobe Design & Styling", "تفصيل وتنسيق الأزياء حسب هوية الإعلان", "Custom wardrobe designed and tailored to match the ad's identity, campaign concept and the brand's visual direction — colours, fabrics and details chosen for a cohesive look.", "تصميم وتفصيل أزياء مخصّصة تتناسب مع هوية الإعلان وفكرة الحملة والتوجه البصري للبراند — تُختار الألوان والخامات والتفاصيل لظهور متكامل ومتناسق."),
            ("05", "Brand & Event Coverage", "تغطية البراندات والفعاليات", "Coverage for brands, events, exhibitions, openings, product launches and activations — photography, video, on-camera presence and social-ready content.", "تغطية البراندات والفعاليات والمعارض والافتتاحات وإطلاق المنتجات والـActivations — تصوير فوتوغرافي وفيديو وظهور أمام الكاميرا ومحتوى جاهز للنشر."),
            ("06", "Content Creation", "صناعة المحتوى", "Creative content for TikTok, Instagram, Snapchat and beyond — ad videos, product showcases, lifestyle and UGC content.", "صناعة محتوى إبداعي لمنصات TikTok وInstagram وSnapchat وغيرها — فيديوهات إعلانية وعرض منتجات ومحتوى لايف ستايل ومحتوى UGC."),
            ("07", "Gulf Market Presence", "التمثيل والتواجد في السوق الخليجي", "Representing brands in the Saudi and Gulf market through local presence, advertising campaigns, content and commercial collaborations.", "تمثيل البراندات في السوق السعودي والخليجي من خلال الحضور المحلي والحملات الإعلانية وصناعة المحتوى والتعاونات التجارية."),
        };
        var taken = new HashSet<string>();
        var order = 0;
        foreach (var s in services)
            db.Services.Add(new Service
            {
                Number = s.n, Title = s.t, TitleAr = s.tAr, Slug = SlugHelper.Unique(s.t, taken),
                ShortDescription = s.d, ShortDescriptionAr = s.dAr,
                DisplayOrder = order++, IsFeatured = order <= 6, IsActive = true, CreatedAt = now
            });
    }

    private static async Task SeedAbayas(ApplicationDbContext db, DateTime now)
    {
        if (await db.Abayas.AnyAsync()) return;
        var taken = new HashSet<string>();
        // (image, name, nameAr, fabric, fabricAr, type) — images map to the 10 client photos.
        var abayas = new (string img, string n, string nAr, string fab, string fabAr, AbayaType type)[]
        {
            ("abaya-01", "Noir Botanical", "نوار بوتانيكال", "Crêpe · Leaf embroidery", "كريب · تطريز أوراق", AbayaType.ReadyToWear),
            ("abaya-02", "Taupe Palm", "بيج بالم", "Crêpe · Palm embroidery", "كريب · تطريز نخيل", AbayaType.ReadyToWear),
            ("abaya-03", "Gold Line", "الخط الذهبي", "Crêpe · Gold piping", "كريب · حواف ذهبية", AbayaType.ReadyToWear),
            ("abaya-04", "Bronze Paisley", "برونز بيزلي", "Crêpe · Beaded paisley", "كريب · تطريز بالخرز", AbayaType.Custom),
            ("abaya-05", "Cocoa Pleat", "كاكاو بليت", "Crêpe · Pleated cuffs", "كريب · أكمام مطويّة", AbayaType.ReadyToWear),
            ("abaya-06", "Gilded Trim", "التطريز الذهبي", "Crêpe · Gold beadwork", "كريب · خرز ذهبي", AbayaType.ReadyToWear),
            ("abaya-07", "Sage Embroidery", "سيج المطرزة", "Crêpe · Silver embroidery", "كريب · تطريز فضي", AbayaType.Custom),
            ("abaya-08", "Onyx Geometric", "أونيكس الهندسية", "Crêpe · Geometric embroidery", "كريب · تطريز هندسي", AbayaType.Custom),
            ("abaya-09", "Ivory Rose", "آيفوري روز", "Crêpe · Floral embroidery", "كريب · تطريز زهري", AbayaType.ReadyToWear),
            ("abaya-10", "Merlot Contrast", "ميرلو", "Crêpe · Contrast stitch", "كريب · حياكة متباينة", AbayaType.Custom),
        };
        var order = 0;
        foreach (var a in abayas)
            db.Abayas.Add(new Abaya
            {
                Name = a.n, NameAr = a.nAr, Slug = SlugHelper.Unique(a.n, taken),
                Fabric = a.fab, FabricAr = a.fabAr, Type = a.type,
                CoverImage = a.img, ThumbnailImage = a.img,
                DisplayOrder = order++, IsFeatured = order <= 4, IsActive = true, CreatedAt = now
            });
    }

    private static async Task SeedCollaborationSteps(ApplicationDbContext db, DateTime now)
    {
        if (await db.CollaborationSteps.AnyAsync()) return;
        var steps = new (string n, string t, string tAr, string d, string dAr)[]
        {
            ("01", "Choose a discipline", "اختر التخصّص", "Pick the category that fits your campaign — from hand model to corporate representation.", "اختر الفئة التي تناسب حملتك — من عارضة يد إلى تمثيل مؤسسي."),
            ("02", "Share the brief", "شارك الملخّص", "Dates, deliverables, usage rights and budget. Note: wardrobe is styled to suit each ad.", "التواريخ والمخرجات وحقوق الاستخدام والميزانية. ملاحظة: يُنسّق الزي ليلائم كل إعلان."),
            ("03", "Receive a proposal", "استلم العرض", "Availability and a tailored quote — priced per assignment for corporate work.", "التوفّر وعرض سعر مخصّص — بالتسعير لكل مهمة في الأعمال المؤسسية."),
            ("04", "Create", "ننفّذ", "On set, on stage, or in the room — one consistent, professional presence.", "على موقع التصوير أو المسرح أو في القاعة — حضور واحد ثابت واحترافي."),
        };
        var order = 0;
        foreach (var s in steps)
            db.CollaborationSteps.Add(new CollaborationStep { StepNumber = s.n, Title = s.t, TitleAr = s.tAr, Description = s.d, DescriptionAr = s.dAr, DisplayOrder = order++, IsActive = true, CreatedAt = now });
    }

    private static async Task SeedFaqs(ApplicationDbContext db, DateTime now)
    {
        if (await db.Faqs.AnyAsync()) return;
        var faqs = new (string q, string qAr, string a, string aAr)[]
        {
            ("How do rates work?", "كيف يتم التسعير؟", "Advertising shoots are quoted by scope and usage. Corporate representation is priced per assignment — by the nature of the event and the duration of attendance.", "تُسعّر جلسات التصوير الإعلاني حسب النطاق والاستخدام. أما التمثيل المؤسسي فيُسعّر لكل مهمة — حسب طبيعة الحدث ومدة الحضور."),
            ("Do you work in the Saudi dialect?", "هل تعملين باللهجة السعودية؟", "Yes — I deliver advertising content in the Saudi dialect, naturally and professionally, to reach the Saudi audience.", "نعم — أقدّم المحتوى الإعلاني باللهجة السعودية بطريقة طبيعية واحترافية للوصول إلى الجمهور السعودي."),
            ("How is wardrobe handled?", "كيف يُدار الزي؟", "Wardrobe is styled to suit each ad — plain and refined for a product like marble, or more expressive and fashion-led where the brand calls for it.", "يُنسّق الزي ليلائم كل إعلان — بسيط وراقٍ لمنتج مثل الرخام، أو أكثر تعبيراً وأزياءً حين تتطلّب العلامة ذلك."),
            ("Do you travel across the GCC?", "هل تسافرين عبر الخليج؟", "Yes — based in Saudi Arabia and available across the GCC. Travel is arranged per booking.", "نعم — مقيمة في السعودية ومتاحة عبر الخليج. يُرتّب السفر لكل حجز."),
            ("How far ahead should we book?", "كم من الوقت مسبقاً يجب الحجز؟", "Two to three weeks is ideal, though rush bookings are considered subject to availability.", "من أسبوعين إلى ثلاثة مثالية، مع إمكانية النظر في الحجوزات العاجلة حسب التوفّر."),
        };
        var order = 0;
        foreach (var f in faqs)
            db.Faqs.Add(new Faq { Question = f.q, QuestionAr = f.qAr, Answer = f.a, AnswerAr = f.aAr, DisplayOrder = order++, IsActive = true, CreatedAt = now });
    }

    private static async Task SeedVideos(ApplicationDbContext db, DateTime now)
    {
        if (await db.VideoCategories.AnyAsync()) return;
        var cats = new (string key, string en, string ar)[]
        {
            ("commercial-campaigns", "Commercial Campaigns", "الحملات الإعلانية"),
            ("modeling", "Modeling", "المودلينج"),
            ("acting", "Acting", "التمثيل"),
            ("bts", "Behind the Scenes", "كواليس التصوير"),
        };
        var catMap = new Dictionary<string, VideoCategory>();
        var o = 0;
        foreach (var c in cats)
        {
            var cat = new VideoCategory { Key = c.key, Name = c.en, NameAr = c.ar, DisplayOrder = o++, IsActive = true };
            catMap[c.key] = cat;
            db.VideoCategories.Add(cat);
        }

        // Real brand films (landscape). Then the client's 11 YouTube Shorts (portrait),
        // auto-distributed across the categories — the client re-sorts in the admin.
        var videos = new (string id, string cat, bool portrait, string t, string tAr, string? client, string? clientAr)[]
        {
            ("ZJl6__WQnSs", "commercial-campaigns", false, "منشار", "Manshar", "شركة الرخام", "Marble Co."),
            ("xrtEC_iYvek", "commercial-campaigns", false, "دانو 2", "Danho 2", "شركة الرخام", "Marble Co."),
            ("0xwe9BzBvEI", "commercial-campaigns", false, "فولاكس", "Volax", null, null),

            ("uxk1KrYPk_s", "commercial-campaigns", true, "Campaign Short 01", "إعلان قصير 01", null, null),
            ("RERaUPPEjmw", "commercial-campaigns", true, "Campaign Short 02", "إعلان قصير 02", null, null),
            ("aorNAN5xBLQ", "commercial-campaigns", true, "Campaign Short 03", "إعلان قصير 03", null, null),

            ("DAkRbMczxc0", "modeling", true, "Modeling Short 01", "مودلينج 01", null, null),
            ("G20Clz2Azks", "modeling", true, "Modeling Short 02", "مودلينج 02", null, null),
            ("CpINHrP_AZM", "modeling", true, "Modeling Short 03", "مودلينج 03", null, null),

            ("T3bY4sxZo8k", "acting", true, "Acting Short 01", "تمثيل 01", null, null),
            ("UgLz09iwgBg", "acting", true, "Acting Short 02", "تمثيل 02", null, null),

            ("X4uGZAA0r4U", "bts", true, "Behind the Scenes 01", "كواليس 01", null, null),
            ("rX-YX2P5Reo", "bts", true, "Behind the Scenes 02", "كواليس 02", null, null),
            ("O6BBgc6UdQA", "bts", true, "Behind the Scenes 03", "كواليس 03", null, null),
        };
        var vo = 0;
        foreach (var v in videos)
            db.Videos.Add(new Video
            {
                Title = v.t, TitleAr = v.tAr, Client = v.client, ClientAr = v.clientAr,
                Provider = VideoProvider.YouTube, ProviderVideoId = v.id,
                VideoUrl = $"https://youtu.be/{v.id}", IsPortrait = v.portrait,
                VideoCategory = catMap[v.cat], DisplayOrder = vo++, IsFeatured = vo <= 3,
                IsPublished = true, CreatedAt = now
            });
    }

    private static async Task SeedContactSettings(ApplicationDbContext db, DateTime now)
    {
        if (await db.ContactSettings.AnyAsync()) return;
        db.ContactSettings.Add(new ContactSettings
        {
            Title = "Looking for a model, actress or advertising face for your next campaign?",
            TitleAr = "هل تبحث عن عارضة أزياء أو ممثلة أو وجه إعلاني لحملتك القادمة؟",
            Description = "Book with Bardees. Send your brief here and I'll reply on WhatsApp or by email — the fastest way to reach me is the WhatsApp button.",
            DescriptionAr = "احجز مع برديس. أرسل ملخّصك من هنا وسأرد عبر واتساب أو البريد — وأسرع طريقة للوصول إليّ هي زر واتساب.",
            Email = "hello@bardeesissa.com",         // placeholder — client to provide
            Phone = "+966 56 576 8902",
            WhatsApp = "966565768902",                  // digits only, incl. country code (for wa.me)
            Location = "Saudi Arabia · GCC",
            LocationAr = "السعودية · الخليج",
            Instagram = "#",                            // placeholder — add in dashboard when ready
            TikTok = "#",                               // placeholder — add in dashboard when ready
            BookingText = "Book with Bardees",
            BookingTextAr = "احجز مع برديس",
            BookingButtonText = "Book",
            BookingButtonTextAr = "احجز",
            BookingUrl = "/contact",
            IsActive = true,
            CreatedAt = now
        });
    }

    private static async Task SeedNavigation(ApplicationDbContext db)
    {
        if (await db.NavigationItems.AnyAsync()) return;
        var header = new (string t, string tAr, string url)[]
        {
            ("Home", "الرئيسية", "/"),
            ("Work", "الأعمال", "/#work"),
            ("About", "عن برديس", "/about"),
            ("Services", "الخدمات", "/services"),
            ("Abayas", "العبايات", "/abayas"),
            ("Videos", "الفيديوهات", "/videos"),
            ("Contact", "تواصل", "/contact"),
        };
        var o = 0;
        foreach (var h in header)
            db.NavigationItems.Add(new NavigationItem { Title = h.t, TitleAr = h.tAr, Url = h.url, Location = NavLocation.Header, DisplayOrder = o++, IsActive = true });

        var footer = new (string t, string tAr, string url, string grp, string grpAr)[]
        {
            ("Home", "الرئيسية", "/", "Explore", "استكشف"),
            ("Work", "الأعمال", "/work", "Explore", "استكشف"),
            ("About", "عن برديس", "/about", "Explore", "استكشف"),
            ("Services", "الخدمات", "/services", "Explore", "استكشف"),
            ("Abayas", "العبايات", "/abayas", "Explore", "استكشف"),
            ("Videos", "الفيديوهات", "/videos", "Explore", "استكشف"),
            ("Book with Bardees", "احجز مع برديس", "/contact", "Contact", "تواصل"),
        };
        o = 0;
        foreach (var f in footer)
            db.NavigationItems.Add(new NavigationItem { Title = f.t, TitleAr = f.tAr, Url = f.url, Location = NavLocation.Footer, Group = f.grp, GroupAr = f.grpAr, DisplayOrder = o++, IsActive = true });
    }

    private static async Task SeedSeoPages(ApplicationDbContext db, DateTime now)
    {
        if (await db.SeoPages.AnyAsync()) return;
        var pages = new (string name, string route, string title, string desc)[]
        {
            ("Home", "/", "BARDEES ISSA — Model · Actress · Brand Ambassador", "Model, actress and brand ambassador based in Saudi Arabia, available across the GCC. Commercial campaigns, fashion, beauty, lifestyle, events, content and acting."),
            ("About", "/about", "About — BARDEES ISSA", "A Saudi-based model, actress and brand ambassador working with brands across Saudi Arabia and the GCC."),
            ("Work", "/work", "Work — BARDEES ISSA", "Selected work across commercial, fashion & beauty, food & lifestyle, medical, brand ambassador and acting."),
            ("Services", "/services", "Services — BARDEES ISSA", "Modelling, acting, brand ambassadorship, wardrobe styling, brand & event coverage and content creation — tailored to the brief."),
            ("Abayas", "/abayas", "Abayas — Bardees Abaya Collection", "Bardees Abaya Collection — ready-to-wear and made-to-measure abayas designed and tailored by Bardees. Order over WhatsApp."),
            ("Videos", "/videos", "Videos — BARDEES ISSA", "Commercial campaigns, modelling, acting and behind-the-scenes films — filter by category and watch."),
            ("Contact", "/contact", "Contact — BARDEES ISSA", "Looking for a model, actress or advertising face? Book with Bardees over WhatsApp or email."),
        };
        foreach (var p in pages)
            db.SeoPages.Add(new SeoPage
            {
                PageName = p.name, Route = p.route, MetaTitle = p.title, MetaDescription = p.desc,
                OgTitle = p.title, OgDescription = p.desc, Robots = "index,follow", IsActive = true, CreatedAt = now
            });
    }

    private static async Task SeedDisciplinesAndProjects(ApplicationDbContext db, DateTime now)
    {
        if (await db.Disciplines.AnyAsync()) return;

        var projectSlugs = new HashSet<string>();

        // (n, slug, status, name, nameAr, tagline, taglineAr, desc, descAr, sub[], subAr[], cover, works[])
        var data = BuildDisciplineData();
        foreach (var d in data)
        {
            var disc = new Discipline
            {
                Number = d.N, Slug = d.Slug, Status = d.Status,
                Name = d.Name, NameAr = d.NameAr, Tagline = d.Tagline, TaglineAr = d.TaglineAr,
                Description = d.Desc, DescriptionAr = d.DescAr, CoverImage = d.Cover,
                DisplayOrder = d.Order, IsActive = true, CreatedAt = now
            };
            var so = 0;
            for (var i = 0; i < d.Sub.Length; i++)
                disc.SubItems.Add(new DisciplineSubItem { Name = d.Sub[i], NameAr = i < d.SubAr.Length ? d.SubAr[i] : null, DisplayOrder = so++ });

            var wo = 0;
            foreach (var w in d.Works)
            {
                var project = new PortfolioProject
                {
                    Title = w.Title, ClientName = w.Brand, Tag = w.Tag,
                    Slug = SlugHelper.Unique($"{d.Slug}-{w.Title}", projectSlugs),
                    CoverImage = w.Img, ThumbnailImage = w.Img,
                    DisplayOrder = wo++, IsFeatured = wo <= 3, IsPublished = true,
                    CreatedAt = now, Discipline = disc
                };
                if (w.IsVideo)
                    project.Media.Add(new PortfolioMedia { MediaType = MediaType.Video, FilePath = w.Video!, ThumbnailPath = w.Img, DisplayOrder = 0 });
                disc.Projects.Add(project);
            }
            db.Disciplines.Add(disc);
        }
    }

    // --- discipline dataset (mirrors CATEGORIES in data.js) -------------------------
    private record WorkSeed(string Title, string Brand, string Tag, string Img, bool IsVideo = false, string? Video = null);
    private record DiscSeed(string N, string Slug, DisciplineStatus Status, int Order, string Name, string NameAr,
        string Tagline, string TaglineAr, string Desc, string DescAr, string[] Sub, string[] SubAr, string Cover, WorkSeed[] Works);

    private static DiscSeed[] BuildDisciplineData() => new[]
    {
        new DiscSeed("01", "commercial", DisciplineStatus.Live, 0,
            "Commercial", "إعلانات تجارية",
            "Advertising that lands with the audience.", "إعلانات تصل إلى الجمهور.",
            "Commercial advertising and brand campaigns — including national-day and seasonal spots — delivered with a natural, persuasive on-camera presence.",
            "إعلانات تجارية وحملات للعلامات — من بينها إعلانات اليوم الوطني والمواسم — بحضور طبيعي ومؤثّر أمام الكاميرا.",
            new[] { "Commercial Advertising", "Brand Campaigns", "National-Day Spots", "Product Films" },
            new[] { "إعلانات تجارية", "حملات العلامات", "إعلانات اليوم الوطني", "أفلام المنتجات" },
            "fashion-olive",
            new[]
            {
                new WorkSeed("Olive Tailoring", "Campaign Look", "Commercial", "fashion-olive"),
                new WorkSeed("The Film", "Brand Showreel", "Commercial · Video", "fashion-pink", true, "assets/video/reel-b.mp4"),
                new WorkSeed("In-Store", "Retail Activation", "Brand Campaign", "ambassador-instore"),
            }),
        new DiscSeed("02", "beauty-fashion", DisciplineStatus.Live, 1,
            "Fashion & Beauty", "أزياء وجمال",
            "The face, the hand, the look.", "الوجه، واليد، والإطلالة.",
            "Fashion and beauty modelling — hand and watch campaigns, makeup and hair, editorial fashion, abayas and couture shoots. One versatile face across the looks a brand builds around.",
            "عرض أزياء وجمال — حملات اليد والساعات، ومكياج وشعر، وأزياء تحريرية، وعبايات وتصوير كوتور. وجه متعدد الإطلالات تبني عليه العلامات.",
            new[] { "Hand Model — Watches", "Makeup", "Hairstyle & Hair", "Fashion", "Abaya & Couture" },
            new[] { "عارضة يد — ساعات", "مكياج", "تسريحات وشعر", "أزياء", "عبايات وكوتور" },
            "beauty-01",
            new[]
            {
                new WorkSeed("Gold Hour", "Beauty Editorial", "Makeup · Glam", "beauty-01"),
                new WorkSeed("Soft Focus", "Beauty Portrait", "Beauty", "beauty-03"),
                new WorkSeed("Poise", "Beauty Story", "Skin · Glow", "beauty-02"),
                new WorkSeed("Pinstripe", "Editorial", "Fashion · Suiting", "fashion-blazer-stand"),
                new WorkSeed("Off Duty", "Editorial", "Fashion · Attitude", "fashion-blazer-seated"),
                new WorkSeed("Bloom", "Resort Story", "Fashion · Print", "fashion-floral"),
                new WorkSeed("Rosette", "Occasion", "Fashion", "fashion-pink"),
                new WorkSeed("Azure I", "Couture Kaftan", "Abaya · Couture", "couture-01"),
                new WorkSeed("Azure II", "Couture Kaftan", "Abaya · Couture", "couture-03"),
                new WorkSeed("Azure III", "Couture Kaftan", "Abaya · Couture", "couture-04"),
                new WorkSeed("Calvin Klein", "Watch Campaign", "Hand · Watches", "hand-calvinklein"),
                new WorkSeed("Coach", "New York — Watch", "Hand · Watches", "hand-coach"),
                new WorkSeed("Roberto Cavalli", "by Franck Muller", "Hand · Watches", "hand-cavalli"),
                new WorkSeed("Ferragamo", "Watch Campaign", "Hand · Watches", "hand-ferragamo"),
            }),
        new DiscSeed("03", "food", DisciplineStatus.Soon, 2,
            "Food & Lifestyle", "أطعمة ولايف ستايل",
            "Appetite & lifestyle, styled.", "شهية ولايف ستايل، بأسلوب.",
            "Food, beverage and lifestyle content for restaurants, cafés, dessert brands and F&B campaigns — appetizing, lifestyle-led content.",
            "محتوى للأطعمة والمشروبات واللايف ستايل للمطاعم والكافيهات وعلامات الحلويات وحملات الأطعمة — محتوى شهيّ بروح اللايف ستايل.",
            new[] { "Restaurants", "Cafés", "Dessert Brands", "Lifestyle Content" },
            new[] { "المطاعم", "الكافيهات", "الحلويات", "محتوى لايف ستايل" },
            "beauty-02", Array.Empty<WorkSeed>()),
        new DiscSeed("04", "medical", DisciplineStatus.Soon, 3,
            "Medical & Healthcare", "طبي وصحي", "Trust, on camera.", "الثقة، أمام الكاميرا.",
            "Content and representation for medical brands, dental clinics, medical centers and healthcare campaigns — a clean, reassuring, professional presence.",
            "محتوى وتمثيل للعلامات الطبية وعيادات الأسنان والمراكز الطبية والحملات الصحية — حضور نظيف ومطمئن واحترافي.",
            new[] { "Medical Brands", "Dental Clinics", "Medical Centers", "Healthcare" },
            new[] { "علامات طبية", "عيادات أسنان", "مراكز طبية", "حملات صحية" },
            "beauty-03", Array.Empty<WorkSeed>()),
        new DiscSeed("05", "ambassador", DisciplineStatus.Live, 4,
            "Brand Ambassador", "سفيرة علامات تجارية",
            "Representing brands across Saudi Arabia & the GCC.", "أمثّل العلامات في السعودية والخليج.",
            "The recognizable, credible face of a brand — in campaigns, at retail and across social. Ambassador for a Gulf marble company and for Alhomaidhi Watches, representing brands across Saudi Arabia and the GCC.",
            "الوجه المميّز والموثوق للعلامة — في الحملات وفي نقاط البيع وعلى السوشيال. سفيرة لشركة رخام خليجية ولساعات الحميضي، أمثّل العلامات في السعودية والخليج.",
            new[] { "Brand Ambassador", "Commercial Modeling", "Campaigns", "Social Media Content", "Events & Activations", "Product Launches" },
            new[] { "سفيرة علامة", "مودلينج تجاري", "الحملات", "محتوى السوشيال ميديا", "الفعاليات والتفعيلات", "إطلاق المنتجات" },
            "ambassador-instore",
            new[]
            {
                new WorkSeed("In-Store", "Alhomaidhi × Roberto Cavalli", "Brand Ambassador", "ambassador-instore"),
                new WorkSeed("Cavalli", "Watch House — Ambassador", "Ambassador", "hand-cavalli"),
            }),
        new DiscSeed("06", "acting", DisciplineStatus.Live, 5,
            "Acting", "تمثيل", "Presence that carries a scene.", "حضور يحمل المشهد.",
            "Acting and on-screen work across TV series and theatre, plus commercial acting. TV: Dialect Challenge, Love Again. Theatre: Hamdy & Hamdeya, The Will.",
            "تمثيل وأعمال أمام الشاشة في المسلسلات والمسرح، إضافة إلى التمثيل الإعلاني. تلفزيون: تحدي اللهجات، العشق مجددًا. مسرح: حمدي وحمدية، مسرحية الوصية.",
            new[] { "TV — Dialect Challenge", "TV — Love Again", "Theatre — Hamdy & Hamdeya", "Theatre — The Will", "Commercial Acting" },
            new[] { "تلفزيون — تحدي اللهجات", "تلفزيون — العشق مجددًا", "مسرح — حمدي وحمدية", "مسرح — مسرحية الوصية", "تمثيل إعلاني" },
            "fashion-blazer-seated", Array.Empty<WorkSeed>()),
    };
}
