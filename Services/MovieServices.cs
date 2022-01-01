using DisneyAPI.Models;
namespace DisneyAPI.Services;

public class MovieServices : IMovieServices
{
    private DisneyContext _ctx;

    public MovieServices(DisneyContext ctx)
    {
        _ctx = ctx;
    }

    public List<Movie> GetAll() => _ctx.Movies.ToList();

    public Movie? Get(int id)
    {
        var m = _ctx.Movies.Find(id);
        _ctx.Entry(m).Collection(m=> m.Characters).Load();
        return m;
    } 

    public void Add(Movie movie)
    {
        _ctx.Movies.Add(movie);
        _ctx.SaveChanges();
    }

    public void Update(Movie movie)
    {
        var m = _ctx.Movies.SingleOrDefault(m => m.MovieId == movie.MovieId);
        if(m == null) return;        
        _ctx.Entry<Movie>(m).CurrentValues.SetValues(movie);
        _ctx.SaveChanges();
    }

    public void Delete(int id)
    {
        var movie = Get(id);
        if(movie == null) return;
        _ctx.Movies.Remove(movie);
        _ctx.SaveChanges();
    }    
    public List<Movie> Search(string? name, int? genre, string? order)
    {
        IQueryable<Movie> query = _ctx.Movies;
        if(name != null)
            query = query.Where(m => m.Title.ToLower().Contains(name.ToLower()));                
        if(genre != null)
        {            
            query = query.Where(m=> m.Genre.GenreId == genre);
        }
        if(order != null)
        {
            if(order == "ASC")
                query = query.OrderBy(m => m.CreationDate);
            if(order == "DESC")
                query = query.OrderByDescending(m=> m.CreationDate);
        }
        foreach(Movie m in query)                      
        {
             _ctx.Entry(m).Collection(m => m.Characters).Load();
        }
        return query.ToList();
    }
}