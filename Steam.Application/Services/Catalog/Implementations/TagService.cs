using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Tag;
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
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _repo;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TagReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id, q => q.Include(t => t.Applications));
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<PagedResponse<TagListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<TagListItemDto>
            {
                Data = _mapper.Map<List<TagListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        public async Task<TagReturnDto> CreateAsync(TagCreateDto dto)
        {
            var entity = _mapper.Map<Tag>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<TagReturnDto> UpdateAsync(int id, TagUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Tag not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }
    }
}
