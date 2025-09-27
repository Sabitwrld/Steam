using AutoMapper;
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
        private readonly IRepository<Tag> _repository;
        private readonly IMapper _mapper;

        public TagService(IRepository<Tag> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TagReturnDto> CreateTagAsync(TagCreateDto dto)
        {
            var entity = _mapper.Map<Tag>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<TagReturnDto>(created);
        }

        public async Task<TagReturnDto> UpdateTagAsync(int id, TagUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Tag with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<TagReturnDto>(updated);
        }

        public async Task<bool> DeleteTagAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<TagReturnDto> GetTagByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Tag with Id {id} not found.");

            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<PagedResponse<TagListItemDto>> GetAllTagAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<TagListItemDto>>(items);

            return new PagedResponse<TagListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };

        }
    }
}
