using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using api.ViewModels;
using api.ViewModels.Response;
using api.Services;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class charactersController : ControllerBase
    {
        private ICharacterService _charactersService;

        public charactersController(ICharacterService charactersService)
        {
            this._charactersService = charactersService;
        }

        // GET: <CharactersController>
        [HttpGet]
        public async Task<IActionResult> GetCharacterList(string? name, int? age, int? movie)
        {
            var result = await this._charactersService.GetCharacterList(name, age, movie);
            if (result.Success)
            {                
                return Ok(result);
            }

            return BadRequest(result);
        }

        // GET <CharactersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            var result = await this._charactersService.GetCharacterById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // POST <CharactersController>
        [HttpPost]
        public async Task<IActionResult> AddCharacter(Character character)
        {
            var result = await this._charactersService.AddCharacter(character);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // PUT <CharactersController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(Character character)
        {
            var result = await this._charactersService.UpdateCharacter(character);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);            
        }

        // DELETE <CharactersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var result = await this._charactersService.DeleteCharacter(id);
            if(result.Success) 
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
