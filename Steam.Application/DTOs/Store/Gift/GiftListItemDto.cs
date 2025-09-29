using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftListItemDto 
    { 
        public int Id { get; init; } 
        public int ReceiverUserId { get; init; } 
        public bool IsRedeemed { get; init; } 
    }
}
