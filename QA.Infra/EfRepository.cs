using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentStore.Core.Interfaces;
using DocumentStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace QA.Infra
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity, IEntity
    {
        private readonly QAContext _dbContext;

        public EfRepository(QAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetById(string id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<List<T>> List()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> RecordExists(string id)
        {
            return _dbContext.Set<T>().AnyAsync(r => r.Id == id);
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}