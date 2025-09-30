using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Genre> _repo;
        private readonly IMapper _mapper;

        public GenreService(IRepository<Genre> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<GenreReturnDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id, q => q.Include(g => g.Applications));
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<PagedResponse<GenreListItemDto>> GetAllAsync(int pageNumber, int pageSize)
        {
            var entities = await _repo.GetAllAsync(skip: (pageNumber - 1) * pageSize, take: pageSize);
            return new PagedResponse<GenreListItemDto>
            {
                Data = _mapper.Map<List<GenreListItemDto>>(entities),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = await _repo.GetQuery().CountAsync()
            };
        }

        public async Task<GenreReturnDto> CreateAsync(GenreCreateDto dto)
        {
            var entity = _mapper.Map<Genre>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<GenreReturnDto> UpdateAsync(int id, GenreUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new Exception("Genre not found");

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            return _mapper.Map<GenreReturnDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repo.DeleteAsync(entity);
        }
    }
}
