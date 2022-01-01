using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Models;
public class Character
{
    public Character()
    {
        this.Movies = new HashSet<Movie>();
    }
    public int CharacterId { get; set; }
    public byte[]? CharacterImage { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Age { get; set; }
    [Required]
    public float Weight { get; set; }
    public string? Story { get; set; }    
    public virtual ICollection<Movie>? Movies { get; set; }
}