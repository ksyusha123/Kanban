﻿using Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Infrastructure;
using Task = System.Threading.Tasks.Task;

namespace Persistence
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly KanbanDbContext _context;

        public Repository(KanbanDbContext context) => _context = context;

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(string id) => await _context.Set<T>().FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}