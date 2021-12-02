using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Response;

namespace api.Services
{
    public class MoviesService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesService(IMovieRepository movieRepository)
        {
            this._movieRepository = movieRepository;
        }

        public Task<Result> AddMovie(Movie character)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteMovie(Movie character)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ExistMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetMovieById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> GetMovieList()
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateMovie(Movie character)
        {
            throw new NotImplementedException();
        }
    }
}
