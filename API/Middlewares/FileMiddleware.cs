using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace API.Middlewares;

public class FileMiddleware
{
    private readonly RequestDelegate _next;

    public FileMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/files"))
        {
            if (!context.Request.Query.ContainsKey("token"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }

            var encodedExpiry = context.Request.Query["token"];
            string expiryString;

            try
            {
                expiryString = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedExpiry));
            }
            catch (FormatException)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }

            if (!DateTime.TryParse(expiryString, null, System.Globalization.DateTimeStyles.RoundtripKind, out var expiryTime))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }

            if (DateTime.UtcNow > expiryTime)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access denied");
                return;
            }
        }

        await _next(context);
    }
}
