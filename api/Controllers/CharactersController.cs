using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class charactersController : ControllerBase
    {
        private ICharactersProvider charactersProvider;

        public charactersController(ICharactersProvider charactersProvider)
        {
            this.charactersProvider = charactersProvider;
        }


        // GET: <CharactersController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await this.charactersProvider.GetAllAsync();
            if (results != null)
            {
                return Ok(results);
            }

            return NotFound();
        }

        // GET <CharactersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await this.charactersProvider.GetAsync(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(id);
        }

        // POST <CharactersController>
        [HttpPost]
        public async Task<IActionResult> AddAsync(Character character)
        {
            if(character == null)
            {
                return BadRequest();
            }

            var result = await this.charactersProvider.AddAsync(character);
            if (result.IsSuccess)
            {
                return Ok(result.Id);
            }

            return NotFound();
        }

        // PUT <CharactersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Character character)
        {
            var result = await this.charactersProvider.UpdateAsync(id, character);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        // DELETE <CharactersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await this.charactersProvider.DeleteAsync(id);

            if (result)
            {
                return Ok(result);
            }

            return NotFound(id);
        }
    }
}
