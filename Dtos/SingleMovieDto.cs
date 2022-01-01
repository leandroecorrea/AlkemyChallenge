using System.ComponentModel.DataAnnotations;
using DisneyAPI.Models;

namespace DisneyAPI.Dtos
{
    public class SingleMovieDto
    {
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }

        public byte[]? Image {get; set;}
        public DateTimeOffset CreationDate {get; set;}
        [Range(1, 5)]
        public int Rated { get; set; }        
        public Genre? Genre { get; set; }        
    }
}