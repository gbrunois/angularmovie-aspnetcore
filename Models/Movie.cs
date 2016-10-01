using System;

namespace Web.MoviesApi.Models
{
    public class Movie
    {
        public Movie()
        {

        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; } 

        public int ReleaseYear { get; set; }

        public string Poster { get; set; }

        public string Directors { get; set; }

        public string Actors { get; set; }

        public string Synopsis { get; set; }

        public int Rate { get; set; }

        public DateTime LastViewDate { get; set; }

        public decimal? Price { get; set; }
    }
}