using AutoMapper;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Campaign;
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
    public class CampaignService : ICampaignService
    {
        private readonly IRepository<Campaign> _repository;
        private readonly IMapper _mapper;

        public CampaignService(IRepository<Campaign> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CampaignReturnDto> CreateCampaignAsync(CampaignCreateDto dto)
        {
            var entity = _mapper.Map<Campaign>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<CampaignReturnDto>(created);
        }

        public async Task<CampaignReturnDto> UpdateCampaignAsync(int id, CampaignUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Campaign with Id {id} not found.");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<CampaignReturnDto>(updated);
        }

        public async Task<bool> DeleteCampaignAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<CampaignReturnDto> GetCampaignByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Campaign with Id {id} not found.");

            return _mapper.Map<CampaignReturnDto>(entity);
        }

        public async Task<PagedResponse<CampaignListItemDto>> GetAllCampaignsAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);

            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            var mappedItems = _mapper.Map<List<CampaignListItemDto>>(items);

            return new PagedResponse<CampaignListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mappedItems
            };
        }
    }
}
