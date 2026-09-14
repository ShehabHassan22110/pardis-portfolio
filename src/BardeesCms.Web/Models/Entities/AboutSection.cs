namespace BardeesCms.Web.Models.Entities;

/// <summary>The About feature block. Singleton row (Id = 1). Facts live in <see cref="AboutFact"/>.</summary>
public class AboutSection : AuditableEntity, IActivatable
{
    public string? Eyebrow { get; set; }
    public string? EyebrowAr { get; set; }
    public string Title { get; set; } = "";
    public string? TitleAr { get; set; }
    /// <summary>Lead / pull-quote.</summary>
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    /// <summary>Long-form body (rich text, sanitized before render).</summary>
    public string? LongDescription { get; set; }
    public string? LongDescriptionAr { get; set; }
    public string? Image { get; set; }
    public string? Location { get; set; }
    public string? LocationAr { get; set; }
    public string? Quote { get; set; }
    public string? QuoteAr { get; set; }
    public string? ButtonText { get; set; }
    public string? ButtonTextAr { get; set; }
    public string? ButtonUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<AboutFact> Facts { get; set; } = new List<AboutFact>();
}

/// <summary>A key/value fact shown in the About block (e.g. "Based in — Saudi Arabia").</summary>
public class AboutFact : IOrderable, IActivatable
{
    public int Id { get; set; }
    public int AboutSectionId { get; set; }
    public AboutSection? AboutSection { get; set; }

    public string Label { get; set; } = "";
    public string? LabelAr { get; set; }
    public string Value { get; set; } = "";
    public string? ValueAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
