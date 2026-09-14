namespace BardeesCms.Web.Services;

/// <summary>Adds baseline security response headers (CSP, framing, sniffing, referrer, permissions).</summary>
public static class SecurityHeaders
{
    // Content-Security-Policy tuned to the origins this site actually uses.
    // 'unsafe-inline' is required for the inline styles/handlers used throughout the
    // editorial markup and small inline scripts (analytics, image onerror fallbacks).
    private const string Csp =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://www.googletagmanager.com https://www.google-analytics.com; " +
        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdn.jsdelivr.net; " +
        "font-src 'self' https://fonts.gstatic.com https://cdn.jsdelivr.net data:; " +
        "img-src 'self' data: blob: https:; " +
        "media-src 'self' blob: https:; " +
        "frame-src https://www.youtube.com https://www.youtube-nocookie.com https://player.vimeo.com; " +
        "connect-src 'self' https://www.google-analytics.com; " +
        "object-src 'none'; base-uri 'self'; frame-ancestors 'self'; form-action 'self'";

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app) => app.Use(async (ctx, next) =>
    {
        var h = ctx.Response.Headers;
        h["X-Content-Type-Options"] = "nosniff";
        h["X-Frame-Options"] = "SAMEORIGIN";
        h["Referrer-Policy"] = "strict-origin-when-cross-origin";
        h["Permissions-Policy"] = "geolocation=(), camera=(), microphone=(), interest-cohort=()";
        h["Content-Security-Policy"] = Csp;
        await next();
    });
}
