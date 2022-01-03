using DisneyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DisneyAPI.Helpers;

namespace DisneyAPI.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly IUserServices _repository;
    private readonly IConfiguration Configuration;

    public AuthController(IUserServices repository, IConfiguration configuration)
    {
        _repository = repository;
        Configuration = configuration;
    }

    // 2 Autenticación de usuarios: Endpoint de registro
    [HttpPost("register")]
    public ActionResult<UserDto> Register(UserDto user)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        if(_repository.GetExistentUser(user.Email) != null) return BadRequest();
        _repository.CreateAsync(new User{
            Email = user.Email,
            Password = user.Password
        });        
        return CreatedAtAction(nameof(Register), user);
    }

    [HttpPost("login")]

    public ActionResult Login(UserDto user)
    {
        if(!ModelState.IsValid) return BadRequest();
        var loggedInUser = _repository.Get(new User{
            Email = user.Email,
            Password = user.Password
        });
        if(loggedInUser == null) return NotFound("Credenciales de acceso inválidas");
        var token = TokenGenerator.GenerateToken(Configuration, loggedInUser.Email);    
        return Ok(token);
    }

    // Enpoint para debug
    [HttpGet]
    public ActionResult<List<UserDto>> Get()
    {
        return Ok(_repository.GetAll());
    }
}