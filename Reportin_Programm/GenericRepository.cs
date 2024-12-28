using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Reportin_Programm
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly EfCoreDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EfCoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<Guid> Add(T item)
        {
            item.Id = Guid.NewGuid();
            _dbSet.Add(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;

            return await _dbContext.SaveChangesAsync() != 0;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id{id} Not Found");
            }
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with {id} Not Found");

            return entity;
        }

        public async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (entity == null)
                throw new KeyNotFoundException("Entity not found");
            return entity;
        }

    }
}
