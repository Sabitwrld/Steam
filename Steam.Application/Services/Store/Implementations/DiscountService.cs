using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Discount;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public DiscountService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DiscountReturnDto> CreateDiscountAsync(DiscountCreateDto dto)
        {
            var entity = _mapper.Map<Discount>(dto);
            await _unitOfWork.DiscountRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DiscountReturnDto>(entity);
        }

        public async Task<DiscountReturnDto> UpdateDiscountAsync(int id, DiscountUpdateDto dto)
        {
            var entity = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Discount), id);

            _mapper.Map(dto, entity);
            _unitOfWork.DiscountRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DiscountReturnDto>(entity);
        }

        public async Task<bool> DeleteDiscountAsync(int id)
        {
            var entity = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.DiscountRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<DiscountReturnDto> GetDiscountByIdAsync(int id)
        {
            var entity = await _unitOfWork.DiscountRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Discount), id);

            return _mapper.Map<DiscountReturnDto>(entity);
        }

        public async Task<PagedResponse<DiscountListItemDto>> GetAllDiscountsAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.DiscountRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<DiscountListItemDto>
            {
                Data = _mapper.Map<List<DiscountListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
