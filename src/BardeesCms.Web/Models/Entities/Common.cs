namespace BardeesCms.Web.Models.Entities;

/// <summary>Base for entities that track creation/update timestamps.</summary>
public abstract class AuditableEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>Marker for entities the admin can manually sort.</summary>
public interface IOrderable
{
    int DisplayOrder { get; set; }
}

/// <summary>Marker for entities that can be toggled on/off on the public site.</summary>
public interface IActivatable
{
    bool IsActive { get; set; }
}
