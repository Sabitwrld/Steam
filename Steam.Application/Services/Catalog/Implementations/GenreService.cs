using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Catalog.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreReturnDto> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.GenreRepository.GetByIdAsync(id, q => q.Include(g => g.Applications));
            if (entity == null) throw new NotFoundException(nameof(Genre), id);
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<PagedResponse<GenreListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            // Sorğu məntiqi Repository-yə daşındı
            var (items, totalCount) = await _unitOfWork.GenreRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<GenreListItemDto>
            {
                Data = _mapper.Map<List<GenreListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<GenreReturnDto> CreateAsync(GenreCreateDto dto)
        {
            var entity = _mapper.Map<Genre>(dto);
            await _unitOfWork.GenreRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<GenreReturnDto> UpdateAsync(int id, GenreUpdateDto dto)
        {
            var entity = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException(nameof(Genre), id);

            _mapper.Map(dto, entity);
            _unitOfWork.GenreRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.GenreRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
