using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

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

            //TODO
            //if StatusCode is 404 and Request.Path has no extension, then set Request.Path to defaultFilename
            //don't forget to invoke the next method
        }
    }
}