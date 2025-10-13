using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<(IEnumerable<Voucher> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
