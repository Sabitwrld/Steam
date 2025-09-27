using AutoMapper;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class MediaService : IMediaService
    {
        private readonly IRepository<Media> _repository;
        private readonly IMapper _mapper;

        public MediaService(IRepository<Media> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MediaReturnDto> CreateMediaAsync(MediaCreateDto dto)
        {
            var entity = _mapper.Map<Media>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<MediaReturnDto>(created);
        }

        public async Task<MediaReturnDto> UpdateMediaAsync(int id, MediaUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Media with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<MediaReturnDto>(updated);
        }

        public async Task<bool> DeleteMediaAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<MediaReturnDto> GetMediaByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Media with Id {id} not found.");

            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<PagedResponse<MediaListItemDto>> GetAllMediaAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<MediaListItemDto>>(items);

            return new PagedResponse<MediaListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }

    }
}
