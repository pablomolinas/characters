using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CharactersRepository : GenericRepository<Character, CharactersDbContext>, ICharacterRepository
    {
        private readonly CharactersDbContext _dbContext;
        public  CharactersRepository(CharactersDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        // Filtro personajes por name
        public IEnumerable<Character> FilterCharacterByName(IEnumerable<Character> characters, string? name)
        {
            if(name == null || name == "" ) { return characters; }

            var result = characters.Where(c => c.Name.ToLower().Contains(name.ToLower()));

            return result;
        }

        // Filtro personajes por edad
        public IEnumerable<Character> FilterCharacterByAge(IEnumerable<Character> characters, int? age)
        {
            if (age == null || age <= 0) { return characters; }

            var result = characters.Where(c => c.Age == age);

            return result;
        }

        // Filtro personajes por pelicula o serie
        public IEnumerable<Character> FilterCharacterByMovieId(IEnumerable<Character> characters, int? MovieId)
        {
            if (MovieId == null || MovieId <= 0) { return characters; }

            var result = characters.Where(c => c.CharacterMovies
                                            .Any(m => m.MovieId == MovieId));

            return result;
        }
    }
}
