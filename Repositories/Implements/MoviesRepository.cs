using System.Linq;
using Web.MoviesApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using Web.MoviesApi.Repositories.Interface;

namespace Web.MoviesApi.Repositories.Implements
{
    public class MoviesRepository : IMoviesRepository
    {
        private IMongoDatabase _dbInstance;
        private const string MoviesCollection = "movies";

        public MoviesRepository()
        {
            _dbInstance = CreateDatabaseConnection();
        }

        private IMongoDatabase CreateDatabaseConnection()
        {
            // or use a connection string
            var settings = new MongoClientSettings()
            {
                Credentials = new[] { MongoCredential.CreateCredential("angularmovies", "angularUser", "password") },
                Server = new MongoServerAddress("192.168.99.100", 27017)
            };
            var client = new MongoClient(settings);
            return client.GetDatabase("angularmovies");
        }

        public async Task<Movie[]> GetMovies()
        {
            var results = await _dbInstance.GetCollection<Movie>(MoviesCollection).Find(x => true).ToListAsync();
            return results.ToArray();
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            var filter = new BsonDocument("_id", id);
            var results = await _dbInstance.GetCollection<Movie>(MoviesCollection).Find(filter).ToListAsync();
            return results.FirstOrDefault();
        }

        public async Task InsertMovie(Movie movie)
        {
            await _dbInstance.GetCollection<Movie>(MoviesCollection).InsertOneAsync(movie);
        }

        public async Task DeleteMovie(Guid id)
        {
            var filter = new BsonDocument("_id", id);
            await _dbInstance.GetCollection<Movie>(MoviesCollection).DeleteOneAsync(filter);
        }

        public async Task UpdateMovie(Movie movie)
        {
            var filter = new BsonDocument("_id", movie.Id);
            await _dbInstance.GetCollection<Movie>(MoviesCollection).ReplaceOneAsync(filter, movie);
        }
    }
}