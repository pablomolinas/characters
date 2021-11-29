using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using api.ModelsViews;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class charactersController : ControllerBase
    {
        private ICharacterRepository _charactersRepository;

        public charactersController(ICharacterRepository charactersRepository)
        {
            this._charactersRepository = charactersRepository;
        }


        // GET: <CharactersController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var results = await this._charactersRepository.GetListAsync();
            if (results != null)
            {
                // Convierto a vista, para enviar solo los campos que deseo mostrar
                var view = new List<CharactersListView>();
                                
                foreach (Character c in results)
                {
                    view.Add(new CharactersListView(c));
                }
                return Ok(view);
            }

            return NotFound();
        }

        // GET <CharactersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await this._charactersRepository.GetByIdAsync(id);

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
            if (character == null) { return BadRequest("Datos invalidos."); }

            var result = await this._charactersRepository.AddAsync(character);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("A ocurrido un error, no se ha creado un nuevo Character");
        }

        // PUT <CharactersController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Character character)
        {
            if (this._charactersRepository.GetByIdAsync(character.CharacterId) == null) { return BadRequest("El Character enviado no existe."); }

            await this._charactersRepository.UpdateAsync(character);
            
            return Ok("Character modificado con éxito.");
            
        }

        // DELETE <CharactersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var e = await this._charactersRepository.GetByIdAsync(id);
            if(e != null)
            {
                await this._charactersRepository.DeleteAsync(e);
                return Ok("Character eliminado con exito.");
            }

            return BadRequest("El Character enviado no existe.");
        }
    }
}
