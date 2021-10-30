using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    // Referencia a DB
    public class CharactersDbContext : DbContext
    {
        // contexto de conexion por inyeccion de dependencias
        public CharactersDbContext(DbContextOptions options) : base(options) { }
        
        
        public DbSet<Character> Characters{ get; set; }
        public DbSet<Movie> Movies{ get; set; }
        
        public DbSet<Genre> Genres{ get; set; }
    }
}