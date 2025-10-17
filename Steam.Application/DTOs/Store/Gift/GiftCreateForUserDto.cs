using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Gift
{
    public record GiftCreateForUserDto
    {
        public string ReceiverUsername { get; init; } = default!;
        public int ApplicationId { get; init; }
    }
}
