using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
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

        // --- IMPLEMENTATION FOR MISSING METHODS ---

        public async Task<UserLibraryReturnDto> CreateUserLibraryAsync(UserLibraryCreateDto dto)
        {
            var existingLibrary = await _repository.IsExistsAsync(ul => ul.UserId == dto.UserId);
            if (existingLibrary)
            {
                throw new Exception($"A library for user with ID {dto.UserId} already exists.");
            }

            var entity = _mapper.Map<UserLibrary>(dto);
            await _repository.CreateAsync(entity);
            return _mapper.Map<UserLibraryReturnDto>(entity);
        }

        public async Task<UserLibraryReturnDto> UpdateUserLibraryAsync(int id, UserLibraryUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(UserLibrary), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return await GetUserLibraryByIdAsync(id); // Return the full updated object
        }

        public async Task<bool> DeleteUserLibraryAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        // --- EXISTING METHODS (WITH CORRECTIONS) ---

        // --- FIX: This method now correctly accepts a string UserId ---
        public async Task<UserLibraryReturnDto> GetUserLibraryByUserIdAsync(string userId)
        {
            // First, ensure the UserLibrary entity also uses string for UserId
            var library = await _repository.GetEntityAsync(
                predicate: ul => ul.UserId == userId,
                includes: new[] {
                    (System.Func<IQueryable<UserLibrary>, IQueryable<UserLibrary>>)(q => q.Include(ul => ul.Licenses)
                                                                                         .ThenInclude(l => l.Application))
                }
            );

            if (library == null)
            {
                var newLibrary = new UserLibrary { UserId = userId };
                await _repository.CreateAsync(newLibrary);
                return _mapper.Map<UserLibraryReturnDto>(newLibrary);
            }

            return _mapper.Map<UserLibraryReturnDto>(library);
        }

        public async Task<UserLibraryReturnDto> GetUserLibraryByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
                predicate: ul => ul.Id == id,
                includes: new[] {
                    (Func<IQueryable<UserLibrary>, IQueryable<UserLibrary>>)(q => q.Include(ul => ul.Licenses)
                                                                                  .ThenInclude(l => l.Application))
                }
            );

            if (entity == null)
                throw new NotFoundException(nameof(UserLibrary), id);

            return _mapper.Map<UserLibraryReturnDto>(entity);
        }

        public async Task<PagedResponse<UserLibraryListItemDto>> GetAllUserLibrariesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true)
                                   .Include(ul => ul.Licenses);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<UserLibraryListItemDto>
            {
                Data = _mapper.Map<List<UserLibraryListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
