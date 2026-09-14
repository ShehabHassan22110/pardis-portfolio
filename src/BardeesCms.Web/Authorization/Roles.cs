namespace BardeesCms.Web.Authorization;

/// <summary>Canonical admin role names used across Identity and [Authorize] policies.</summary>
public static class Roles
{
    public const string SuperAdmin = "SuperAdmin";
    public const string ContentManager = "ContentManager";
    public const string Editor = "Editor";
    public const string Viewer = "Viewer";

    public static readonly string[] All = { SuperAdmin, ContentManager, Editor, Viewer };
}
