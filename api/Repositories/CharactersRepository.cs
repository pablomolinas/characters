using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class CharactersRepository : GenericRepository<Character, CharactersDbContext>, ICharacterRepository
    {
        private readonly CharactersDbContext _dbContext;
        public  CharactersRepository(CharactersDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
