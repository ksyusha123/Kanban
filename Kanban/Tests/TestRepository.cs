using System.Collections.Generic;
using System.Threading.Tasks;
using Application;
using Infrastructure;

namespace Tests
{
    public class TestRepository<T> : IRepository<T> where T : IEntity
    {
        public Dictionary<string, T> Entities;

        public TestRepository() : this(new Dictionary<string, T>())
        {
        }

        public TestRepository(Dictionary<string, T> entities) => Entities = entities;

        public Task<T> GetAsync(string id) => Task.Run(() => Entities[id]);

        public Task AddAsync(T entity) => Task.Run(() => Entities[entity.Id] = entity);

        public Task AddAsync(IEnumerable<T> entities) =>
            Task.Run(() =>
            {
                foreach (var entity in entities) Entities[entity.Id] = entity;
                return null;
            });

        public Task UpdateAsync(T entity) => Task.Run(() => Entities[entity.Id] = entity);

        public Task DeleteAsync(T entity) => Task.Run(() => Entities.Remove(entity.Id));

        public Task DeleteAsync(IEnumerable<T> entities) =>
            Task.Run(() =>
            {
                foreach (var entity in entities) Entities.Remove(entity.Id);
                return null;
            });
    }
}