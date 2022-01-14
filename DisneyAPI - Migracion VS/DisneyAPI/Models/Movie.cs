using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Characters = new HashSet<Character>();
        }
        public int MovieId { get; set; }
        [Required]
        public string? Title { get; set; }

        public byte[]? Image {get; set;}
        public DateTimeOffset CreationDate {get; set;}
        [Range(1, 5)]
        public int Rated { get; set; }        
        public Genre? Genre { get; set; }
        public virtual ICollection<Character>? Characters { get; set; }
    }
}