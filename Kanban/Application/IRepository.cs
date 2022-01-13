using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetAsync(string id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}