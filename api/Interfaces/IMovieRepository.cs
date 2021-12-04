using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        // Metodos particulares para Movie
        public IEnumerable<Movie> FilterMovieByName(IEnumerable<Movie> movies, string? name);
        public IEnumerable<Movie> FilterMovieByGenreId(IEnumerable<Movie> movies, int? genreId);
        public IEnumerable<Movie> FilterMovieOrder(IEnumerable<Movie> movies, string? order);
    }
}
