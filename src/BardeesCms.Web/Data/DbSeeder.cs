using BardeesCms.Web.Authorization;
using BardeesCms.Web.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Data;

/// <summary>
/// Applies migrations, seeds roles + an optional admin account (from configuration only),
/// and populates the CMS with the current site's content on first run.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, ILogger logger)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var config = services.GetRequiredService<IConfiguration>();

        await db.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager, config, logger);
        await ContentSeed.SeedAsync(db, logger);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        foreach (var role in Roles.All)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
    }

    /// <summary>
    /// Creates the initial SuperAdmin from AdminSeed:Email / AdminSeed:Password.
    /// If either is missing, no account is created (never seed an insecure default).
    /// </summary>
    private static async Task SeedAdminAsync(
        UserManager<ApplicationUser> userManager, IConfiguration config, ILogger logger)
    {
        var email = config["AdminSeed:Email"];
        var password = config["AdminSeed:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning(
                "AdminSeed:Email / AdminSeed:Password not configured — no admin account seeded. " +
                "Set them via user-secrets or environment variables to create the initial SuperAdmin.");
            return;
        }

        if (await userManager.FindByEmailAsync(email) is not null) return;

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            FullName = config["AdminSeed:FullName"] ?? "Administrator",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, Roles.SuperAdmin);
            logger.LogInformation("Seeded initial SuperAdmin {Email}.", email);
        }
        else
        {
            logger.LogError("Failed to seed admin: {Errors}",
                string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}
