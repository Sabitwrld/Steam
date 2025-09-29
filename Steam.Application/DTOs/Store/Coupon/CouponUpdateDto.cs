using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Coupon
{
    public record CouponUpdateDto 
    { 
        public int Id { get; init; } 
        public string Code { get; init; } = default!; 
        public double Percentage { get; init; } 
        public DateTime ExpirationDate { get; init; } 
        public bool IsActive { get; init; } 
    }
}
