using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
#nullable enable

namespace api.Interfaces
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        // Metodos particulares de Character
        public IEnumerable<Character> FilterCharacterByName(IEnumerable<Character> characters, string? name);
        public IEnumerable<Character> FilterCharacterByAge(IEnumerable<Character> characters, int? age);
        public IEnumerable<Character> FilterCharacterByMovieId(IEnumerable<Character> characters, int? MovieId);
    }
}
