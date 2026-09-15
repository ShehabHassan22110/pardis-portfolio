using BardeesCms.Web.Models.Entities;

namespace BardeesCms.Web.Models.ViewModels;

/// <summary>Shared layout data (header/footer) available on every public page.</summary>
public class LayoutData
{
    public SiteSettings Settings { get; set; } = new();
    public List<NavigationItem> HeaderNav { get; set; } = new();
    public List<IGrouping<string, NavigationItem>> FooterNav { get; set; } = new();
}

/// <summary>Everything the single-page homepage needs.</summary>
public class HomeViewModel
{
    public HeroSection? Hero { get; set; }
    public List<HeroSlide> HeroSlides { get; set; } = new();
    public List<TrustedBrand> TrustedBrands { get; set; } = new();
    public AboutSection? About { get; set; }
    public List<AboutFact> AboutFacts { get; set; } = new();
    public List<Brand> Clients { get; set; } = new();
    public List<MarketPosition> MarketPositions { get; set; } = new();
    public List<Discipline> Disciplines { get; set; } = new();
    public List<PortfolioProject> FeaturedWork { get; set; } = new();
    public List<Video> Showreel { get; set; } = new();
    public List<Service> Services { get; set; } = new();
    public List<CollaborationStep> Steps { get; set; } = new();
    public List<Faq> Faqs { get; set; } = new();
    public List<Abaya> Abayas { get; set; } = new();
    public Dictionary<string, PageSection> Sections { get; set; } = new();

    public PageSection? Section(string key) => Sections.TryGetValue(key, out var s) ? s : null;
}

/// <summary>Work index page.</summary>
public class WorkIndexViewModel
{
    public List<Discipline> Disciplines { get; set; } = new();
    public List<PortfolioProject> Projects { get; set; } = new();
    public string? ActiveDiscipline { get; set; }
}

/// <summary>A single project detail page.</summary>
public class ProjectDetailViewModel
{
    public PortfolioProject Project { get; set; } = new();
    public List<PortfolioProject> Related { get; set; } = new();
}

/// <summary>Services index.</summary>
public class ServicesViewModel
{
    public List<Service> Services { get; set; } = new();
    public List<CollaborationStep> Steps { get; set; } = new();
    public List<Faq> Faqs { get; set; } = new();
    public PageSection? Heading { get; set; }
}

/// <summary>Videos page with categories for filter tabs.</summary>
public class VideosViewModel
{
    public List<Video> Videos { get; set; } = new();
    public List<VideoCategory> Categories { get; set; } = new();
    /// <summary>Channel link surfaced on the page header, if set in Site Settings.</summary>
    public string? YouTubeUrl { get; set; }
}

/// <summary>Abaya collection page + optional heading chrome.</summary>
public class AbayasViewModel
{
    public List<Abaya> Abayas { get; set; } = new();
    public PageSection? Heading { get; set; }
    /// <summary>WhatsApp number (digits, incl. country code) used for "Request Order".</summary>
    public string? WhatsApp { get; set; }
}

/// <summary>Contact page: settings + the bindable form.</summary>
public class ContactViewModel
{
    public ContactSettings? Settings { get; set; }
    public List<Discipline> Disciplines { get; set; } = new();
    public ContactFormInput Form { get; set; } = new();
    public bool Sent { get; set; }
}
