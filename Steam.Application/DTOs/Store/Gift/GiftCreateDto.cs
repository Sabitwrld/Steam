using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftCreateDto
    {
        // Names changed from SenderUserId and ReceiverUserId to match the validator
        public int SenderId { get; init; }
        public int ReceiverId { get; init; }
        public int ApplicationId { get; init; }
    }
}
