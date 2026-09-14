namespace BardeesCms.Web.Models.Entities;

/// <summary>A social / digital platform in the "Digital presence" section.</summary>
public class Platform : AuditableEntity, IOrderable, IActivatable
{
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public string? Username { get; set; }
    public string? Url { get; set; }
    /// <summary>Bootstrap-icons class name, e.g. "instagram".</summary>
    public string? Icon { get; set; }
    public string? Followers { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>A headline audience statistic (e.g. "XXK+ Followers").</summary>
public class PresenceStat : IOrderable, IActivatable
{
    public int Id { get; set; }
    public string Value { get; set; } = "";
    public string Label { get; set; } = "";
    public string? LabelAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>A content-style tag chip (e.g. "Reels", "Stories").</summary>
public class ContentStyle : IOrderable, IActivatable
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? NameAr { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
