using AutoMapper;
using Steam.Application.DTOs.Auth;
using Steam.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            // Mapping for user management
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Ignore Roles, as it will be populated manually in the service
        }
    }
}
