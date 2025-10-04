using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryUpdateDto
    {
        // For now, there isn't much to update on a library itself, 
        // but we keep the DTO for future features. 
        // We'll use the Id from the route.
        public int UserId { get; init; }
    }
}
