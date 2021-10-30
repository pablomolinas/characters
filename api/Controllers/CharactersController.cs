using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public string Get(int id)
        {
            return "value";
        }

        // POST <CharactersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT <CharactersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <CharactersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
