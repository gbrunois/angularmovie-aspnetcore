using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Web.MoviesApi.Middleware;

namespace Web.MoviesApi
{
    public class Startup
    {
        private string DEFAULT_FILENAME = "/index.html";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //TODO add SPAMiddleware
            
            //set default document
            app.UseDefaultFiles(DEFAULT_FILENAME);

            // serve static files like JavaScripts, CSS styles, images, or even HTML files
            // TODO add static files middleware here. use StaticFileOptions with CompositeFileProvider. 
            // env.WebRootPath is the path to wwwroot
            // all static files to serve are in app directory

            app.UseMvc();
        }
    }
}