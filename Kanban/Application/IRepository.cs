using System;
using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetAsync(Guid id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}