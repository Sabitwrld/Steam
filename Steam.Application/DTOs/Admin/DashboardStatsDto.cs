using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Admin
{
    public class DashboardStatsDto
    {
        public decimal TotalRevenue { get; set; }
        public int NewUsersLast30Days { get; set; }
        public int TotalOrders { get; set; }
        public int PendingReviews { get; set; }
    }
}
