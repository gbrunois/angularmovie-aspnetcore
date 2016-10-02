using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Controllers
{
    /// <summary>
    /// Movies Controller.
    /// TODO : Make a controller named MoviesController. 
    /// </summary>
    public class MoviesController
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
        // TODO Implement Get method

        // GET server/api/movies/:id
        // TODO Implement Get method

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