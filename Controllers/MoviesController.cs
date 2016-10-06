using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Controllers
{
    /// <summary>
    /// Movies Controller.
    /// </summary>
    [Route("server/api/[controller]")]
    public class MoviesController : Controller
    {
        private Movie[] _movies;

        public MoviesController()
        {
            // For this step, we work with fixed values
            _movies = new Movie[] {
                new Movie() { Id = "1", Title = "Titre 1" },
                new Movie() { Id = "2", Title = "Titre 2" },
                new Movie() { Id = "3", Title = "Titre 3" },
            };
        }

        // GET server/api/movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return GetMovies();
        }

        // GET server/api/movies/:id
        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            return GetMovie(id);
        }

        /// <summary>
        /// Return all movies
        /// </summary>
        /// <returns></returns>
        public Movie[] GetMovies()
        {
            return _movies;
        }

        /// <summary>
        /// Return a movie identified by id or null if not exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movie GetMovie(string id)
        {
            return _movies.Where(movie => movie.Id.ToString() == id).FirstOrDefault();
        }
    }
}