using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Voucher
{
    public record VoucherUpdateDto
    {
        public int Id { get; init; }
        public bool IsUsed { get; init; }
    }
}
