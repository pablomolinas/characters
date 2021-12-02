using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public abstract class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class where TContext : CharactersDbContext
    {
        private readonly TContext _dbContext;
        public GenericRepository(TContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<bool> AddAsync(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            var affected = await _dbContext.SaveChangesAsync();            
            return affected != 0;
        }  

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Remove(entity);
            var affected = await _dbContext.SaveChangesAsync();
            return affected != 0;
        }

        public async Task<bool> EntityExists(int id)
        {
            var e = await this._dbContext.Set<TEntity>().FindAsync(id);
            return e != null;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this._dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyList<TEntity>> GetListAsync()
        {
            return await this._dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Attach(entity);
            this._dbContext.Entry(entity).State = EntityState.Modified;            
            
            var affected = await this._dbContext.SaveChangesAsync();
            return affected != 0;
        }
    }
}
