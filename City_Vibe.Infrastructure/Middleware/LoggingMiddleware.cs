using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace City_Vibe.Infrastructure.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
           _logger.LogInformation($"Request: {request.Method} {request.Path}");

            await _next(context);

            var response = context.Response;
            _logger.LogInformation($"Response: {response.StatusCode}");
        }
    }
}
