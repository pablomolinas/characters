using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Context;
using api.Interfaces;

namespace api.Repositories
{
    public class MoviesRepository : GenericRepository<Movie, CharactersDbContext>, IMovieRepository
    {
        private readonly CharactersDbContext _dbContext;

        public MoviesRepository(CharactersDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
                
        // Filtro peliculas o series por name
        public IEnumerable<Movie> FilterMovieByName(IEnumerable<Movie> movies, string? name)
        {
            if (name == null || name == "") { return movies; }

            var result = movies.Where(c => c.Title.ToLower().Contains(name.ToLower()));

            return result;
        }

        // Filtro personajes por GenreId
        public IEnumerable<Movie> FilterMovieByGenreId(IEnumerable<Movie> movies, int? genreId)
        {
            if (genreId == null || genreId <= 0) { return movies; }

            var result = movies.Where(c => c.GenreId == genreId);

            return result;
        }

        // Ordena resultados ASC | DESC
        public IEnumerable<Movie> FilterMovieOrder(IEnumerable<Movie> movies, string? order)
        {
            if (order == null || (order.ToUpper() != "ASC" && order.ToUpper() != "DESC")) { return movies; }

            if (order.ToUpper() == "ASC")
            {
                return movies.OrderBy(c => c.Title);
            }
            else
            {
                return movies.OrderByDescending(c => c.Title);
            }
        }
    }
}
