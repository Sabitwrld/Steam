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
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public UserLibraryService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserLibraryReturnDto> CreateUserLibraryAsync(UserLibraryCreateDto dto)
        {
            var existingLibrary = await _unitOfWork.UserLibraryRepository.IsExistsAsync(ul => ul.UserId == dto.UserId);
            if (existingLibrary)
            {
                throw new Exception($"A library for user with ID {dto.UserId} already exists.");
            }

            var entity = _mapper.Map<UserLibrary>(dto);
            await _unitOfWork.UserLibraryRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserLibraryReturnDto>(entity);
        }

        public async Task<UserLibraryReturnDto> UpdateUserLibraryAsync(int id, UserLibraryUpdateDto dto)
        {
            var entity = await _unitOfWork.UserLibraryRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(UserLibrary), id);

            _mapper.Map(dto, entity);
            _unitOfWork.UserLibraryRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return await GetUserLibraryByIdAsync(id);
        }

        public async Task<bool> DeleteUserLibraryAsync(int id)
        {
            var entity = await _unitOfWork.UserLibraryRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.UserLibraryRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<UserLibraryReturnDto> GetUserLibraryByUserIdAsync(string userId)
        {
            var library = await _unitOfWork.UserLibraryRepository.GetEntityAsync(
                predicate: ul => ul.UserId == userId,
                includes: new[] {
                    (Func<IQueryable<UserLibrary>, IQueryable<UserLibrary>>)(q => q.Include(ul => ul.Licenses)
                                                                                  .ThenInclude(l => l.Application))
                }
            );

            if (library == null)
            {
                var newLibrary = new UserLibrary { UserId = userId };
                await _unitOfWork.UserLibraryRepository.CreateAsync(newLibrary);
                await _unitOfWork.CommitAsync(); // Yeni kitabxana dərhal yaradılmalıdır
                return _mapper.Map<UserLibraryReturnDto>(newLibrary);
            }

            return _mapper.Map<UserLibraryReturnDto>(library);
        }

        public async Task<UserLibraryReturnDto> GetUserLibraryByIdAsync(int id)
        {
            var entity = await _unitOfWork.UserLibraryRepository.GetEntityAsync(
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
            var query = _unitOfWork.UserLibraryRepository.GetQuery(asNoTracking: true)
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
