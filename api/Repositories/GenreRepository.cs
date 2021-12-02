using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;

namespace api.Repositories
{
    public class GenreRepository : GenericRepository<Genre, CharactersDbContext>, IGenreRepository
    {
        private readonly CharactersDbContext _dbContext;
        public GenreRepository(CharactersDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }    
    }
}
