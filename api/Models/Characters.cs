using Microsoft.EntityFrameworkCore;

namespace api
{
    // Referencia a DB
    class CharactersDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Data source=(localdb)\MSSQLLocalDB; Initial Catalog=Characters;Integrated Security=true");
        }
        
        public DbSet<Character> Characters{ get; set; }
        public DbSet<Movie> Movies{ get; set; }
        
        public DbSet<Genre> Genres{ get; set; }
    }
}