using System;
using System.ComponentModel.DataAnnotations;

namespace Web.MoviesApi.Models
{
    public class Movie
    {
        public Movie()
        {

        }

        public string Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Title { get; set; }

        public string Category { get; set; } 

        public int ReleaseYear { get; set; }

        public string Poster { get; set; }

        public string Directors { get; set; }

        public string Actors { get; set; }

        [StringLength(500)]
        public string Synopsis { get; set; }

        public int Rate { get; set; }

        public DateTime LastViewDate { get; set; }

        [Range(1d,100d)]
        public decimal? Price { get; set; }
        
    }
}