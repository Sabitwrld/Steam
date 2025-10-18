using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Admin
{
    public class AssignRolesRequestDto
    {
        public List<string> Roles { get; set; }
    }
}
