using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Library
{
    public class UserLibrary : BaseEntity
    {
        public int UserId { get; set; }
        public ICollection<License> Licenses { get; set; } = new List<License>();
    }
}
