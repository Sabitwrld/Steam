using Steam.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IApplicationCatalogRepository : IRepository<ApplicationCatalog>
    {
        Task<List<ApplicationCatalog>> GetByReleaseDateAsync(DateTime from, DateTime to);   
    }
}
