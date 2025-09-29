using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointCreateDto
    {
        public int ApplicationId { get; init; }
        public decimal BasePrice { get; init; }
    }
}
