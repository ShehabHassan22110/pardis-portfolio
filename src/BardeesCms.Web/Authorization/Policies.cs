using Microsoft.AspNetCore.Authorization;

namespace BardeesCms.Web.Authorization;

/// <summary>Named authorization policies used across the admin area.</summary>
public static class Policies
{
    public const string ViewAdmin = "ViewAdmin";
    public const string ManageContent = "ManageContent";
    public const string ManageMedia = "ManageMedia";
    public const string ManageMessages = "ManageMessages";
    public const string ManageSeo = "ManageSeo";
    public const string ManageSettings = "ManageSettings";
    public const string ManageUsers = "ManageUsers";
    public const string AdvancedSettings = "AdvancedSettings";

    /// <summary>Registers every policy against the appropriate role sets.</summary>
    public static void AddCmsPolicies(this AuthorizationOptions options)
    {
        // Anyone with an admin role can view the dashboard (Viewer = read-only).
        options.AddPolicy(ViewAdmin, p => p.RequireRole(Roles.All));
        // Content editing: everyone except Viewer.
        options.AddPolicy(ManageContent, p => p.RequireRole(Roles.SuperAdmin, Roles.ContentManager, Roles.Editor));
        options.AddPolicy(ManageMedia, p => p.RequireRole(Roles.SuperAdmin, Roles.ContentManager, Roles.Editor));
        options.AddPolicy(ManageMessages, p => p.RequireRole(Roles.SuperAdmin, Roles.ContentManager, Roles.Editor));
        options.AddPolicy(ManageSeo, p => p.RequireRole(Roles.SuperAdmin, Roles.ContentManager));
        options.AddPolicy(ManageSettings, p => p.RequireRole(Roles.SuperAdmin, Roles.ContentManager));
        // Users + advanced code: SuperAdmin only.
        options.AddPolicy(ManageUsers, p => p.RequireRole(Roles.SuperAdmin));
        options.AddPolicy(AdvancedSettings, p => p.RequireRole(Roles.SuperAdmin));
    }
}
