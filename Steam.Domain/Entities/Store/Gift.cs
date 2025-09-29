using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Gift : BaseEntity
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsRedeemed { get; set; }
    }
}
