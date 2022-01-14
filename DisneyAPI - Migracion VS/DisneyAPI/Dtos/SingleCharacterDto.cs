using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Dtos;
public class SingleCharacterDto
{
    public int CharacterId { get; set; }
    public byte[]? CharacterImage { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Age { get; set; }
    [Required]
    public float Weight { get; set; }
    public string? Story { get; set; }        
}