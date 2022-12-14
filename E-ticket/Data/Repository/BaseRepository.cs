using E_ticket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace E_ticket.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().SingleOrDefaultAsync(a => a.Id == id);


        public async Task DeleteAsync(int id)
        {
            var entity= await _context.Set<T>().SingleOrDefaultAsync(a => a.Id == id);

            EntityEntry entry = _context.Entry<T>(entity);
            entry.State = EntityState.Deleted ;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entry= _context.Entry<T>(entity);
            entry.State =  EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
    }
}
