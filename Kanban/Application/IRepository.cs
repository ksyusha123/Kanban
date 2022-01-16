using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(string id);

        Task AddAsync(T entity);

        Task AddAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(IEnumerable<T> entities);
    }
}