using System.Linq;
using Web.MoviesApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Web.MoviesApi.Repositories.MongoDB
{
    public class MoviesRepository : IMoviesRepository
    {
        private const string MoviesCollection = "movies";

        private IMongoDatabase _dbInstance;
        private IMongoCollection<Movie> _collection;

        public MoviesRepository(IOptions<MongoSettings> mongoSettings)
        {
            _dbInstance = CreateDatabaseConnection(mongoSettings);
            _collection = _dbInstance.GetCollection<Movie>(MoviesCollection);
        }

        private IMongoDatabase CreateDatabaseConnection(IOptions<MongoSettings> mongoSettings)
        {
            var configuration = mongoSettings.Value;
            // or use a connection string
            var settings = new MongoClientSettings()
            {
                Credentials = new[] {
                    MongoCredential.CreateCredential(
                        configuration.DatabaseName,
                        configuration.Username,
                        configuration.Password) },
                Server = MongoServerAddress.Parse(configuration.ConnectionString)
            };
            var client = new MongoClient(settings);
            return client.GetDatabase(configuration.DatabaseName);
        }

        public async Task<Movie[]> GetMovies()
        {
            var results = await _collection.Find(x => true).ToListAsync();
            return results.ToArray();
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            var filter = new BsonDocument("_id", id);
            var results = await _collection.Find(filter).ToListAsync();
            return results.FirstOrDefault();
        }

        public async Task InsertMovie(Movie movie)
        {
            await _collection.InsertOneAsync(movie);
        }

        public async Task DeleteMovie(Guid id)
        {
            var filter = new BsonDocument("_id", id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task UpdateMovie(Movie movie)
        {
            var filter = new BsonDocument("_id", movie.Id);
            await _collection.ReplaceOneAsync(filter, movie);
        }
    }
}