using Microsoft.AspNetCore.Builder;

namespace Web.MoviesApi.Middleware
{
    public static class SPAMiddlewareExtensions
    {
        public static IApplicationBuilder UseSPAMiddleware(this IApplicationBuilder builder, string defaultFilename)
        {
            return builder.UseMiddleware<SPAMiddleware>(defaultFilename);
        }
    }
}