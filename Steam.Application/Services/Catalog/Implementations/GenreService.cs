using AutoMapper;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
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
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public GenreService(IRepository<Genre> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GenreReturnDto> CreateGenreAsync(GenreCreateDto dto)
        {
            var entity = _mapper.Map<Genre>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<GenreReturnDto>(created);
        }

        public async Task<GenreReturnDto> UpdateGenreAsync(int id, GenreUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Genre with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<GenreReturnDto>(updated);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<GenreReturnDto> GetGenreByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Genre with Id {id} not found.");

            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<PagedResponse<GenreListItemDto>> GetAllGenreAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<GenreListItemDto>>(items);

            return new PagedResponse<GenreListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };

        }
    }
}
