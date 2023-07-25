using City_Vibe.Infrastructure.Middleware;

namespace City_Vibe.ExtensionMethod
{
    public static class  MiddlewareCollectionExtensions
    {
        public static  IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomRequestValidationMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            return app;
           
        }
    }
}
