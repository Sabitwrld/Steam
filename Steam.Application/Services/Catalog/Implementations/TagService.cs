using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TagReturnDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.TagRepository.GetByIdAsync(id, q => q.Include(t => t.Applications));
            if (entity == null) throw new NotFoundException(nameof(Tag), id);
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<PagedResponse<TagListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _unitOfWork.TagRepository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var entities = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<TagListItemDto>
            {
                Data = _mapper.Map<List<TagListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<TagReturnDto> CreateAsync(TagCreateDto dto)
        {
            var entity = _mapper.Map<Tag>(dto);
            await _unitOfWork.TagRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<TagReturnDto> UpdateAsync(int id, TagUpdateDto dto)
        {
            var entity = await _unitOfWork.TagRepository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException(nameof(Tag), id);

            _mapper.Map(dto, entity);
            _unitOfWork.TagRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TagReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.TagRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.TagRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
