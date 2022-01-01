using System.ComponentModel.DataAnnotations;
namespace DisneyAPI.Dtos;
public class MovieDto
{
    public byte[]? Image {get; set;}
    [Required]
    public string Title { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}