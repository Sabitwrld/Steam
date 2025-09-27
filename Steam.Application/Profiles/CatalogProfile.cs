using AutoMapper;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<ApplicationCatalogCreateDto, ApplicationCatalog>();
            CreateMap<ApplicationCatalogUpdateDto, ApplicationCatalog>();
            CreateMap<ApplicationCatalog, ApplicationCatalogReturnDto>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.MediaUrls, opt => opt.MapFrom(src => src.Media.Select(m => m.Url)));
            CreateMap<ApplicationCatalog, ApplicationCatalogListItemDto>();

            CreateMap<Genre, GenreReturnDto>();
            CreateMap<Genre, GenreListItemDto>();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();
        }
    }
}
