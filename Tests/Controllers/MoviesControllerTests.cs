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
        public async Task GetMethodShouldReturnAllMovies()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var result = await movieController.Get();
                Assert.Equal(movies.Length, result.Count());
            }
        }

        [Fact]
        public async Task GetMethodWithIdArgShouldReturnMovieIdentifiedById()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var movie = movies[0];
                var result = (await movieController.Get(movie.Id.ToString())) as OkObjectResult;
                Assert.NotNull(result);
                Assert.Equal(movie, result.Value);
            }
        }

        [Fact]
        public async Task GetMethodWithUnknownIdArgShouldReturnNotFoundException()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                var result = await movieController.Get(Guid.NewGuid().ToString());
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task PostMethodShouldInsertNewMovie()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                Movie newMovie = new Movie() { Title = "Titre 3" };
                var result = await movieController.Post(newMovie);
                Assert.IsType<OkObjectResult>(result);
                var insertedMovie = ((OkObjectResult)result).Value as Movie;
                Assert.NotNull(insertedMovie.Id);
                Assert.NotEqual(string.Empty, insertedMovie.Id);
            }
        }

        [Fact]
        public async Task PutMethodShouldUpdateExitingMovie()
        {
            var movies = CreateMoviesCollection();
            var moviesRepository = CreateMoviesRepositoryMock(movies);
            using (MoviesController movieController = new MoviesController(moviesRepository))
            {
                Movie movie = new Movie() { Id = "1", Title = "Nouveau titre" };

                var result = await movieController.Put(movie);
                Assert.IsType<OkResult>(result);

                await moviesRepository.Received().UpdateMovie(NSubstitute.Arg.Any<Movie>());
            }
        }

        [Fact]
        public async Task PutMethodWhenMovieDontExistsShouldReturn304HttpError()
        {
            var movies = CreateMoviesCollection();
            var moviesRepository = CreateMoviesRepositoryMock(movies);
            using (MoviesController movieController = new MoviesController(moviesRepository))
            {
                Movie movie = new Movie() { Id = "3", Title = "Nouveau titre" };

                var result = await movieController.Put(movie);
                Assert.IsType<StatusCodeResult>(result);
                Assert.Equal(((StatusCodeResult)result).StatusCode, 304);
            }
        }

        [Fact]
        public async Task PostMethodShouldReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                movieController.ModelState.AddModelError("Title", "Required");

                Movie newMovie = new Movie() { Title = "Titre 3" };
                var result = await movieController.Post(newMovie);
                
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async Task PutMethodShouldReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            var movies = CreateMoviesCollection();
            using (MoviesController movieController = new MoviesController(CreateMoviesRepositoryMock(movies)))
            {
                movieController.ModelState.AddModelError("Title", "Required");

                Movie movie = new Movie() { Id = "1", Title = "Nouveau titre" };
                var result = await movieController.Put(movie);
                
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