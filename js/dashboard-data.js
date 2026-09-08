/* =============================================================================
   DASHBOARD MOCK DATA  (private studio demo — behind login gate)
   Front-end only sample inbox. Replace with API calls when the backend is ready.
   Brand names below are placeholder samples, not real clients.
   ============================================================================= */

const BOOKINGS = [
  { id: "BK-1042", brand: "Luxury Timepieces Co.", contact: "Layla Haddad", email: "layla@example.com", type: "Watches & Hand", date: "2026-09-18", budget: "40,000+ SAR", status: "confirmed", createdAt: "2026-09-02", message: "New automatic line — macro hand campaign + lifestyle set." },
  { id: "BK-1041", brand: "Atelier Kaftan", contact: "Omar Nasser", email: "omar@example.com", type: "Modest & Couture", date: "2026-09-12", budget: "15,000 – 40,000 SAR", status: "in-review", createdAt: "2026-09-04", message: "Autumn couture kaftan lookbook — 12 looks, half + full day." },
  { id: "BK-1040", brand: "Maison Beauté", contact: "Sara Fahad", email: "sara@example.com", type: "Beauty & Glam", date: "2026-10-02", budget: "15,000 – 40,000 SAR", status: "new", createdAt: "2026-09-05", message: "Skin + glow campaign — print and social, warm editorial tone." },
  { id: "BK-1039", brand: "Northline Brand", contact: "Faisal Otaibi", email: "faisal@example.com", type: "Ambassador & Commercial", date: "2026-09-23", budget: "40,000+ SAR", status: "new", createdAt: "2026-09-05", message: "Brand film 30s + cutdowns, studio in Riyadh." },
  { id: "BK-1038", brand: "Studio Editorial", contact: "Noura Salem", email: "noura@example.com", type: "Fashion & Editorial", date: "2026-09-15", budget: "15,000 – 40,000 SAR", status: "confirmed", createdAt: "2026-08-29", message: "Suiting editorial, 18 frames, half-day + full-day option." },
  { id: "BK-1037", brand: "Gulf Expo Pavilion", contact: "Khalid Amir", email: "khalid@example.com", type: "Ambassador & Commercial", date: "2026-11-04", budget: "To be discussed", status: "in-review", createdAt: "2026-08-27", message: "3-day exhibition representation + client meetings. Quote per day." },
  { id: "BK-1036", brand: "Rosewater Beauty", contact: "Reem Ali", email: "reem@example.com", type: "Beauty & Glam", date: "2026-08-30", budget: "5,000 – 15,000 SAR", status: "declined", createdAt: "2026-08-20", message: "Launch shoot — dates clashed, proposed reschedule." },
  { id: "BK-1035", brand: "Heritage Couture", contact: "Yousef Karim", email: "yousef@example.com", type: "Modest & Couture", date: "2026-10-19", budget: "40,000+ SAR", status: "new", createdAt: "2026-09-01", message: "Occasion abaya capsule — editorial + campaign stills." },
  { id: "BK-1034", brand: "Meridian Watches", contact: "Dana Riyad", email: "dana@example.com", type: "Watches & Hand", date: "2026-09-09", budget: "15,000 – 40,000 SAR", status: "confirmed", createdAt: "2026-08-18", message: "Hand model, dress-watch line. Macro + lifestyle." },
  { id: "BK-1033", brand: "Resort Label", contact: "Hala Zayd", email: "hala@example.com", type: "Fashion & Editorial", date: "2026-08-24", budget: "5,000 – 15,000 SAR", status: "declined", createdAt: "2026-08-10", message: "Budget below rate for scope. Kept warm for future." },
];

const MESSAGES = [
  { from: "Layla Haddad", brand: "Luxury Timepieces Co.", time: "2h ago", unread: true, text: "Contract signed — sending the shoot schedule for the 18th today." },
  { from: "Sara Fahad", brand: "Maison Beauté", time: "5h ago", unread: true, text: "Loved the portfolio. Are you available for a call this week?" },
  { from: "Faisal Otaibi", brand: "Northline Brand", time: "1d ago", unread: false, text: "Can you share usage terms for a 6-month broadcast buyout?" },
  { from: "Khalid Amir", brand: "Gulf Expo Pavilion", time: "2d ago", unread: false, text: "What is your day rate for on-stand representation?" },
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
