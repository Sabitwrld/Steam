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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public ApplicationCatalogService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApplicationCatalogReturnDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ApplicationCatalogRepository.GetByIdAsync(id,
                q => q.Include(a => a.Genres),
                q => q.Include(a => a.Tags),
                q => q.Include(a => a.Media),
                q => q.Include(a => a.SystemRequirements));

            if (entity == null)
                throw new NotFoundException(nameof(ApplicationCatalog), id);

            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm, int? genreId, int? tagId)
        {
            var query = _unitOfWork.ApplicationCatalogRepository.GetQuery(asNoTracking: true);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                    a.Name.Contains(searchTerm) ||
                    a.Developer.Contains(searchTerm) ||
                    a.Publisher.Contains(searchTerm));
            }

            if (genreId.HasValue)
            {
                query = query.Where(a => a.Genres.Any(g => g.Id == genreId.Value));
            }

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
            await _unitOfWork.ApplicationCatalogRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<ApplicationCatalogReturnDto> UpdateAsync(int id, ApplicationCatalogUpdateDto dto)
        {
            var entity = await _unitOfWork.ApplicationCatalogRepository.GetEntityAsync(
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

            _mapper.Map(dto, entity);

            if (dto.GenreIds != null)
            {
                entity.Genres.Clear();
                var genres = await _unitOfWork.GenreRepository.GetAllAsync(g => dto.GenreIds.Contains(g.Id));
                foreach (var genre in genres)
                {
                    entity.Genres.Add(genre);
                }
            }

            if (dto.TagIds != null)
            {
                entity.Tags.Clear();
                var tags = await _unitOfWork.TagRepository.GetAllAsync(t => dto.TagIds.Contains(t.Id));
                foreach (var tag in tags)
                {
                    entity.Tags.Add(tag);
                }
            }

            _unitOfWork.ApplicationCatalogRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ApplicationCatalogReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.ApplicationCatalogRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.ApplicationCatalogRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<List<GenreListItemDto>> GetGenresByApplicationIdAsync(int applicationId)
        {
            var application = await _unitOfWork.ApplicationCatalogRepository.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                   q => q.Include(a => a.Genres)
                },
                asNoTracking: true
            );

            if (application == null) throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            return _mapper.Map<List<GenreListItemDto>>(application.Genres);
        }

        public async Task<List<TagListItemDto>> GetTagsByApplicationIdAsync(int applicationId)
        {
            var application = await _unitOfWork.ApplicationCatalogRepository.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                   q => q.Include(a => a.Tags)
                },
                asNoTracking: true
            );

            if (application == null) throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            return _mapper.Map<List<TagListItemDto>>(application.Tags);
        }

        public async Task<List<MediaListItemDto>> GetMediaByApplicationIdAsync(int applicationId)
        {
            var application = await _unitOfWork.ApplicationCatalogRepository.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                   q => q.Include(a => a.Media)
                },
                asNoTracking: true
            );

            if (application == null) throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            return _mapper.Map<List<MediaListItemDto>>(application.Media.OrderBy(m => m.Order));
        }

        public async Task<List<SystemRequirementsListItemDto>> GetSystemRequirementsByApplicationIdAsync(int applicationId)
        {
            var application = await _unitOfWork.ApplicationCatalogRepository.GetEntityAsync(
                predicate: a => a.Id == applicationId,
                includes: new Func<IQueryable<ApplicationCatalog>, IQueryable<ApplicationCatalog>>[]
                {
                    q => q.Include(a => a.SystemRequirements)
                },
                asNoTracking: true
            );

            if (application == null) throw new NotFoundException(nameof(ApplicationCatalog), applicationId);
            return _mapper.Map<List<SystemRequirementsListItemDto>>(application.SystemRequirements);
        }
    }
}
