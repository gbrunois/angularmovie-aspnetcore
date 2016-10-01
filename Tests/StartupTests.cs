using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Web.MoviesApi.Tests
{
    public class StartupTests
    {
        public StartupTests()
        {

        }

        [Fact]
        public async void Should_Serve_StaticFiles()
        {
            var contentRoot = Directory.GetCurrentDirectory();

            // Arrange
            var _server = new TestServer(
                new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseStartup<Startup>());
            var _client = _server.CreateClient();

            // Act
            var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), "index.html");
            var responseMessage = await _server.CreateClient().SendAsync(requestMessage);

            // Assert
            Assert.Equal(responseMessage.StatusCode, HttpStatusCode.OK);
        }
    }
}