/* =============================================================================
   DASHBOARD MOCK DATA
   Front-end only. Replace with API calls when the backend is ready.
   ============================================================================= */

const BOOKINGS = [
  { id: "BK-1042", brand: "Marmara Marble Co.", contact: "Layla Haddad", email: "layla@marmara.co", type: "Brand Ambassador", date: "2026-09-18", budget: "40,000+ SAR", status: "confirmed", createdAt: "2026-09-02", message: "12-month ambassador renewal — new quarry collection film + print." },
  { id: "BK-1041", brand: "Rawi Desserts", contact: "Omar Nasser", email: "omar@rawi.sa", type: "Food & Beverage", date: "2026-09-12", budget: "15,000 – 40,000 SAR", status: "in-review", createdAt: "2026-09-04", message: "New pistachio line — hero shots + 3 reels for launch week." },
  { id: "BK-1040", brand: "Lumière Aesthetics", contact: "Dr. Sara Fahad", email: "sara@lumiere.clinic", type: "Medical & Wellness", date: "2026-10-02", budget: "15,000 – 40,000 SAR", status: "new", createdAt: "2026-09-05", message: "Clinic rebrand campaign, needs a trusted, warm face. Print + web." },
  { id: "BK-1039", brand: "Tawheed Telecom", contact: "Faisal Otaibi", email: "faisal@tawheed.sa", type: "Commercial", date: "2026-09-23", budget: "40,000+ SAR", status: "new", createdAt: "2026-09-05", message: "National Day TVC, 30s + cutdowns. Studio in Riyadh." },
  { id: "BK-1038", brand: "Maison Abaya", contact: "Noura Salem", email: "noura@maisonabaya.com", type: "Beauty & Fashion", date: "2026-09-15", budget: "15,000 – 40,000 SAR", status: "confirmed", createdAt: "2026-08-29", message: "Autumn abaya lookbook, 18 looks, half-day + full-day option." },
  { id: "BK-1037", brand: "Expo Gulf 2026", contact: "Khalid Amir", email: "khalid@expogulf.ae", type: "Corporate Representative", date: "2026-11-04", budget: "To be discussed", status: "in-review", createdAt: "2026-08-27", message: "3-day exhibition booth representation + client meetings. Quote per day." },
  { id: "BK-1036", brand: "Qahwa House", contact: "Reem Ali", email: "reem@qahwa.house", type: "Food & Beverage", date: "2026-08-30", budget: "5,000 – 15,000 SAR", status: "declined", createdAt: "2026-08-20", message: "Café opening — dates clashed, proposed reschedule." },
  { id: "BK-1035", brand: "Layali TV", contact: "Yousef Karim", email: "yousef@layali.tv", type: "Acting & TV", date: "2026-10-19", budget: "40,000+ SAR", status: "new", createdAt: "2026-09-01", message: "Guest role, 2 episodes of a ramadan drama. Script attached." },
  { id: "BK-1034", brand: "Chronos Watches", contact: "Dana Riyad", email: "dana@chronos.sa", type: "Beauty & Fashion", date: "2026-09-09", budget: "15,000 – 40,000 SAR", status: "confirmed", createdAt: "2026-08-18", message: "Hand model, new automatic line. Macro + lifestyle." },
  { id: "BK-1033", brand: "Nabta Wellness", contact: "Hala Zayd", email: "hala@nabta.health", type: "Medical & Wellness", date: "2026-08-24", budget: "5,000 – 15,000 SAR", status: "declined", createdAt: "2026-08-10", message: "Budget below rate for scope. Kept warm for future." },
];

const MESSAGES = [
  { from: "Layla Haddad", brand: "Marmara Marble Co.", time: "2h ago", unread: true, text: "Contract signed — sending the shoot schedule for the 18th today." },
  { from: "Dr. Sara Fahad", brand: "Lumière Aesthetics", time: "5h ago", unread: true, text: "Loved the portfolio. Are you available for a call this week?" },
  { from: "Faisal Otaibi", brand: "Tawheed Telecom", time: "1d ago", unread: false, text: "Can you share usage terms for a 6-month broadcast buyout?" },
  { from: "Khalid Amir", brand: "Expo Gulf 2026", time: "2d ago", unread: false, text: "What is your day rate for on-stand representation?" },
];

// booking counts over the last 6 months (for the chart)
const MONTHLY = [
  { m: "Apr", v: 6 }, { m: "May", v: 9 }, { m: "Jun", v: 7 },
  { m: "Jul", v: 12 }, { m: "Aug", v: 15 }, { m: "Sep", v: 18 },
];

const STATUS_LABELS = {
  "new": "New",
  "in-review": "In review",
  "confirmed": "Confirmed",
  "declined": "Declined",
};
