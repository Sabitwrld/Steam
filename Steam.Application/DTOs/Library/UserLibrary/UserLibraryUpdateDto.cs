using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
    }
}
