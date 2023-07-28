using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace City_Vibe.Infrastructure.Middleware
{
    public class CustomRequestValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomRequestValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (!IsValidRequest(context.Request))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid request");

               

                context.Items["text"] = "Invalid request";
                return; 
            }

            await _next(context);
        }

        private bool IsValidRequest(HttpRequest request)
        {
            var userAgent = request.Headers[HeaderNames.UserAgent].ToString();

            var YandexBrowser = "YaBrowser";

            if (userAgent.Contains(YandexBrowser, StringComparison.OrdinalIgnoreCase)) return false;

            if (Regex.IsMatch(userAgent, "MSIE|Trident"))
            {
                return false;
            }

            return true;
        }
    }
}
