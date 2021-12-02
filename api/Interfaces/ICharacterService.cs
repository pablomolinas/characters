using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Response;
using api.Models;

namespace api.Interfaces
{
    public interface ICharacterService
    {
        public Task<Result> GetCharacterById(int id);

        public Task<Result> GetCharacterList();
        public Task<Result> AddCharacter(Character character);
        public Task<Result> UpdateCharacter(Character character);
        public Task<Result> DeleteCharacter(int id);
        public Task<Result> ExistCharacter(int id);
    }
}
