using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MoviesApi.Models;
using Web.MoviesApi.Repositories;

namespace Web.MoviesApi.Controllers
{
    [Route("server/api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesDAO;

        public MoviesController(IMoviesRepository moviesDAO)
        {
            _moviesDAO = moviesDAO;
        }

        // GET server/api/movies
        [HttpGet]
        public async Task<IEnumerable<Movie>> Get()
        {
            return await _moviesDAO.GetMovies();
        }

        // GET server/api/movies/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var movie = await _moviesDAO.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        // POST server/api/movies
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Movie value)
        {
            await _moviesDAO.InsertMovie(value);
            return Ok(value);
        }

        // PUT server/api/movies
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]Movie value)
        {
            Movie movie = await _moviesDAO.GetMovie(value.Id);
            if (movie != null)
            {
                await _moviesDAO.UpdateMovie(value);
                return Ok();
            }
            else
            {
                return new StatusCodeResult(304);
            }
        }

        // DELETE server/api/movies/:id
        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            await _moviesDAO.DeleteMovie(id);
        }
    }
}