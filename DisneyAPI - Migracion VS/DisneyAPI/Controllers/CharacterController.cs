using DisneyAPI.Dtos;
using DisneyAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DisneyAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CharactersController : ControllerBase
{
    private readonly ICharacterServices _repository;
    public CharactersController(ICharacterServices repository)
    {
        _repository = repository;
    }
    
    
    // GET/characters/{id} 
    // Personaje por ID
    // 5. Detalle de personaje como CharacterDetailsDto
    // lista de películas como SingleMovieDto para evitar referencia cruzada
    [HttpGet("{id:int}")]
    public ActionResult<CharacterDetailsDto> Get(int id)
    {
        var character = _repository.Get(id);
        if(character == null) return NotFound();
        return character.AsDetailsDto();
    }    

    // 4. CRUD
    [HttpPost]
    public IActionResult Create(CharacterDetailsDto character)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }         
        _repository.Add(character.AsModel());
        return CreatedAtAction(nameof(Create), new {id = character.CharacterId}, character);
    }

    [HttpPut("{id}")]
    public ActionResult<Character> Update(Character character)
    {       
       if(_repository.Get(character.CharacterId) == null) return NotFound();
       _repository.Update(character);
       return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {        
       if(_repository.Get(id) == null) return NotFound();
       _repository.Delete(id);
       return NoContent();
    }

    // GET/characters 
    //3. Listado de personajes 
    //Imagen y nombre como CharacterDto    
    // 6. Búsqueda de personajes
    // Buscar por nombre, filtrar por edad y por películas
    [HttpGet]    
    public ActionResult<IEnumerable<CharacterDetailsDto>> Search(string? name, int? age, int? movieId)
    {      
        if(name == null && age == null && movieId == null)
        {
            var allCharacters = _repository.GetAll().Select(c => c.AsDto());
            return Ok(allCharacters);
        }
        var filteredCharacters = _repository.Search(name, age, movieId).Select(c => c.AsDetailsDto());
        return Ok(filteredCharacters);
    }
}