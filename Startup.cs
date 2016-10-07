using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.MoviesApi.Repositories;
using Web.MoviesApi.Repositories.MongoDB;
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
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<MongoSettings>(Configuration.GetSection("Mongo"));

            services.AddMvc();

            services.AddTransient<IMoviesRepository, MoviesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<SPAMiddleware>(DEFAULT_FILENAME);

            //set default document
            app.UseDefaultFiles(DEFAULT_FILENAME);

            // serve static files like JavaScripts, CSS styles, images, or even HTML files
            app.UseStaticFiles(new StaticFileOptions
            {
                //override default directory
                FileProvider = new CompositeFileProvider(new PhysicalFileProvider(Path.Combine(env.WebRootPath, "app")))
            });

            app.UseMvc();
        }
    }
}