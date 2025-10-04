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
            // UserLibrary Mappings
            CreateMap<UserLibraryCreateDto, UserLibrary>();

            CreateMap<UserLibrary, UserLibraryReturnDto>();

            CreateMap<UserLibrary, UserLibraryListItemDto>()
                .ForMember(dest => dest.LicenseCount, opt => opt.MapFrom(src => src.Licenses.Count));

            // License Mappings
            CreateMap<LicenseCreateDto, License>();

            // For updates, ignore null values so we can update specific fields.
            CreateMap<LicenseUpdateDto, License>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<License, LicenseReturnDto>()
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name));

            CreateMap<License, LicenseListItemDto>()
                .ForMember(dest => dest.ApplicationName, opt => opt.MapFrom(src => src.Application.Name));
        }
    }
}
