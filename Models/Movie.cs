using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Web.MoviesApi.Models
{
    public class Movie
    {
        public Movie() {

        }    

        [Required]
        [BsonId(IdGenerator = typeof(MongoDB.Bson.Serialization.IdGenerators.GuidGenerator))]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("category")]
        public string Category { get; set; } 

        [BsonElement("releaseYear")]  
        public int ReleaseYear { get; set; }

        [BsonElement("poster")]
        public string Poster { get; set; }

        [BsonElement("directors")]
        public string Directors { get; set; }

        [BsonElement("actors")]
        public string Actors { get; set; }

        [StringLength(500)]
        [BsonElement("synopsis")]
        public string Synopsis { get; set; }

        [BsonElement("rate")]
        public int Rate { get; set; }

        [BsonElement("lastViewDate")]
        public DateTime LastViewDate { get; set; }

        [BsonElement("price")]
        [Range(1d, 100d)]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
    }
}