using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class moviesController : ControllerBase
    {
        private IMovieService _movieService;

        public moviesController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        // GET: <MoviesController>
        [HttpGet]
        public async Task<IActionResult> GetMovieList(string? name, int? genreId, string? order)
        {
            var results = await this._movieService.GetMovieList(name, genreId, order);
            if (results.Success)
            {                
                return Ok(results);
            }

            return BadRequest(results);
        }

        // GET <MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var result = await this._movieService.GetMovieById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // POST <MoviesController>
        [HttpPost]
        public async Task<IActionResult> AddMovie(Movie movie)
        {            
            var result = await this._movieService.AddMovie(movie);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // PUT <MoviesController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(Movie movie)
        {
            var result = await this._movieService.UpdateMovie(movie);
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }

        // DELETE <MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await this._movieService.DeleteMovie(id);
            if (result.Success)
            {                
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
