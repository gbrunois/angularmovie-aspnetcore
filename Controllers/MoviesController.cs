using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Controllers
{
    [Route("server/api/[controller]")]
    public class MoviesController : Controller
    {
        // GET server/api/movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return Data.MoviesDAO.GetMovies();
        }

        // GET server/api/movies/:id
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return Data.MoviesDAO.GetMovie(id);
        }

        // POST server/api/movies
        [HttpPost]
        public void Post([FromBody]Movie value)
        {
            Data.MoviesDAO.InsertMovie(value);
        }

        // PUT server/api/movies
        [HttpPut]
        public void Put([FromBody]Movie value)
        {
            Movie movie = Data.MoviesDAO.GetMovie(value.Id);
            if (movie != null)
            {
                Data.MoviesDAO.UpdateMovie(value);
            }
        }

        // DELETE server/api/movies/:id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Data.MoviesDAO.DeleteMovie(id);
        }
    }

}