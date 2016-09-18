using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.MoviesApi.Controllers;
using Web.MoviesApi.Models;
using Web.MoviesApi.Repositories.Interface;
using Xunit;

namespace Web.MoviesApi.Tests.Controllers
{
    public class MoviesControllerTests
    {
        public MoviesControllerTests()
        {

        }

        [Fact]
        public void GetMethodShouldReturnAllMovies()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var result = movieController.Get();
                Assert.Equal(movies.Length, result.Result.Count());
            }
        }

        [Fact]
        public void GetMethodWithIdArgShouldReturnMovieIdentifiedById()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var movie = movies[0];
                var result = movieController.Get(movie.Id.ToString()).Result as OkObjectResult;
                Assert.NotNull(result);
                Assert.Equal(movie, result.Value);
            }
        }

        [Fact]
        public void GetMethodWithUnknownIdArgShouldReturnNotFoundException()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var result = movieController.Get(Guid.NewGuid().ToString()).Result;
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void GetMethodWithBadIdArgShouldReturnBadRequestException()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var result = movieController.Get("1").Result;
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public void PostMethodShouldInsertNewMovie()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                Movie newMovie = new Movie() { Title = "Titre 3" };
                var result = movieController.Post(newMovie).Result;
                Assert.IsType<OkObjectResult>(result);
                var insertedMovie = ((OkObjectResult)result).Value as Movie;
                Assert.NotNull(insertedMovie.Id);
                Assert.NotEqual(Guid.Empty, insertedMovie.Id);
            }
        }

        [Fact]
        public void PostMethodWithBadArgumentShouldReturnBadRequest()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                //must add error manually because the normal MVC request pipeline that performs model validation is bypassed altogether. 
                //The object passed into the Post method never gets validated
                movieController.ModelState.AddModelError("title", "error");
                Movie newMovie = new Movie() { Title = null };
                var result = movieController.Post(newMovie).Result;
                Assert.IsType<BadRequestObjectResult>(result);                
            }
        }

        private Movie[] CreateMoviesCollection()
        {
            return new Movie[] {
                new Movie() {
                    Id = Guid.NewGuid(),
                    Title = "Titre 1"
                },
                new Movie() {
                    Id = Guid.NewGuid(),
                    Title = "Titre 2"
                }
                };
        }

        private IMoviesRepository CreateMoviesRepositoryMock(Movie[] collection)
        {
            var substitute = Substitute.For<IMoviesRepository>();

            substitute.GetMovies().Returns(Task.FromResult(collection));

            substitute.GetMovie(NSubstitute.Arg.Any<Guid>()).ReturnsForAnyArgs(arg1 => Task.FromResult(collection.Where(movie => movie.Id == arg1.Arg<Guid>()).FirstOrDefault()));

            substitute.InsertMovie(NSubstitute.Arg.Any<Movie>()).ReturnsForAnyArgs(arg1 =>
            {
                var movie = arg1.Arg<Movie>();
                movie.Id = Guid.NewGuid();
                return Task.FromResult(movie);
            }
             );
            return substitute;
        }
    }
}