using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryCreateDto
    {
        public int UserId { get; init; }
    }
}
