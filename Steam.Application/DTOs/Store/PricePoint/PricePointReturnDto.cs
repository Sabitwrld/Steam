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
        public decimal BasePrice { get; init; }
    }
}
