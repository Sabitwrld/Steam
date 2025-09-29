using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Campaign
{
    public record CampaignCreateDto 
    { 
        public string Name { get; init; } = default!; 
        public DateTime StartDate { get; init; } 
        public DateTime EndDate { get; init; } 
    }
}
