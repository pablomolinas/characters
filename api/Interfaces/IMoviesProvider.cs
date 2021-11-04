using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMoviesProvider
    {
        Task<ICollection<Movie>> GetAllAsync();

        Task<ICollection<Movie>> SearchAsync(string search);
        Task<Movie> GetAsync(int id);
        Task<bool> UpdateAsync(int id, Movie movie);

        Task<(bool IsSuccess, int? Id)> AddAsync(Movie movie);

        Task<bool> DeleteAsync(int id);
    }
}
