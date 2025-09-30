using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Media> _repo;
        private readonly IMapper _mapper;

        public MediaService(IRepository<Media> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<MediaReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id, q => q.Include(m => m.Application));
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<PagedResponse<MediaListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<MediaListItemDto>
            {
                Data = _mapper.Map<List<MediaListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        public async Task<MediaReturnDto> CreateAsync(MediaCreateDto dto)
        {
            var entity = _mapper.Map<Media>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<MediaReturnDto> UpdateAsync(int id, MediaUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Media not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<MediaReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }
    }
}
