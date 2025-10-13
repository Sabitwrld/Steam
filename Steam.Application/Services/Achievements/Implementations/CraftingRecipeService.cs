using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Achievements.CraftingRecipe;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Exceptions;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Achievements.Implementations
{
    public class CraftingRecipeService : ICraftingRecipeService
    {
        private readonly IUnitOfWork _unitOfWork; // Dəyişdirildi
        private readonly IMapper _mapper;

        public CraftingRecipeService(IUnitOfWork unitOfWork, IMapper mapper) // Dəyişdirildi
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CraftingRecipeReturnDto> CreateCraftingRecipeAsync(CraftingRecipeCreateDto dto)
        {
            var entity = _mapper.Map<CraftingRecipe>(dto);

            entity.Requirements = dto.RequiredBadgeIds.Select(badgeId => new CraftingRecipeRequirement
            {
                RequiredBadgeId = badgeId
            }).ToList();

            await _unitOfWork.CraftingRecipeRepository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return await GetCraftingRecipeByIdAsync(entity.Id);
        }

        public async Task<CraftingRecipeReturnDto> UpdateCraftingRecipeAsync(int id, CraftingRecipeUpdateDto dto)
        {
            var entity = await _unitOfWork.CraftingRecipeRepository.GetEntityAsync(
                predicate: cr => cr.Id == id,
                includes: new Func<IQueryable<CraftingRecipe>, IQueryable<CraftingRecipe>>[] { q => q.Include(cr => cr.Requirements) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(CraftingRecipe), id);

            _mapper.Map(dto, entity);

            entity.Requirements.Clear();
            foreach (var badgeId in dto.RequiredBadgeIds)
            {
                entity.Requirements.Add(new CraftingRecipeRequirement
                {
                    RequiredBadgeId = badgeId
                });
            }

            _unitOfWork.CraftingRecipeRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return await GetCraftingRecipeByIdAsync(id);
        }

        public async Task<bool> DeleteCraftingRecipeAsync(int id)
        {
            var entity = await _unitOfWork.CraftingRecipeRepository.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.CraftingRecipeRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<CraftingRecipeReturnDto> GetCraftingRecipeByIdAsync(int id)
        {
            var entity = await _unitOfWork.CraftingRecipeRepository.GetEntityAsync(
                predicate: cr => cr.Id == id,
                includes: new Func<IQueryable<CraftingRecipe>, IQueryable<CraftingRecipe>>[]
                {
                    q => q.Include(cr => cr.ResultBadge),
                    q => q.Include(cr => cr.Requirements)
                         .ThenInclude(r => r.RequiredBadge)
                }
            );

            if (entity == null)
                throw new NotFoundException(nameof(CraftingRecipe), id);

            return _mapper.Map<CraftingRecipeReturnDto>(entity);
        }

        public async Task<PagedResponse<CraftingRecipeListItemDto>> GetAllCraftingRecipesAsync(int pageNumber, int pageSize)
        {
            var (items, totalCount) = await _unitOfWork.CraftingRecipeRepository.GetAllPagedAsync(pageNumber, pageSize);

            return new PagedResponse<CraftingRecipeListItemDto>
            {
                Data = _mapper.Map<List<CraftingRecipeListItemDto>>(items),
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
