using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;


namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class moviesController : ControllerBase
    {
        private IMoviesProvider moviesProvider;

        public moviesController(IMoviesProvider moviesProvider)
        {
            this.moviesProvider = moviesProvider;
        }

        // GET: <MoviesController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await this.moviesProvider.GetAllAsync();
            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }

        // GET <MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await this.moviesProvider.GetAsync(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(id);
        }

        // POST <MoviesController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest();
            }

            var result = await this.moviesProvider.AddAsync(movie);
            if (result.IsSuccess)
            {
                return Ok(result.Id);
            }

            return NotFound();
        }

        // PUT <MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Movie movie)
        {
            var result = await this.moviesProvider.UpdateAsync(id, movie);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        // DELETE <MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.moviesProvider.DeleteAsync(id);

            if (result)
            {
                return Ok(result);
            }

            return NotFound(id);
        }
    }
}
