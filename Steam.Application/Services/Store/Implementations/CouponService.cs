using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Coupon;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<CouponReturnDto>(created);
        }

        public async Task<CouponReturnDto> UpdateCouponAsync(int id, CouponUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Coupon with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<CouponReturnDto>(updated);
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
                throw new KeyNotFoundException($"Coupon with Id {id} not found.");

            return _mapper.Map<CouponReturnDto>(entity);
        }

        public async Task<PagedResponse<CouponListItemDto>> GetAllCouponsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<CouponListItemDto>>(items);

            return new PagedResponse<CouponListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
