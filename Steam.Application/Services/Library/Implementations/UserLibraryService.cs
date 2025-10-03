using AutoMapper;
using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Library.Interfaces;
using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Library.Implementations
{
    public class UserLibraryService : IUserLibraryService
    {
        private readonly IRepository<UserLibrary> _repository;
        private readonly IMapper _mapper;

        public UserLibraryService(IRepository<UserLibrary> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserLibraryReturnDto> CreateUserLibraryAsync(UserLibraryCreateDto dto)
        {
            var entity = _mapper.Map<UserLibrary>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<UserLibraryReturnDto>(created);
        }

        public async Task<UserLibraryReturnDto> UpdateUserLibraryAsync(int id, UserLibraryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"UserLibrary with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<UserLibraryReturnDto>(updated);
        }

        public async Task<bool> DeleteUserLibraryAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<UserLibraryReturnDto> GetUserLibraryByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"UserLibrary with Id {id} not found.");

            return _mapper.Map<UserLibraryReturnDto>(entity);
        }

        public async Task<PagedResponse<UserLibraryListItemDto>> GetAllUserLibrariesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<UserLibraryListItemDto>>(items);

            return new PagedResponse<UserLibraryListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
