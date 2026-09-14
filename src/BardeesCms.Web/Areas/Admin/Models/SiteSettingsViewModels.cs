using System.ComponentModel.DataAnnotations;

namespace BardeesCms.Web.Areas.Admin.Models;

/// <summary>Editor for the global site settings singleton.</summary>
public class SiteSettingsFormVm
{
    public int Id { get; set; }

    // Branding
    [Required, StringLength(200), Display(Name = "Site name")]
    public string SiteName { get; set; } = "";
    [Display(Name = "Site name (Arabic)"), StringLength(200)]
    public string? SiteNameAr { get; set; }

    [Required, StringLength(200), Display(Name = "Brand name")]
    public string BrandName { get; set; } = "";
    [Display(Name = "Brand name (Arabic)"), StringLength(200)]
    public string? BrandNameAr { get; set; }

    [Display(Name = "Brand name (short)")]
    public string? BrandNameShort { get; set; }
    [Display(Name = "Brand name short (Arabic)")]
    public string? BrandNameShortAr { get; set; }

    [Display(Name = "Role")]
    public string? Role { get; set; }
    [Display(Name = "Role (Arabic)")]
    public string? RoleAr { get; set; }

    [Display(Name = "Tagline")]
    public string? Tagline { get; set; }
    [Display(Name = "Tagline (Arabic)")]
    public string? TaglineAr { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
    [Display(Name = "Description (Arabic)")]
    public string? DescriptionAr { get; set; }

    /// <summary>Existing stored logo path.</summary>
    public string? Logo { get; set; }
    [Display(Name = "Logo")]
    public IFormFile? LogoFile { get; set; }

    [Display(Name = "Favicon")]
    public string? Favicon { get; set; }

    // Contact
    [EmailAddress, Display(Name = "Email")]
    public string? Email { get; set; }
    [Display(Name = "Phone")]
    public string? Phone { get; set; }
    [Display(Name = "WhatsApp")]
    public string? WhatsApp { get; set; }
    [Display(Name = "Location")]
    public string? Location { get; set; }
    [Display(Name = "Location (Arabic)")]
    public string? LocationAr { get; set; }

    [Display(Name = "Copyright text")]
    public string? CopyrightText { get; set; }
    [Display(Name = "Copyright text (Arabic)")]
    public string? CopyrightTextAr { get; set; }

    // Default SEO
    [Display(Name = "Default meta title")]
    public string? DefaultMetaTitle { get; set; }
    [Display(Name = "Default meta description")]
    public string? DefaultMetaDescription { get; set; }
    [Display(Name = "Default OG image")]
    public string? DefaultOgImage { get; set; }

    // Social
    [Display(Name = "Instagram URL")]
    public string? InstagramUrl { get; set; }
    [Display(Name = "TikTok URL")]
    public string? TikTokUrl { get; set; }
    [Display(Name = "Facebook URL")]
    public string? FacebookUrl { get; set; }
    [Display(Name = "Snapchat URL")]
    public string? SnapchatUrl { get; set; }
    [Display(Name = "LinkedIn URL")]
    public string? LinkedInUrl { get; set; }
    [Display(Name = "YouTube URL")]
    public string? YouTubeUrl { get; set; }

    // Booking
    [EmailAddress, Display(Name = "Booking email")]
    public string? BookingEmail { get; set; }
    [Display(Name = "Booking WhatsApp")]
    public string? BookingWhatsApp { get; set; }

    // Analytics
    [Display(Name = "Google Analytics ID")]
    public string? GoogleAnalyticsId { get; set; }
    [Display(Name = "Google Tag Manager ID")]
    public string? GoogleTagManagerId { get; set; }

    // Advanced (SuperAdmin only)
    [Display(Name = "Custom CSS")]
    public string? CustomCss { get; set; }
    [Display(Name = "Custom JS")]
    public string? CustomJs { get; set; }
}
