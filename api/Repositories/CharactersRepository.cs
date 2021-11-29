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

        /*public async Task<ICollection<Character>> SearchNameAsync(string name)
        {
            var raw = _dbSet.FromSqlRaw($"SELECT * FROM Characters WHERE Name LIKE '%{name}%'");
            var results = await raw.ToListAsync();

            return results;
        }*/
    }
}
