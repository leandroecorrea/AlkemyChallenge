using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DisneyAPI.Helpers;
public static class TokenGenerator
{
    public static string GenerateToken(IConfiguration Configuration, string email)
    {
        var claims =  new[]
        {
            new Claim(ClaimTypes.NameIdentifier, email)            
        };
        var token = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256)
        );
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token); 
        return tokenString; 
    }
}