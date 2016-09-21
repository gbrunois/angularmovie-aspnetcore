using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.MoviesApi.Models;
using Xunit;

namespace Web.MoviesApi.Tests.Models
{
    public class MovieValidationTests
    {
        public MovieValidationTests()
        {

        }

        [Fact]
        public void CompletedMovieShouldBeValid()
        {
            var movie = NewMovie(); 
            Assert.True(ValidateMovie(movie));
        }

        [Fact]
        public void NoTitleShouldBeInvalid()
        {
            var movie = NewMovie(); 
            movie.Title = null;
            Assert.False(ValidateMovie(movie));
        }

        [Fact]
        public void TitleLess1charactersShouldBeInvalid()
        {
            var movie = NewMovie(); 
            movie.Title = "";
            Assert.False(ValidateMovie(movie));
        }

        [Fact]
        public void SynopsisMore500charactersShouldBeInvalid()
        {
            var movie = NewMovie(); 
            movie.Synopsis = new String('X', 501);
            Assert.False(ValidateMovie(movie));
        }

        private bool ValidateMovie(Movie target) {
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(target, context, results, true);
        }

        private Movie NewMovie()
        {
            Movie result = new Movie();
            result.Title = "Nouveau film";
            return result;
        }
    }
}