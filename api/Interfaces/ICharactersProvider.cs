using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ICharactersProvider
    {
        Task<ICollection<Character>> GetAllAsync();

        Task<ICollection<Character>> SearchNameAsync(string name);
        Task<Character> GetAsync(int id);
        Task<bool> UpdateAsync(int id, Character character);

        Task<(bool IsSuccess, int? Id)> AddAsync(Character character);

        Task<bool> DeleteAsync(int id);
    }
}
