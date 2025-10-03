using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class ApplicationCatalogService : IApplicationCatalogService
    {
        private readonly IRepository<ApplicationCatalog> _repo;
        private readonly IRepository<Genre> _genreRepo;
        private readonly IRepository<Tag> _tagRepo;
        private readonly IMapper _mapper;

        public ApplicationCatalogService(IRepository<ApplicationCatalog> repo, IMapper mapper, IRepository<Genre> genreRepo, IRepository<Tag> tagRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _genreRepo = genreRepo;
            _tagRepo = tagRepo;
        }

        public async Task<ApplicationCatalogReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id,
                q => q.Include(a => a.Genres),
                q => q.Include(a => a.Tags),
                q => q.Include(a => a.Media),
                q => q.Include(a => a.SystemRequirements));
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllAsync(
        int pageNumber, int pageSize, string? searchTerm, int? genreId, int? tagId)
        {
            var query = _repo.GetQuery(asNoTracking: true);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                    a.Name.Contains(searchTerm) ||
                    a.Developer.Contains(searchTerm) ||
                    a.Publisher.Contains(searchTerm));
            }

            // Apply genre filter
            if (genreId.HasValue)
            {
                query = query.Where(a => a.Genres.Any(g => g.Id == genreId.Value));
            }

            // Apply tag filter
            if (tagId.HasValue)
            {
                query = query.Where(a => a.Tags.Any(t => t.Id == tagId.Value));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<ApplicationCatalogListItemDto>
            {
                Data = _mapper.Map<List<ApplicationCatalogListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ApplicationCatalogReturnDto> CreateAsync(ApplicationCatalogCreateDto dto)
        {
            var entity = _mapper.Map<ApplicationCatalog>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        // Steam.Application/Services/Catalog/Implementations/ApplicationCatalogService.cs (içində)

        public async Task<ApplicationCatalogReturnDto> UpdateAsync(int id, ApplicationCatalogUpdateDto dto)
        {
            // 1. Varlığı əlaqəli məlumatları ilə birlikdə gətiririk
            var entity = await _repo.GetEntityAsync(
                predicate: a => a.Id == id,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
            q => q.Include(a => a.Genres),
            q => q.Include(a => a.Tags)
                }
            );

            if (entity == null)
            {
                throw new NotFoundException(nameof(ApplicationCatalog), id);
            }

            // 2. Əsas məlumatları mapləyirik (GenreIds və TagIds xaric)
            _mapper.Map(dto, entity);

            // 3. Janrları yeniləyirik
            if (dto.GenreIds != null)
            {
                entity.Genres.Clear(); // Mövcud janrları təmizlə
                var genres = await _genreRepo.GetAllAsync(g => dto.GenreIds.Contains(g.Id));
                foreach (var genre in genres)
                {
                    entity.Genres.Add(genre);
                }
            }

            // 4. Teqləri yeniləyirik
            if (dto.TagIds != null)
            {
                entity.Tags.Clear(); // Mövcud teqləri təmizlə
                var tags = await _tagRepo.GetAllAsync(t => dto.TagIds.Contains(t.Id));
                foreach (var tag in tags)
                {
                    entity.Tags.Add(tag);
                }
            }

            // 5. Dəyişiklikləri yadda saxlayırıq
            await _repo.UpdateAsync(entity);
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }

        // Steam.Application/Services/Catalog/Implementations/ApplicationCatalogService.cs (içində)

        public async Task<List<GenreListItemDto>> GetGenresByApplicationIdAsync(int applicationId)
        {
            var application = await _repo.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
            q => q.Include(a => a.Genres) // Yalnız janrları include edirik
                },
                asNoTracking: true // Məlumatı yalnız oxumaq üçün istifadə etdiyimizdən performansı artırır
            );

            if (application == null)
            {
                throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            }

            return _mapper.Map<List<GenreListItemDto>>(application.Genres);
        }

        public async Task<List<TagListItemDto>> GetTagsByApplicationIdAsync(int applicationId)
        {
            var application = await _repo.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                q => q.Include(a => a.Tags)
                },
                asNoTracking: true
            );

            if (application == null)
            {
                throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            }

            return _mapper.Map<List<TagListItemDto>>(application.Tags);
        }

        // YENİ METOD: Media fayllarını gətirmək üçün
        public async Task<List<MediaListItemDto>> GetMediaByApplicationIdAsync(int applicationId)
        {
            var application = await _repo.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                q => q.Include(a => a.Media)
                },
                asNoTracking: true
            );

            if (application == null)
            {
                throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            }

            return _mapper.Map<List<MediaListItemDto>>(application.Media.OrderBy(m => m.Order));
        }

        // YENİ METOD: Sistem tələblərini gətirmək üçün
        public async Task<List<SystemRequirementsListItemDto>> GetSystemRequirementsByApplicationIdAsync(int applicationId)
        {
            var application = await _repo.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                q => q.Include(a => a.SystemRequirements)
                },
                asNoTracking: true
            );

            if (application == null)
            {
                throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            }

            return _mapper.Map<List<SystemRequirementsListItemDto>>(application.SystemRequirements);
        }
    }
}
