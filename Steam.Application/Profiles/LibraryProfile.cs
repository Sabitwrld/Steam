using AutoMapper;
using Steam.Application.DTOs.Library.License;
using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Domain.Entities.Library;

namespace Steam.Application.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            // UserLibrary
            CreateMap<UserLibraryCreateDto, UserLibrary>();
            CreateMap<UserLibraryUpdateDto, UserLibrary>();
            CreateMap<UserLibrary, UserLibraryReturnDto>();
            CreateMap<UserLibrary, UserLibraryListItemDto>()
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses.Count));

            // License
            CreateMap<LicenseCreateDto, License>();
            CreateMap<LicenseUpdateDto, License>();
            CreateMap<License, LicenseReturnDto>();
            CreateMap<License, LicenseListItemDto>();
        }
    }
}
