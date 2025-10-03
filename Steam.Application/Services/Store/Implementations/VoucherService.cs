using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Voucher;
using Steam.Application.Exceptions;
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
    public class VoucherService : IVoucherService
    {
        private readonly IRepository<Voucher> _repository;
        private readonly IMapper _mapper;

        public VoucherService(IRepository<Voucher> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VoucherReturnDto> CreateVoucherAsync(VoucherCreateDto dto)
        {
            var entity = _mapper.Map<Voucher>(dto);
            await _repository.CreateAsync(entity);
            return _mapper.Map<VoucherReturnDto>(entity);
        }

        public async Task<VoucherReturnDto> RedeemVoucherAsync(string code, int userId)
        {
            var voucher = await _repository.GetEntityAsync(v => v.Code == code);

            if (voucher == null)
                throw new NotFoundException("Voucher with this code does not exist.");

            if (voucher.IsUsed)
                throw new Exception("This voucher has already been used.");

            if (voucher.ExpirationDate < DateTime.UtcNow)
                throw new Exception("This voucher has expired.");

            voucher.IsUsed = true;
            await _repository.UpdateAsync(voucher);

            // TODO: Add logic here to add the game (voucher.ApplicationId) to the user's (userId) library

            return _mapper.Map<VoucherReturnDto>(voucher);
        }

        public async Task<VoucherReturnDto> GetVoucherByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Voucher), id);

            return _mapper.Map<VoucherReturnDto>(entity);
        }

        public async Task<PagedResponse<VoucherListItemDto>> GetAllVouchersAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResponse<VoucherListItemDto>
            {
                Data = _mapper.Map<List<VoucherListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
