using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Voucher
{
    public record VoucherCreateDto 
    { 
        public string Code { get; init; } = default!; 
        public int ApplicationId { get; init; } 
        public DateTime ExpirationDate { get; init; } 
    }
}
