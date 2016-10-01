using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.FileProviders;
using Web.MoviesApi.Middleware;
using Xunit;

namespace Web.MoviesApi.Tests.Middleware
{
    public class SPAMiddlewareTests
    {
        public SPAMiddlewareTests()
        {

        }

        [Fact]
        public async void Should_Always_Serve_DefaultFile()
        {
            var contentRoot = Directory.GetCurrentDirectory();

            //  Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseMiddleware<SPAMiddleware>("/index.html");
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        //override default directory
                        FileProvider = new CompositeFileProvider(new PhysicalFileProvider(Path.Combine(contentRoot, "wwwroot/app")))
                    });
                });
            // Act
            using (var server = new TestServer(hostBuilder))
            {
                
                var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "home");
                var responseMessage = await server.CreateClient().SendAsync(requestMessage);

                // Assert
                Assert.Equal(responseMessage.StatusCode, HttpStatusCode.OK);
            }
        }
    }
}