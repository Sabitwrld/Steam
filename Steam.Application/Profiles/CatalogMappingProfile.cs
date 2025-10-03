using AutoMapper;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Domain.Entities.Catalog;

namespace Steam.Application.Profiles
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            // ApplicationCatalog
            CreateMap<ApplicationCatalog, ApplicationCatalogReturnDto>();
            CreateMap<ApplicationCatalog, ApplicationCatalogListItemDto>();
            CreateMap<ApplicationCatalogCreateDto, ApplicationCatalog>();
            CreateMap<ApplicationCatalogUpdateDto, ApplicationCatalog>();

            // Genre
            CreateMap<Genre, GenreReturnDto>();
            CreateMap<Genre, GenreListItemDto>();
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();

            // Tag
            CreateMap<Tag, TagReturnDto>();
            CreateMap<Tag, TagListItemDto>();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();

            // Media
            CreateMap<Media, MediaReturnDto>();
            CreateMap<Media, MediaListItemDto>();
            CreateMap<MediaCreateDto, Media>();
            CreateMap<MediaUpdateDto, Media>();

            // SystemRequirements
            CreateMap<SystemRequirements, SystemRequirementsReturnDto>();
            CreateMap<SystemRequirements, SystemRequirementsListItemDto>();
            CreateMap<SystemRequirementsCreateDto, SystemRequirements>();
            CreateMap<SystemRequirementsUpdateDto, SystemRequirements>();
        }
    }

}
