using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Coupon
{
    public record CouponCreateDto
    {
        public string Code { get; init; } = default!;
        public decimal Percentage { get; init; }
        public DateTime ExpirationDate { get; init; }
    }
}
