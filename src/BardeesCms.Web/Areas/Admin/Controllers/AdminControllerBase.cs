using BardeesCms.Web.Authorization;
using BardeesCms.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BardeesCms.Web.Areas.Admin.Controllers;

/// <summary>
/// Base for all admin controllers. Scopes to the Admin area, requires an admin role,
/// provides a flash-message helper, and invalidates the public content cache after any
/// successful mutation so edits appear on the live site immediately.
/// </summary>
[Area("Admin")]
[Authorize(Policy = Policies.ViewAdmin)]
public abstract class AdminControllerBase : Controller
{
    protected void Flash(string message, string type = "success")
    {
        TempData["FlashMessage"] = message;
        TempData["FlashType"] = type;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        // Any non-GET admin request that didn't error is treated as a content change.
        if (!HttpContext.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase)
            && context.Exception is null)
        {
            HttpContext.RequestServices.GetService<IPublicContentService>()?.Invalidate();
        }
    }
}
