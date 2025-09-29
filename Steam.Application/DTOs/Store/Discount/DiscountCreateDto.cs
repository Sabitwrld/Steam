using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Discount
{
    public record DiscountCreateDto 
    {
        public int PricePointId { get; init; } 
        public double Percentage { get; init; } 
        public DateTime StartDate { get; init; } 
        public DateTime EndDate { get; init; } 
    }
}
