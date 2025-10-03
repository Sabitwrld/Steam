using Steam.Application.DTOs.Store.RegionalPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string Name { get; init; } = default!;
        public decimal BasePrice { get; init; }
        public List<RegionalPriceReturnDto> RegionalPrices { get; init; } = new();
    }
}
