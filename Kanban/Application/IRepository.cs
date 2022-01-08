using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public interface IRepository<T, in TId> where T : IEntity<TId>
    {
        Task<T> GetAsync(TId id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}