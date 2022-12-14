using E_ticket.Models;
using System.Linq.Expressions;

namespace E_ticket.Data.Repository
{
    public interface IBaseRepository<T> where T : class, IEntityBase , new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        //T GetByName(string name);
        Task AddAsync(T  entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
