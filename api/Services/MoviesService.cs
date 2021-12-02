using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Repositories;
using api.Response;
using api.ModelsViews;

namespace api.Services
{
    public class MoviesService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesService(IMovieRepository movieRepository)
        {
            this._movieRepository = movieRepository;
        }

        public async Task<Result> AddMovie(Movie movie)
        {
            if (movie == null) { return Result.FailureResult("Datos invalidos."); }

            var result = await this._movieRepository.AddAsync(movie);
            if (result)
            {
                return Result<string>.SuccessResult("Pelicula agregada exitosamente.");
            }

            return Result.FailureResult("No fue posible agregar pelicula.");
        }

        public async Task<Result> DeleteMovie(int id)
        {
            var movie = await this._movieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                var r = await this._movieRepository.DeleteAsync(movie);
                if (r)
                {
                    return Result<string>.SuccessResult("Pelicula eliminada exitosamente.");
                }

                return Result.FailureResult("No fue posible eliminar Pelicula.");
            }

            return Result.FailureResult("Id de Pelicula invalida.");
        }

        public async Task<Result> ExistMovie(int id)
        {
            var movie = await this._movieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                return Result.SuccessResult();
            }

            return Result.FailureResult("Pelicula o serie inexistente.");
        }

        public async Task<Result> GetMovieById(int id)
        {
            var movie = await this._movieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                return Result<Movie>.SuccessResult(movie);
            }

            return Result.FailureResult("Pelicula o Serie inexistente.");
        }

        public async Task<Result> GetMovieList()
        {
            var results = await this._movieRepository.GetListAsync();
            if (results != null)
            {
                // Convierto a vista, para enviar solo los campos que deseo mostrar
                var view = new List<MoviesListView>();

                foreach (Movie c in results)
                {
                    view.Add(new MoviesListView(c));
                }
                return Result<List<MoviesListView>>.SuccessResult(view);
            }

            return Result.FailureResult("Sin resultados");
        }

        public async Task<Result> UpdateMovie(Movie movie)
        {
            if (this._movieRepository.GetByIdAsync(movie.MovieId) == null) { return Result.FailureResult("La Pelicula o Serie enviada no existe."); }

            var r = await this._movieRepository.UpdateAsync(movie);
            if (r) 
            {
                return Result<string>.SuccessResult("Pelicula actualizada exitosamente.");
            }

            return Result.FailureResult("Pelicula no actualizada.");
        }
    }
}
