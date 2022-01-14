using System;
using System.ComponentModel.DataAnnotations;

namespace DisneyAPI.Dtos;

public class CharacterDto
{
    public byte[]? Image { get; set; }
    [Required]
    public string? Name { get; set; }    
}
