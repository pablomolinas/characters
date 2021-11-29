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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await this._dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();            
            return entity;
        }  

        public async Task DeleteAsync(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
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

        public async Task UpdateAsync(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Attach(entity);
            this._dbContext.Entry(entity).State = EntityState.Modified;            
            
            await this._dbContext.SaveChangesAsync();
        }
    }
}
