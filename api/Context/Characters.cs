using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Context
{
    // Referencia a DB
    public class CharactersDbContext : DbContext
    {
        // contexto de conexion por inyeccion de dependencias
        public CharactersDbContext(DbContextOptions<CharactersDbContext> options) : base(options) {
            Database.EnsureCreated();
        }        
        
        public DbSet<Character> Characters{ get; set; }
        public DbSet<Movie> Movies{ get; set; }
        
        public DbSet<Genre> Genres{ get; set; }

        public DbSet<CharacterMovie> CharacterMovie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterMovie>()
                .HasKey(bc => new { bc.CharacterId, bc.MovieId });
            modelBuilder.Entity<CharacterMovie>()
                .HasOne(bc => bc.Character)
                .WithMany(b => b.CharacterMovies)
                .HasForeignKey(bc => bc.CharacterId);
            modelBuilder.Entity<CharacterMovie>()
                .HasOne(bc => bc.Movie)
                .WithMany(c => c.CharacterMovies)
                .HasForeignKey(bc => bc.MovieId);

            // Datos iniciales
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Accion", Image = "accion.jpg" },
                new Genre { GenreId = 2, Name = "Aventura", Image = "aventura.jpg" },
                new Genre { GenreId = 3, Name = "Animacion", Image = "animacion.jpg" },
                new Genre { GenreId = 4, Name = "Drama", Image = "drama.jpg" },
                new Genre { GenreId = 5, Name = "Ciencia Ficcion", Image = "ciencia_ficcion.jpg" },
                new Genre { GenreId = 6, Name = "Suspenso", Image = "suspenso.jpg" },
                new Genre { GenreId = 7, Name = "Thriller", Image = "thriller.jpg" },
                new Genre { GenreId = 8, Name = "Terror", Image = "terror.jpg" }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { CharacterId = 1, Name = "Iron Man", Age=46, Image = "iron_man.jpg", Weight = 140, Story = @"El personaje fue co-creado por el escritor y editor Stan Lee, desarrollado por el guionista Larry Lieber y diseñado por los artistas Don Heck y Jack Kirby." },
                new Character { CharacterId = 2, Name = "Hulk", Age = 48, Image = "hulk.jpg", Weight = 230, Story = @"Bruce Banner (hulk) era un científico que trabajaba en una bomba de radiación Gamma para el ejercito estadounidense. ... Cuando Bruce se enojaba, éste se transformaba en una criatura de color verde (a veces gris) con poderes increíbles que aumentaban su capacidad fisica al que le apodaron Hulk." },
                new Character { CharacterId = 3, Name = "Thor", Age = 1500, Image = "thor.jpg", Weight = 85, Story = @"Hijo de Odín y Frigg, Thor era el dios del trueno y el rayo y el campeón de la raza humana de Midgard, a la que defendía de todos los males que poblaban los Nueve Reinos." },
                new Character { CharacterId = 4, Name = "Spider-Man", Age = 15, Image = "spiderman.jpg", Weight = 60, Story = @"Nacido en el 1962 para obra de Stan Lee, Spiderman es la historia del tímido estudiante Peter Parker que viene mordisco de una araña contaminada de los radios radiactivos en el curso de un experimento científico..." },
                new Character { CharacterId = 5, Name = "Doctor Strange", Age = 49, Image = "dr_strange.jpg", Weight = 70, Story = @"El doctor Stephen Strange fue creado por Steve Ditko y llevado a Marvel, donde Stan Lee creó las historias para presentarlo en Strange Tales #110 (1963), y fue hasta el número 115 de la misma colección que su identidad fue revelada." },
                new Character { CharacterId = 6, Name = "Deadpool", Age = 60, Image = "hulk.jpg", Weight = 75, Story = @"Wade Winston Wilson, también conocido como Deadpool, es un personaje del universo cinematográfico X-Men y protagonista de su propio grupo de películas. Tiene una fuerza sobrehumana, resistencia, agilidad, reflejos y un rápido y eficaz factor curativo." },
                new Character { CharacterId = 7, Name = "Wolverine", Age = 80, Image = "wolverine.jpg", Weight = 120, Story = @"Es un mutante que posee sentidos afinados a los animales, capacidades físicas mejoradas, poderosa capacidad de regeneración conocida como un factor de curación, y tres garras retráctiles en cada mano." },
                new Character { CharacterId = 8, Name = "Magneto", Age = 78, Image = "magneto.jpg", Weight = 78, Story = @"Un poderoso terrorista mutante con la habilidad de generar y controlar campos magnéticos mentales, Magneto ha sido el enemigo más eminente que hayan tenido los X-Men desde su creación. En sus primeras apariciones, su motivación se debía a la megalomanía." }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { MovieId = 1, Title = "Iron Man", Rating = 4, Date = new System.DateTime(2008, 4, 12), Image = "iron_man_poster.jpg", GenreId = 5 },
                new Movie { MovieId = 2, Title = "Avengers", Rating = 3, Date = new System.DateTime(2012, 4, 12), Image = "avengers_poster.jpg", GenreId = 1 },
                new Movie { MovieId = 3, Title = "Spider-Man: No Way Home", Rating = 3, Date = new System.DateTime(2021, 12, 10), Image = "spiderman_no_way_poster.jpg", GenreId = 2 },
                new Movie { MovieId = 4, Title = "Deadpool", Rating = 2, Date = new System.DateTime(2016, 2, 11), Image = "deadpool_poster.jpg", GenreId = 5 },
                new Movie { MovieId = 5, Title = "X-Men", Rating = 5, Date = new System.DateTime(2000, 11, 2), Image = "xmen_poster.jpg", GenreId = 5 },
                new Movie { MovieId = 6, Title = "X-Men Origins: Wolverine", Rating = 3, Date = new System.DateTime(2009, 3, 23), Image = "wolverine_origins_poster.jpg", GenreId = 1 },
                new Movie { MovieId = 7, Title = "Avengers: Endgame", Rating = 5, Date = new System.DateTime(2019, 4, 12), Image = "avengers_endgame_poster.jpg", GenreId = 2 }                
            );

            modelBuilder.Entity<CharacterMovie>().HasData(
                new CharacterMovie { CharacterId = 1, MovieId = 1 },
                new CharacterMovie { CharacterId = 1, MovieId = 2 },
                new CharacterMovie { CharacterId = 2, MovieId = 2 },
                new CharacterMovie { CharacterId = 3, MovieId = 2 },
                new CharacterMovie { CharacterId = 4, MovieId = 3 },
                new CharacterMovie { CharacterId = 5, MovieId = 3 },
                new CharacterMovie { CharacterId = 6, MovieId = 4 },
                new CharacterMovie { CharacterId = 7, MovieId = 5 },
                new CharacterMovie { CharacterId = 8, MovieId = 5 },
                new CharacterMovie { CharacterId = 6, MovieId = 6 },
                new CharacterMovie { CharacterId = 7, MovieId = 6 },
                new CharacterMovie { CharacterId = 1, MovieId = 7 },
                new CharacterMovie { CharacterId = 2, MovieId = 7 },
                new CharacterMovie { CharacterId = 3, MovieId = 7 },
                new CharacterMovie { CharacterId = 4, MovieId = 7 },
                new CharacterMovie { CharacterId = 5, MovieId = 7 }
            );

        }       
    }
}