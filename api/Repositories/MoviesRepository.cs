using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;

namespace api.Repositories
{
    public class MoviesRepository : GenericRepository<Movie, CharactersDbContext>, IMovieRepository
    {
        private readonly CharactersDbContext _dbContext;

        public MoviesRepository(CharactersDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
