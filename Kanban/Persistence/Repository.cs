using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Persistence
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly KanbanDbContext context;

        public Repository(KanbanDbContext context) => this.context = context;

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id) => await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}