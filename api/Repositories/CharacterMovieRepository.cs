using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;

namespace api.Repositories
{
    public class CharacterMovieRepository : GenericRepository<CharacterMovie, CharactersDbContext>, ICharacterMovieRepository
    {
        private readonly CharactersDbContext _dbContext;
        private readonly IMovieRepository _movieRepository;
        private readonly ICharacterRepository _characterRepository;
        public CharacterMovieRepository(CharactersDbContext dbContext, IMovieRepository movieRepository, ICharacterRepository characterRepository) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._movieRepository = movieRepository;
            this._characterRepository = characterRepository;
        }

        // Entrega listado de peliculas asociadas a un personaje
        public async Task<ICollection<CharacterMovie>> GetMoviesByCharacterId(int CharacterId)
        {
            var movies = new List<CharacterMovie>();
            var allitems = await this.GetListAsync();
            if(allitems != null)
            {
                foreach (CharacterMovie c in allitems)
                {
                    if (c.CharacterId == CharacterId)
                    {
                        c.Movie = await this._movieRepository.GetByIdAsync(c.MovieId);
                        movies.Add(c);
                    }                    
                }
            }

            return movies;
        }

        // Entrega listado de personajes asociados a una pelicula
        public async Task<ICollection<CharacterMovie>> GetCharactersByMovieId(int MovieId)
        {
            var characters = new List<CharacterMovie>();
            var allitems = await this.GetListAsync();
            if (allitems != null)
            {
                foreach (CharacterMovie c in allitems)
                {
                    if (c.MovieId == MovieId)
                    {
                        c.Character = await this._characterRepository.GetByIdAsync(c.CharacterId);
                        characters.Add(c);
                    }
                }
            }

            return characters;
        }
    }
}
