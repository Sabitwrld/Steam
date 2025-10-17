using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class RegionalPriceService : IRegionalPriceService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public RegionalPriceService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegionalPriceReturnDto> CreateRegionalPriceAsync(RegionalPriceCreateDto dto)
        {
            var entity = _mapper.Map<RegionalPrice>(dto);
            await _unitOfWork.RegionalPriceRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<RegionalPriceReturnDto> UpdateRegionalPriceAsync(int id, RegionalPriceUpdateDto dto)
        {
            var entity = await _unitOfWork.RegionalPriceRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(RegionalPrice), id);

            _mapper.Map(dto, entity);
            _unitOfWork.RegionalPriceRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<bool> DeleteRegionalPriceAsync(int id)
        {
            var entity = await _unitOfWork.RegionalPriceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.RegionalPriceRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<RegionalPriceReturnDto> GetRegionalPriceByIdAsync(int id)
        {
            var entity = await _unitOfWork.RegionalPriceRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(RegionalPrice), id);

            return _mapper.Map<RegionalPriceReturnDto>(entity);
        }

        public async Task<PagedResponse<RegionalPriceListItemDto>> GetAllRegionalPricesAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.RegionalPriceRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<RegionalPriceListItemDto>
            {
                Data = _mapper.Map<List<RegionalPriceListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
