using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Campaign
{
    public record CampaignListItemDto 
    { 
        public int Id { get; init; } 
        public string Name { get; init; } = default!; 
    }
}
