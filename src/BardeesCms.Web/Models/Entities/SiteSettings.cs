namespace BardeesCms.Web.Models.Entities;

/// <summary>Global, site-wide settings. Singleton row (Id = 1).</summary>
public class SiteSettings : AuditableEntity
{
    // Identity / branding
    public string SiteName { get; set; } = "";
    public string? SiteNameAr { get; set; }
    public string BrandName { get; set; } = "";
    public string? BrandNameAr { get; set; }
    public string? BrandNameShort { get; set; }
    public string? BrandNameShortAr { get; set; }
    public string? Role { get; set; }
    public string? RoleAr { get; set; }
    public string? Tagline { get; set; }
    public string? TaglineAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }

    public string? Logo { get; set; }
    public string? Favicon { get; set; }

    // Contact
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? WhatsApp { get; set; }
    public string? Location { get; set; }
    public string? LocationAr { get; set; }

    public string? CopyrightText { get; set; }
    public string? CopyrightTextAr { get; set; }

    // Default SEO
    public string? DefaultMetaTitle { get; set; }
    public string? DefaultMetaDescription { get; set; }
    public string? DefaultOgImage { get; set; }

    // Social links
    public string? InstagramUrl { get; set; }
    public string? TikTokUrl { get; set; }
    public string? FacebookUrl { get; set; }
    public string? SnapchatUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? YouTubeUrl { get; set; }

    // Booking
    public string? BookingEmail { get; set; }
    public string? BookingWhatsApp { get; set; }

    // Analytics (optional, not a hard dependency)
    public string? GoogleAnalyticsId { get; set; }
    public string? GoogleTagManagerId { get; set; }

    // Advanced (SuperAdmin only)
    public string? CustomCss { get; set; }
    public string? CustomJs { get; set; }
}
