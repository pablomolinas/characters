using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Provider
{
    public class SqlServerCharactersProvider : ICharactersProvider
    {
        private readonly CharactersDbContext db;

        public SqlServerCharactersProvider(CharactersDbContext db) {
            this.db = db;
        }        
        
        public async Task<(bool IsSuccess, int? Id)> AddAsync(Character character)
        {            
            this.db.Characters.Add(character);
            var newId = await this.db.SaveChangesAsync();
            return (true, newId);
        }

        public async Task<ICollection<Character>> GetAllAsync()
        {
            var results = await this.db.Characters.ToListAsync();
            return results;            
        }

        public async Task<Character> GetAsync(int id)
        {
            var result = await this.db.Characters.FirstOrDefaultAsync(c => c.CharacterId == id);
            return result;
        }

        public async Task<ICollection<Character>> SearchNameAsync(string name)
        {            
            var raw = this.db.Characters.FromSqlRaw($"SELECT * FROM Characters WHERE Name LIKE '%{name}%'");
            var results = await raw.ToListAsync();

            return results;
        }

        public async Task<bool> UpdateAsync(int id, Character character)
        {
            this.db.Characters.Update(character);
            var results = await this.db.SaveChangesAsync();

            return results == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var results = 0;
            var character = await this.db.Characters.FirstAsync<Character>(x => x.CharacterId == id);
            
            if( character != null)
            {
                this.db.Characters.Remove(character);
                results = await this.db.SaveChangesAsync();                
            }

            return results == 1;
        }
    }
}
