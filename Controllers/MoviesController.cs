using System;
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
        public MoviesController(IMoviesRepository moviesDAO)
        {

        }

        // GET server/api/movies
        [HttpGet]
        public Task<IEnumerable<Movie>> Get()
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        // GET server/api/movies/:id
        [HttpGet("{id}")]
        public Task<IActionResult> Get(string id)
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        // POST server/api/movies
        [HttpPost]
        public Task<IActionResult> Post([FromBody]Movie value)
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        // PUT server/api/movies
        [HttpPut]
        public Task<IActionResult> Put([FromBody]Movie value)
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        // DELETE server/api/movies/:id
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            // TODO Implement
            throw new NotImplementedException();
        }
    }
}