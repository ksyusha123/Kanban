using System;
using System.Threading.Tasks;

namespace Application
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Guid id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}