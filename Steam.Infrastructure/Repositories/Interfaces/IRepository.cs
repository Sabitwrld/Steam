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

        // DƏYİŞİKLİK: saveChanges parametri ləğv edildi
        Task<T> CreateAsync(T entity);

        // DƏYİŞİKLİK: saveChanges parametri ləğv edildi və sinxron oldu
        void Update(T entity);

        // DƏYİŞİKLİK: saveChanges parametri ləğv edildi və sinxron oldu
        void Delete(T entity, bool hardDelete = false);

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

    }
}
