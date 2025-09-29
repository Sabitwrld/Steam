using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftCreateDto 
    { 
        public int SenderUserId { get; init; } 
        public int ReceiverUserId { get; init; } 
        public int ApplicationId { get; init; } 
    }
}
