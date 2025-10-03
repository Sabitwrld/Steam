using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Gift;
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
    public class GiftService : IGiftService
    {
        private readonly IRepository<Gift> _repository;
        private readonly IMapper _mapper;

        public GiftService(IRepository<Gift> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GiftReturnDto> SendGiftAsync(GiftCreateDto dto)
        {
            var entity = _mapper.Map<Gift>(dto);
            entity.SentAt = System.DateTime.UtcNow;
            await _repository.CreateAsync(entity);
            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<bool> RedeemGiftAsync(int giftId, int receiverId)
        {
            var gift = await _repository.GetEntityAsync(g => g.Id == giftId && g.ReceiverId == receiverId);
            if (gift == null)
                throw new NotFoundException("Gift not found or does not belong to this user.");

            if (gift.IsRedeemed)
                throw new System.Exception("This gift has already been redeemed.");

            gift.IsRedeemed = true;
            await _repository.UpdateAsync(gift);

            // TODO: Add logic here to add the game to the user's library

            return true;
        }

        public async Task<GiftReturnDto> GetGiftByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Gift), id);

            return _mapper.Map<GiftReturnDto>(entity);
        }

        public async Task<PagedResponse<GiftListItemDto>> GetGiftsForUserAsync(int userId, int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(g => g.ReceiverId == userId, asNoTracking: true);
            var totalCount = await query.CountAsync();
            var items = await query.OrderByDescending(g => g.SentAt)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            return new PagedResponse<GiftListItemDto>
            {
                Data = _mapper.Map<List<GiftListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
