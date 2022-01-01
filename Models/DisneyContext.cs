using Microsoft.EntityFrameworkCore;
namespace DisneyAPI.Models;


public class DisneyContext : DbContext
{
    public DbSet<Character>? Characters { get; set; }
    public DbSet<Movie>? Movies { get; set; }    
    public DbSet<Genre>? Genres { get; set; }
    public DbSet<User>? Users {get; set;}
    public string DbPath { get; private set; }

    public DisneyContext(DbContextOptions options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}disneyworld.db";           
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");    
}