using System.Text;
using System.Xml;
using BardeesCms.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BardeesCms.Web.Controllers;

/// <summary>Serves sitemap.xml and robots.txt from live content.</summary>
public class SeoController : Controller
{
    private readonly ApplicationDbContext _db;
    public SeoController(ApplicationDbContext db) => _db = db;

    [HttpGet("/robots.txt")]
    [ResponseCache(Duration = 3600)]
    public IActionResult Robots()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var sb = new StringBuilder();
        sb.AppendLine("User-agent: *");
        sb.AppendLine("Allow: /");
        sb.AppendLine("Disallow: /Admin");
        sb.AppendLine("Disallow: /Identity");
        sb.AppendLine($"Sitemap: {baseUrl}/sitemap.xml");
        return Content(sb.ToString(), "text/plain", Encoding.UTF8);
    }

    [HttpGet("/sitemap.xml")]
    [ResponseCache(Duration = 3600)]
    public async Task<IActionResult> Sitemap()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var urls = new List<(string loc, DateTime? mod)>
        {
            ($"{baseUrl}/", null),
            ($"{baseUrl}/about", null),
            ($"{baseUrl}/work", null),
            ($"{baseUrl}/services", null),
            ($"{baseUrl}/videos", null),
            ($"{baseUrl}/contact", null),
        };

        foreach (var p in await _db.PortfolioProjects.AsNoTracking().Where(x => x.IsPublished)
                     .Select(x => new { x.Slug, x.UpdatedAt, x.CreatedAt }).ToListAsync())
            urls.Add(($"{baseUrl}/work/{p.Slug}", p.UpdatedAt ?? p.CreatedAt));

        foreach (var s in await _db.Services.AsNoTracking().Where(x => x.IsActive)
                     .Select(x => new { x.Slug, x.UpdatedAt, x.CreatedAt }).ToListAsync())
            urls.Add(($"{baseUrl}/services/{s.Slug}", s.UpdatedAt ?? s.CreatedAt));

        var sb = new StringBuilder();
        var settings = new XmlWriterSettings { Indent = true, Encoding = new UTF8Encoding(false) };
        await using var sw = new StringWriter(sb);
        using (var w = XmlWriter.Create(sw, settings))
        {
            w.WriteStartDocument();
            w.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
            foreach (var (loc, mod) in urls)
            {
                w.WriteStartElement("url");
                w.WriteElementString("loc", loc);
                if (mod is not null) w.WriteElementString("lastmod", mod.Value.ToString("yyyy-MM-dd"));
                w.WriteEndElement();
            }
            w.WriteEndElement();
            w.WriteEndDocument();
        }
        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }
}
