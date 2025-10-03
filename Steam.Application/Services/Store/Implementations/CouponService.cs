using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Coupon> _repository;
        private readonly IMapper _mapper;

        public CouponService(IRepository<Coupon> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CouponReturnDto> CreateCouponAsync(CouponCreateDto dto)
        {
            var entity = _mapper.Map<Coupon>(dto);
            await _repository.CreateAsync(entity);
            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<CouponReturnDto> UpdateCouponAsync(int id, CouponUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Coupon), id);

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<CouponReturnDto> GetCouponByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Coupon), id);

            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<PagedResponse<CouponListItemDto>> GetAllCouponsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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
