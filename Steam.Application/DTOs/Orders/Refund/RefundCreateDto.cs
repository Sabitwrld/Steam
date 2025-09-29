using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Refund
{
    public record RefundCreateDto
    {
        public int PaymentId { get; init; }
        public string Reason { get; init; } = string.Empty;
    }
}
