using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Web.MoviesApi.Middleware
{
    public class SPAMiddleware
    {
        //for angular, We need to serve the index.html to the client, if there was an 404 error, on requests without extensions

        private readonly RequestDelegate _next;
        private readonly string _defaultFilename;

        public SPAMiddleware(RequestDelegate next, string defaultFilename)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            _defaultFilename = defaultFilename;

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            await _next.Invoke(context);

            if (context.Response.StatusCode == 404
                && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Request.Path = _defaultFilename;
                await _next.Invoke(context);
            }
        }
    }
}