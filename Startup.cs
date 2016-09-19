using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.MoviesApi.Repositories.Interface;
using Web.MoviesApi.Repositories.Implements;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Web.MoviesApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IMoviesRepository, MoviesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //for angular, We need to serve the index.html to the client, if there was an 404 error, on requests without extensions
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            //set default document
            app.UseDefaultFiles("/index.html");

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