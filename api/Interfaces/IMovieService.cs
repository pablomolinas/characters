using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Response;
using api.Models;

namespace api.Interfaces
{
    public interface IMovieService
    {
        public Task<Result> GetMovieById(int id);

        public Task<Result> GetMovieList();
        public Task<Result> AddMovie(Movie character);
        public Task<Result> UpdateMovie(Movie character);
        public Task<Result> DeleteMovie(int id);
        public Task<Result> ExistMovie(int id);
    }
}
