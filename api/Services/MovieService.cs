using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Result> DeleteMovie(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if(movie != null)
            {
                await _movieRepository.DeleteAsync(movie);
                return Result<string>.SuccessResult("Borrado con exito!");
            }else
            {
                return Result.FailureResult("No existe movie.");
            }
        }
    }
}
