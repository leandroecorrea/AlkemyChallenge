using System;
using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Dtos;

public class CharacterDetailsDto
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
    public IList<SingleMovieDto>? Movies { get; set; }  
}
