using System.ComponentModel.DataAnnotations;
using DisneyAPI.Models;

public class UserDto
{    
    
    [EmailAddress]    
    public string Email { get; set; }
    public string Password { get; set; }
}