﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Domain.Common;
using OnionArchitectureApp.Persistence.Context;
using System.Linq.Expressions;

namespace OnionArchitectureApp.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll(params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _context.Set<T>();
        query = IncludeProperties(query, includeProperties);

        return query;
    }

    public IQueryable<T> GetFiltered(Expression<Func<T, bool>> expression, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties)
    {
        var query = _context.Set<T>().AsQueryable();
        query = IncludeProperties(query, includeProperties);

        return query.Where(expression);
    }

    public async Task<T> GetByIdAsync(Guid id, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties)
    {
        var query = _context.Set<T>().AsQueryable();
        query = IncludeProperties(query, includeProperties);

        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Entry<T>(entity).State = EntityState.Modified;
    }


    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        Update(entity);
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AnyAsync(expression);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private IQueryable<T> IncludeProperties(IQueryable<T> query, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeProperties)
    {
        foreach (var includeProperty in includeProperties)
        {
            query = includeProperty(query);
        }
        return query;
    }
}
