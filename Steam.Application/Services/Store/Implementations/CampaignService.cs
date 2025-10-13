using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Campaign;
using Steam.Application.Exceptions;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Store.Implementations
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public CampaignService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CampaignReturnDto> CreateCampaignAsync(CampaignCreateDto dto)
        {
            var entity = _mapper.Map<Campaign>(dto);
            await _unitOfWork.CampaignRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CampaignReturnDto>(entity);
        }

        public async Task<CampaignReturnDto> UpdateCampaignAsync(int id, CampaignUpdateDto dto)
        {
            var entity = await _unitOfWork.CampaignRepository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException(nameof(Campaign), id);

            _mapper.Map(dto, entity);
            _unitOfWork.CampaignRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CampaignReturnDto>(entity);
        }

        public async Task<bool> DeleteCampaignAsync(int id)
        {
            var entity = await _unitOfWork.CampaignRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _unitOfWork.CampaignRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<CampaignReturnDto> GetCampaignByIdAsync(int id)
        {
            var entity = await _unitOfWork.CampaignRepository.GetEntityAsync(
                predicate: c => c.Id == id,
                includes: new Func<IQueryable<Campaign>, IQueryable<Campaign>>[] { q => q.Include(c => c.Discounts) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(Campaign), id);

            return _mapper.Map<CampaignReturnDto>(entity);
        }

        public async Task<PagedResponse<CampaignListItemDto>> GetAllCampaignsAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.CampaignRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<CampaignListItemDto>
            {
                Data = _mapper.Map<List<CampaignListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
