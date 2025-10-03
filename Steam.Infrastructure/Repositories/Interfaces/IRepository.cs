using System.Linq.Expressions;

namespace Steam.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetEntityAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[]? includes);

        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            int skip = 0,
            int take = 0,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[] includes);

        Task<T> CreateAsync(T entity, bool saveChanges = true);

        Task<T> UpdateAsync(T entity, bool saveChanges = true);

        /// <summary>
        /// Soft delete by default. Hard delete üçün hardDelete = true.
        /// </summary>
        Task<bool> DeleteAsync(T entity, bool hardDelete = false, bool saveChanges = true);

        Task<bool> IsExistsAsync(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool isIgnoredDeleteBehaviour = false);

        IQueryable<T> GetQuery(
            Expression<Func<T, bool>>? predicate = null,
            bool asNoTracking = false,
            bool asSplitQuery = false,
            bool isIgnoredDeleteBehaviour = false,
            params Func<IQueryable<T>, IQueryable<T>>[]? includes);
        Task<T?> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[]? includes);

        Task<int> SaveChangesAsync();
    }
}
