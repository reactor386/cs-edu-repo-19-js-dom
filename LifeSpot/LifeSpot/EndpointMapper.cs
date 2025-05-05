//-
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace LifeSpot;

public static class EndpointMapper
{
    /// <summary>
    ///  Маппинг CSS-файлов
    /// </summary>
    public static void MapCss(this IEndpointRouteBuilder builder)
    {
        var cssFiles = new[] { "index.css" };
        
        foreach (var fileName in cssFiles)
        {
            builder.MapGet($"/wwwroot/css/{fileName}", async context =>
            {
                var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", fileName);
                var css = await File.ReadAllTextAsync(cssPath);
                await context.Response.WriteAsync(css);
            });
        }
    }

    /// <summary>
    ///  Маппинг JS
    /// </summary>
    public static void MapJs(this IEndpointRouteBuilder builder)
    {
        var jsFiles = new[] { "index.js", "testing.js", "about.js" };
    
        foreach (var fileName in jsFiles)
        {
            builder.MapGet($"/wwwroot/js/{fileName}", async context =>
            {
                var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "js", fileName);
                var js = await File.ReadAllTextAsync(jsPath);
                await context.Response.WriteAsync(js);
            });
        }
    }

    /// <summary>
    ///  Маппинг Html-страниц
    /// </summary>
    public static void MapHtml(this IEndpointRouteBuilder builder)
    {
        string footerHtml = File.ReadAllText(
            Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
        string sideBarHtml = File.ReadAllText(
            Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sidebar.html"));
        
        builder.MapGet("/", async context =>
        {
            var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index.html");
            var viewText = await File.ReadAllTextAsync(viewPath);
            
            // Загружаем шаблон страницы, вставляя в него элементы
            var html =  new StringBuilder(await File.ReadAllTextAsync(viewPath))
                .Replace("<!--SIDEBAR-->", sideBarHtml)
                .Replace("<!--FOOTER-->", footerHtml)
                .ToString();
            
            await context.Response.WriteAsync(html);
        });
        
        builder.MapGet("/testing", async context =>
        {
            var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "testing.html");
            
            // Загружаем шаблон страницы, вставляя в него элементы
            var html =  new StringBuilder(await File.ReadAllTextAsync(viewPath))
                .Replace("<!--SIDEBAR-->", sideBarHtml)
                .Replace("<!--FOOTER-->", footerHtml)
                .ToString();
            
            await context.Response.WriteAsync(html);
        });
        
        builder.MapGet("/about", async context =>
        {
            var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "about.html");
            
            // Загружаем шаблон страницы, вставляя в него элементы
            var html =  new StringBuilder(await File.ReadAllTextAsync(viewPath))
                .Replace("<!--SIDEBAR-->", sideBarHtml)
                .Replace("<!--FOOTER-->", footerHtml)
                .ToString();
            
            await context.Response.WriteAsync(html);
        });
    }
}
