using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Coupon;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public CouponService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CouponReturnDto> CreateCouponAsync(CouponCreateDto dto)
        {
            var entity = _mapper.Map<Coupon>(dto);
            await _unitOfWork.CouponRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<CouponReturnDto> UpdateCouponAsync(int id, CouponUpdateDto dto)
        {
            var entity = await _unitOfWork.CouponRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Coupon), id);

            _mapper.Map(dto, entity);
            _unitOfWork.CouponRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var entity = await _unitOfWork.CouponRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.CouponRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<CouponReturnDto> GetCouponByIdAsync(int id)
        {
            var entity = await _unitOfWork.CouponRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Coupon), id);

            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<PagedResponse<CouponListItemDto>> GetAllCouponsAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.CouponRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<CouponListItemDto>
            {
                Data = _mapper.Map<List<CouponListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
