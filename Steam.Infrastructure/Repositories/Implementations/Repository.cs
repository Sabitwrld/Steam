using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Common;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Steam.Infrastructure.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity, bool saveChanges = true)
        {
            await _dbSet.AddAsync(entity);
            if (saveChanges) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity, bool saveChanges = true)
        {
            _dbSet.Update(entity);
            if (saveChanges) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity, bool hardDelete = false, bool saveChanges = true)
        {
            if (hardDelete)
                _dbSet.Remove(entity);
            else
                entity.IsDeleted = true;

            if (saveChanges)
                await _context.SaveChangesAsync();

            return true;
        }

        public async Task<T?> GetEntityAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (skip > 0) query = query.Skip(skip);
            if (take > 0) query = query.Take(take);
            if (asNoTracking) query = query.AsNoTracking();
            if (asSplitQuery) query = query.AsSplitQuery();
            if (isIgnoredDeleteBehaviour) query = query.IgnoreQueryFilters();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (skip > 0) query = query.Skip(skip);
            if (take > 0) query = query.Take(take);
            if (asNoTracking) query = query.AsNoTracking();
            if (asSplitQuery) query = query.AsSplitQuery();
            if (isIgnoredDeleteBehaviour) query = query.IgnoreQueryFilters();

            return await query.ToListAsync();
        }

        public IQueryable<T> GetQuery(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (asNoTracking) query = query.AsNoTracking();
            if (asSplitQuery) query = query.AsSplitQuery();
            if (isIgnoredDeleteBehaviour) query = query.IgnoreQueryFilters();

            return query;
        }

        public async Task<bool> IsExistsAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool isIgnoredDeleteBehaviour = false)
        {
            IQueryable<T> query = _dbSet;
            if (asNoTracking) query = query.AsNoTracking();
            if (isIgnoredDeleteBehaviour) query = query.IgnoreQueryFilters();
            return predicate != null && await query.AnyAsync(predicate);
        }
        public async Task<T?> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();


    }
}


