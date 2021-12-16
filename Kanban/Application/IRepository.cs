﻿using System;
using System.Threading.Tasks;
using Infrastructure;

namespace Application
{
    public interface IRepository<T, TId> where T : class, IEntity<TId> where TId: struct
    {
        Task<T> GetAsync(TId id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}