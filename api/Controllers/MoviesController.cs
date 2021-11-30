using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using api.ModelsViews;


namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class moviesController : ControllerBase
    {
        private IMovieRepository _moviesRepository;

        public moviesController(IMovieRepository moviesRepository)
        {
            this._moviesRepository = moviesRepository;
        }

        // GET: <MoviesController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await this._moviesRepository.GetListAsync();
            if (results != null)
            {
                // Convierto a vista, para enviar solo los campos que deseo mostrar
                var view = new List<MoviesListView>();

                foreach (Movie c in results)
                {
                    view.Add(new MoviesListView(c));
                }
                return Ok(view);
            }

            return BadRequest("Sin resultados");
        }

        // GET <MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await this._moviesRepository.GetByIdAsync(id);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("El Id enviado no existe.");
        }

        // POST <MoviesController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(Movie movie)
        {
            if (movie == null){ return BadRequest("Datos invalidos."); }

            var result = await this._moviesRepository.AddAsync(movie);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("A ocurrido un error, no se ha creado la Pelicula");
        }

        // PUT <MoviesController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Movie movie)
        {
            if (this._moviesRepository.GetByIdAsync(movie.MovieId) == null) { return BadRequest("La Pelicula o Serie enviada no existe."); }
            
            await this._moviesRepository.UpdateAsync(movie);
            
            return Ok("Pelicula modificada con éxito.");
        }

        // DELETE <MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var e = await this._moviesRepository.GetByIdAsync(id);
            if (e != null)
            {
                await this._moviesRepository.DeleteAsync(e);
                return Ok("Pelicula eliminada con exito.");
            }

            return BadRequest("El pelicula enviada no existe.");
        }
    }
}
