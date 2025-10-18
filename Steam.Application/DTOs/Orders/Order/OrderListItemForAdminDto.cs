using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderListItemForAdminDto
    {
        public int Id { get; init; }
        public DateTime OrderDate { get; init; }
        public int ItemCount { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = string.Empty;
        public string UserEmail { get; init; } = string.Empty; // Əlavə edildi
    }
}
