using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public decimal BasePrice { get; init; }
    }
}
