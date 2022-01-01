using DisneyAPI.Dtos;
using DisneyAPI.Models;
namespace DisneyAPI.Services;


public class CharacterServices : ICharacterServices
{
    private DisneyContext _ctx;

    public CharacterServices(DisneyContext ctx)
    {
        _ctx = ctx;
    }

    public void Add(Character character)
    {    
        _ctx.Characters.Add(character);           
        _ctx.SaveChanges();
    } 
    public List<Character> GetAll() => _ctx.Characters.ToList();
    public Character? Get(int id)
    {        
        var c = _ctx.Characters.Find(id);
        _ctx.Entry(c).Collection(c => c.Movies).Load();
        return c;
    } 

    public void Delete(int id)
    {
        var character = Get(id);
        if (character == null) return;
        _ctx.Characters.Remove(character);
        _ctx.SaveChanges();
    }

    public void Update(Character character)
    {
        var c = _ctx.Characters.SingleOrDefault(c => c.CharacterId == character.CharacterId);                        
        if(c == null) return;        
        _ctx.Entry<Character>(c).CurrentValues.SetValues(character);        
        _ctx.SaveChanges();
    }

    public List<Character> Search(string? name, int? age, int? movieId)
    {
        IQueryable<Character> query = _ctx.Characters;
        if(name != null)
            query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));        
        if(age != null)
            query = query.Where(c => c.Age == age);
        if(movieId != null)
        {
            var movie = _ctx.Movies.Find(movieId);
            query = query.Where(c=> c.Movies.Contains(movie));
        }
        foreach(Character c in query)                      
        {
             _ctx.Entry(c).Collection(c => c.Movies).Load();
        }
        return query.ToList();
    }    
}