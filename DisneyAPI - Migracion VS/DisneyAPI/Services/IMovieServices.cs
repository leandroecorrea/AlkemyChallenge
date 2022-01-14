using DisneyAPI.Models;

public interface IMovieServices
{
    Movie Get(int id);
    List<Movie> GetAll();
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(int id);
    List<Movie> Search(string? nombre, int? idGenero, string? order);    
}