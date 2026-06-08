using BuildManager.Contexts;
using BuildManager.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildManager.Repositories
{
    public class Repository<K, T> : IRepository<K, T> where T : class
    {
        private readonly BuildManagerDbContext _context;

        public Repository(BuildManagerDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(K key)
        {
            var entity = await Get(key);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(K key)
        {
            var entity = await _context.Set<T>().FindAsync(key);
            if (entity == null)
                throw new BuildManager.Exceptions.EntityNotFoundException(typeof(T).Name, key!);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }
    }
}
