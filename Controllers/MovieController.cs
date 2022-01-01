using DisneyAPI.Dtos;
using DisneyAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DisneyAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class MoviesController : ControllerBase
{
    private readonly IMovieServices _repository;

    public MoviesController(IMovieServices repository)
    {
        _repository = repository;
    }
    
    // GET/movies/{id}
    // 8. Detalle de pelicula/serie con sus personajes
    // Pelicula como MovieDetailsDto, personajes como SingleCharactersDto
    [HttpGet("{id}")]
    public ActionResult<MovieDetailsDto> Get(int id)
    {
        var movie = _repository.Get(id);
        if (movie == null) return NotFound();
        return movie.AsDetailsDto();
    }


    // 9. CRUD

    [HttpPost]
    public IActionResult Create (MovieDetailsDto movie)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _repository.Add(movie.AsModel());
        return CreatedAtAction(nameof(Create), new {id = movie.MovieId}, movie);
    }

    [HttpPut("{id}")]

    public ActionResult Update(Movie movie)
    {
        if(_repository.Get(movie.MovieId) == null) return NotFound();
        _repository.Update(movie);
        return NoContent();
    }

    [HttpDelete("{id}")]

    public ActionResult Delete(int id)
    {
        if(_repository.Get(id) == null) return NotFound();
        _repository.Delete(id);
        return NoContent();
    }
    
    // GET/movies 
    // 7. Listado de películas
    // Películas como MovieDto
    // 10. Búsqueda de películas o series
    // buscar por título, y filtrar por género. Además, permitir ordenar los resultados por fecha 
    // de creación de forma ascendiente o descendiente.
    [HttpGet]    
    public ActionResult<IEnumerable<MovieDetailsDto>> Search(string? name, int? genre, string? order)
    {
        if(name == null && genre == null && order == null)
        {
            var allMovies = _repository.GetAll().Select(m => m.AsDto());
            return Ok(allMovies);
        }
        var filteredMovies = _repository.Search(name, genre, order).Select(m => m.AsDetailsDto());
        return Ok(filteredMovies);
    }
}