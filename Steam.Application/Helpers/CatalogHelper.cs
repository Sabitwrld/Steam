using Steam.Application.DTOs.Catalog.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Helpers
{
    public class CatalogHelper
    {
        public Dictionary<int, ApplicationCatalogReturnDto> MapListToIndexedObject(List<ApplicationCatalogReturnDto> list)
        {
            // List<DTO> obyektini sizin istədiyiniz format olan
            // Dictionary<ID, DTO> formatına çevirir
            return list.ToDictionary(item => item.Id, item => item);
        }
    }
}
