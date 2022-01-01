using DisneyAPI.Dtos;
using DisneyAPI.Models;

public interface ICharacterServices
{    
    void Add(Character character);
    void Delete(int id);
    Character? Get(int id);
    List<Character> GetAll();
    void Update(Character character);
    List<Character> Search(string? name, int? age, int? idMovie);    
}