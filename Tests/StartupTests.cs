using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Web.MoviesApi.Repositories;
using Web.MoviesApi.Repositories.MongoDB;
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

        [Fact]
        public void Should_Resolve_IMoviesRepository()
        {
            // Arrange
            var env = new Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            
            Startup startup = new Startup(env);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            // Act
            IMoviesRepository moviesRepository = (IMoviesRepository)serviceCollection.BuildServiceProvider().GetService(typeof(IMoviesRepository));

            // Assert
            Assert.NotNull(moviesRepository);
        }

        [Fact]
        public void Should_Set_MongoSettings()
        {
            var env = new Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            Startup startup = new Startup(env);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            IOptions<MongoSettings> mongoSettings = (IOptions<MongoSettings>)serviceCollection.BuildServiceProvider().GetService(typeof(IOptions<MongoSettings>));

            Assert.NotNull(mongoSettings.Value); 
        }

        [Fact]
        public void Should_MongoSettings_ConnectionString_HaveValueInDefaultEnvironment()
        {
            var env = new Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            Startup startup = new Startup(env);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            IOptions<MongoSettings> mongoSettings = (IOptions<MongoSettings>)serviceCollection.BuildServiceProvider().GetService(typeof(IOptions<MongoSettings>));

            Assert.Equal("mongo:27017", mongoSettings.Value.ConnectionString); 
        }

        [Fact]
        public void Should_MongoSettings_ConnectionString_HaveValueInDevelopmentEnvironment()
        {
            var env = new Microsoft.AspNetCore.Hosting.Internal.HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();            
            env.EnvironmentName = "development";

            Startup startup = new Startup(env);
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            IOptions<MongoSettings> mongoSettings = (IOptions<MongoSettings>)serviceCollection.BuildServiceProvider().GetService(typeof(IOptions<MongoSettings>));

            Assert.Equal("192.168.99.100:27017", mongoSettings.Value.ConnectionString); 
        }
    }
}