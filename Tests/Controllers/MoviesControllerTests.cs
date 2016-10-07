using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Web.MoviesApi.Controllers;
using Web.MoviesApi.Models;
using Web.MoviesApi.Repositories;
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
                Assert.NotEqual(string.Empty, insertedMovie.Id);
            }
        }

        [Fact]
        public void PutMethodShouldUpdateExitingMovie()
        {
            var movies = CreateMoviesCollection();
            var moviesRepository = CreateMoviesRepositoryMock(movies);
            using (MoviesController movieController = new MoviesController(moviesRepository))
            {
                Movie movie = new Movie() { Id = "1", Title = "Nouveau titre" };

                var result = movieController.Put(movie).Result;
                Assert.IsType<OkResult>(result);

                moviesRepository.Received().UpdateMovie(NSubstitute.Arg.Any<Movie>());
            }
        }

        [Fact]
        public void PutMethodWhenMovieDontExistsShouldReturn304HttpError()
        {
            var movies = CreateMoviesCollection();
            var moviesRepository = CreateMoviesRepositoryMock(movies);
            using (MoviesController movieController = new MoviesController(moviesRepository))
            {
                Movie movie = new Movie() { Id = "3", Title = "Nouveau titre" };

                var result = movieController.Put(movie).Result;
                Assert.IsType<StatusCodeResult>(result);
                Assert.Equal(((StatusCodeResult)result).StatusCode, 304);
            }
        }

        [Fact]
        public void PostMethodShouldReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                movieController.ModelState.AddModelError("Title", "Required");

                Movie newMovie = new Movie() { Title = "Titre 3" };
                var result = movieController.Post(newMovie).Result;
                
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public void PutMethodShouldReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                movieController.ModelState.AddModelError("Title", "Required");

                Movie movie = new Movie() { Id = "1", Title = "Nouveau titre" };
                var result = movieController.Put(movie).Result;
                
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        private Movie[] CreateMoviesCollection()
        {
            return new Movie[] {
                new Movie() {
                    Id = "1",
                    Title = "Titre 1"
                },
                new Movie() {
                    Id = "2",
                    Title = "Titre 2"
                }
                };
        }

        private IMoviesRepository CreateMoviesRepositoryMock(Movie[] collection)
        {
            var substitute = Substitute.For<IMoviesRepository>();

            substitute.GetMovies().Returns(Task.FromResult(collection));

            substitute.GetMovie(NSubstitute.Arg.Any<string>()).ReturnsForAnyArgs(arg1 => Task.FromResult(collection.Where(movie => movie.Id == arg1.Arg<string>()).FirstOrDefault()));

            substitute.InsertMovie(NSubstitute.Arg.Any<Movie>()).ReturnsForAnyArgs(arg1 =>
            {
                var movie = arg1.Arg<Movie>();
                movie.Id = "3";
                return Task.FromResult(movie);
            });
            substitute.UpdateMovie(NSubstitute.Arg.Any<Movie>()).ReturnsForAnyArgs(arg1 =>
           {
               return Task.CompletedTask;
           }
            );
            return substitute;
        }
    }
}