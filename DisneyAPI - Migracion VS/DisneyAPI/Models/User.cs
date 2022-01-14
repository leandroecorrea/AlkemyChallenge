namespace DisneyAPI.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserId { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
}