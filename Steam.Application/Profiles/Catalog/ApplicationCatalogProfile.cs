using AutoMapper;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Profiles.Catalog
{
    public class ApplicationCatalogProfile : Profile
    {
        public ApplicationCatalogProfile()
        {
            CreateMap<ApplicationCatalogCreateDto, ApplicationCatalog>();
            CreateMap<ApplicationCatalogUpdateDto, ApplicationCatalog>();

            CreateMap<ApplicationCatalog, ApplicationCatalogReturnDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.MediaUrls, opt => opt.MapFrom(src => src.Media.Select(m => m.Url)));

            CreateMap<ApplicationCatalog, ApplicationCatalogListItemDto>();
        }
    }
}
