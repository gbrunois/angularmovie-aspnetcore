using System;
using System.IO;
using System.Threading.Tasks;
using Web.MoviesApi.Models;
using System.Linq;

namespace Web.MoviesApi.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {

        public async Task<Movie> GetMovie(string id)
        {
            Movie[] movies = await ReadMoviesFile();
            return movies.Where(movie => movie.Id == id).FirstOrDefault();
        }

        public async Task<Movie[]> GetMovies()
        {
            return await ReadMoviesFile();
        }

        public Task InsertMovie(Movie movie)
        {
            throw new NotSupportedException();
        }

        public Task UpdateMovie(Movie movie)
        {
            throw new NotSupportedException();
        }

        public Task DeleteMovie(string id)
        {
            throw new NotSupportedException();
        }

        private async Task<Movie[]> ReadMoviesFile()
        {
            return await Task.Factory.StartNew(() => Newtonsoft.Json.JsonConvert.DeserializeObject<Movie[]>(File.ReadAllText(@"Repositories/movies.json")));
        }
    }
}