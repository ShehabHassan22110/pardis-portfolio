using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BardeesCms.Web.Areas.Identity.Pages.Account;

/// <summary>
/// Self-service registration is disabled — admin accounts are provisioned through the CMS
/// Users area (SuperAdmin only). This override replaces the default Identity UI Register
/// page so it is not reachable (returns 404).
/// </summary>
[AllowAnonymous]
public class RegisterModel : PageModel
{
    public IActionResult OnGet() => NotFound();
    public IActionResult OnPost() => NotFound();
}
