using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class PricePointService : IPricePointService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public PricePointService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PricePointReturnDto> CreatePricePointAsync(PricePointCreateDto dto)
        {
            var entity = _mapper.Map<PricePoint>(dto);
            await _unitOfWork.PricePointRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<PricePointReturnDto> UpdatePricePointAsync(int id, PricePointUpdateDto dto)
        {
            var entity = await _unitOfWork.PricePointRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(PricePoint), id);

            _mapper.Map(dto, entity);
            _unitOfWork.PricePointRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<bool> DeletePricePointAsync(int id)
        {
            var entity = await _unitOfWork.PricePointRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.PricePointRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<PricePointReturnDto> GetPricePointByIdAsync(int id)
        {
            var entity = await _unitOfWork.PricePointRepository.GetByIdWithRegionsAsync(id);

            if (entity == null)
                throw new NotFoundException(nameof(PricePoint), id);

            return _mapper.Map<PricePointReturnDto>(entity);
        }

        public async Task<PagedResponse<PricePointListItemDto>> GetAllPricePointsAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.PricePointRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<PricePointListItemDto>
            {
                Data = _mapper.Map<List<PricePointListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
