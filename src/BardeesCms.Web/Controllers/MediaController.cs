using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace BardeesCms.Web.Controllers;

/// <summary>
/// First-party media proxy. Serves YouTube thumbnails from our own origin so ad/tracker
/// blockers (Brave, uBlock, etc.) don't hide them, and so we can pick the best available
/// rendition server-side (maxres/oar 404 for many Shorts). Cached hard at the edge/browser.
/// </summary>
[Route("yt")]
public class MediaController : Controller
{
    private static readonly Regex IdRx = new("^[A-Za-z0-9_-]{6,20}$", RegexOptions.Compiled);
    // Portrait-friendly first (oar = original aspect ratio → full vertical for Shorts).
    private static readonly string[] Renditions = { "oardefault", "maxresdefault", "hqdefault", "mqdefault", "sddefault" };

    private readonly IHttpClientFactory _http;
    public MediaController(IHttpClientFactory http) => _http = http;

    [HttpGet("thumb/{id}")]
    public async Task<IActionResult> Thumb(string id, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(id) || !IdRx.IsMatch(id)) return NotFound();
        var client = _http.CreateClient("yt");
        foreach (var r in Renditions)
        {
            try
            {
                var resp = await client.GetAsync($"https://i.ytimg.com/vi/{id}/{r}.jpg", ct);
                if (!resp.IsSuccessStatusCode) continue;
                var bytes = await resp.Content.ReadAsByteArrayAsync(ct);
                // YouTube sometimes returns a tiny grey placeholder with 200 — skip those.
                if (bytes.Length < 2048) continue;
                Response.Headers.CacheControl = "public,max-age=604800,immutable"; // 7 days
                return File(bytes, "image/jpeg");
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { return NoContent(); }
            catch { /* try next rendition */ }
        }
        return NotFound();
    }
}
