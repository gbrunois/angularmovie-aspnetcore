using System.Threading.Tasks;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Repositories
{
    /// <summary>
    /// Interface for movies repository
    /// </summary>
    public interface IMoviesRepository
    {
        /// <summary>
        /// Fetch all movies
        /// </summary>
        /// <returns>All movies</returns>
        Task<Movie[]> GetMovies();

        /// <summary>
        /// Fetch a movie from id
        /// </summary>
        /// <param name="id">Movie unique identifier</param>
        /// <returns>A movie or null</returns>
        Task<Movie> GetMovie(string id);

        /// <summary>
        /// Insert a new movie
        /// </summary>
        /// <param name="movie">A movie to insert</param>
        /// <returns></returns>
        Task InsertMovie(Movie movie);

        /// <summary>
        /// Delete an existing movie
        /// </summary>
        /// <param name="id">Movie unique identifier</param>
        /// <returns></returns>
        Task DeleteMovie(string id);

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="movie">A movie to update</param>
        /// <returns></returns>
        Task UpdateMovie(Movie movie);
    }
}