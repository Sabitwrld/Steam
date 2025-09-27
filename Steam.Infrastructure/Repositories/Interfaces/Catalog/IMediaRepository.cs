using Steam.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IMediaRepository : IRepository<Media>
    {
        Task<List<Media>> GetByApplicationIdAsync(int applicationId);   
    }
}
