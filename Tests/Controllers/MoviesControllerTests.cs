using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Web.MoviesApi.Tests.Controllers
{
    public class MoviesControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public MoviesControllerTests()
        {
            var contentRoot = Directory.GetCurrentDirectory();
            _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        private async Task<string> GetResponseString(string request)
        {
            var response = await _client.GetAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        [Fact]
        public async Task GetAllMoviesRequestShouldReturnAllMovies()
        {
            // Act
            var responseString = await GetResponseString("/server/api/movies");

            // Assert
            Assert.Equal("[{\"id\":\"1\",\"title\":\"Titre 1\"},{\"id\":\"2\",\"title\":\"Titre 2\"},{\"id\":\"3\",\"title\":\"Titre 3\"}]", responseString);
        }

        [Fact]
        public async Task GetMovieRequestShouldReturnMovie()
        {
            // Act
            var responseString = await GetResponseString("/server/api/movies/1");

            // Assert
            Assert.Equal("{\"id\":\"1\",\"title\":\"Titre 1\"}", responseString);
        }
    }
}