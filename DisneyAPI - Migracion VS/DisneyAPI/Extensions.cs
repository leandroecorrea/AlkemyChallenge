using DisneyAPI.Dtos;
using DisneyAPI.Models;

namespace DisneyAPI;
public static class Extensions
{
    public static CharacterDto AsDto(this Character character)
    {
        return new CharacterDto{ 
            Name = character.Name, 
            Image = character.CharacterImage
        };
    }

    public static MovieDto AsDto(this Movie movie)
    {
        return new MovieDto{
            Image = movie.Image,
            Title = movie.Title,
            CreationDate = movie.CreationDate
        };
    }

    public static CharacterDetailsDto AsDetailsDto(this Character character)
    {        
        return new CharacterDetailsDto{
            CharacterId = character.CharacterId,
            CharacterImage = character.CharacterImage,
            Name = character.Name,
            Age = character.Age,
            Weight = character.Weight,
            Story = character.Story,
            Movies = character.Movies == null? null : character.Movies
            .Select(m => m.AsSingleDto())
            .ToList()
        };
    }

    public static MovieDetailsDto AsDetailsDto(this Movie movie)
    {
        return new MovieDetailsDto{
            MovieId = movie.MovieId,
            Title = movie.Title,
            Image = movie.Image,
            CreationDate = movie.CreationDate,
            Rated = movie.Rated,
            Genre = movie.Genre,
            Characters = movie.Characters == null? null :
            movie.Characters
                .Select(c => c.AsSingleDto())
                .ToList()
        };
    }

    public static SingleCharacterDto AsSingleDto(this Character character)
    {
        return new SingleCharacterDto{
            CharacterId = character.CharacterId,
            CharacterImage = character.CharacterImage,
            Name = character.Name,
            Age = character.Age,
            Weight = character.Weight,
            Story = character.Story
        };
    }
    public static SingleMovieDto AsSingleDto(this Movie movie)
    {
        return new SingleMovieDto{
            MovieId = movie.MovieId,
            Title = movie.Title,
            Image = movie.Image,
            CreationDate = movie.CreationDate,
            Rated = movie.Rated,
            Genre = movie.Genre,
        };
    }    

    public static Character AsModel(this CharacterDetailsDto characterDetailsDto)
    {
        var character = new Character{
                CharacterId = characterDetailsDto.CharacterId,
                CharacterImage = characterDetailsDto.CharacterImage,
                Name = characterDetailsDto.Name,
                Age = characterDetailsDto.Age,
                Weight = characterDetailsDto.Weight,
                Story = characterDetailsDto.Story      
            };
        if(characterDetailsDto.Movies != null)
            characterDetailsDto.Movies.ToList().ForEach(m=>{
                Movie movie = new Movie{    
                                Title = m.Title,
                                Image = m.Image,
                                CreationDate = m.CreationDate,
                                Rated = m.Rated,
                                Genre = m.Genre    
                };
                character.Movies.Add(movie);                            
            });
        return character;           
    }

    public static Movie AsModel(this MovieDetailsDto movieDetailsDto)
    {
        var movie = new Movie{
            MovieId = movieDetailsDto.MovieId,
            Title = movieDetailsDto.Title,
            Image = movieDetailsDto.Image,
            CreationDate = movieDetailsDto.CreationDate,
            Rated = movieDetailsDto.Rated,
            Genre = movieDetailsDto.Genre,
        };
        if(movieDetailsDto.Characters != null)
            movieDetailsDto.Characters.ToList().ForEach(c=>{
                Character character = new Character{
                    CharacterId = c.CharacterId,
                    CharacterImage = c.CharacterImage,
                    Name = c.Name,
                    Age = c.Age,
                    Weight = c.Weight,
                    Story = c.Story
                };
                movie.Characters.Add(character);
            });
            return movie;
    }

    public static UserDto AsDto(this User user)
    {
        return new UserDto{            
            Email = user.Email,
            Password = user.Password
        };
    }
}