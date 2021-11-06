using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Provider
{
    public class SqlServerMoviesProvider : IMoviesProvider
    {
        private readonly CharactersDbContext db;

        public SqlServerMoviesProvider(CharactersDbContext db)
        {
            this.db = db;
        }

        public async Task<(bool IsSuccess, int? Id)> AddAsync(Movie movie)
        {
            this.db.Movies.Add(movie);
            var newId = await this.db.SaveChangesAsync();
            return (true, newId);
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
            var results = await this.db.Movies.ToListAsync();
            return results;
        }

        public async Task<Movie> GetAsync(int id)
        {
            var result = await this.db.Movies.FirstOrDefaultAsync(c => c.MovieId == id);
            if (result != null) {
                result.Genre = await this.db.Genres.FirstOrDefaultAsync(c => c.GenreId == result.GenreId);                
            }
            return result;
        }

        public async Task<ICollection<Movie>> SearchTitleAsync(string search)
        {
            var raw = this.db.Movies.FromSqlRaw($"SELECT * FROM Movies WHERE Title LIKE '%{search}%'");            
            var results = await raw.ToListAsync();

            return results;
        }

        public async Task<bool> UpdateAsync(int id, Movie movie)
        {
            this.db.Movies.Update(movie);
            var results = await this.db.SaveChangesAsync();

            return results == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var results = 0;
            var movie = await this.db.Movies.FirstAsync<Movie>(x => x.MovieId == id);

            if (movie != null)
            {
                this.db.Movies.Remove(movie);
                results = await this.db.SaveChangesAsync();
            }

            return results == 1;
        }
    }
}
