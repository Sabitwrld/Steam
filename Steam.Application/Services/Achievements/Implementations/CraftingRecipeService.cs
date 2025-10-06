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
        private readonly IRepository<CraftingRecipe> _repository;
        // The IRepository<CraftingRecipeRequirement> is REMOVED
        private readonly IMapper _mapper;

        public CraftingRecipeService(
            IRepository<CraftingRecipe> repository,
            IMapper mapper) // Dependency REMOVED from constructor
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CraftingRecipeReturnDto> CreateCraftingRecipeAsync(CraftingRecipeCreateDto dto)
        {
            var entity = _mapper.Map<CraftingRecipe>(dto);

            // The requirements will be added to the collection, but the join entity itself won't be created here.
            entity.Requirements = dto.RequiredBadgeIds.Select(badgeId => new CraftingRecipeRequirement
            {
                RequiredBadgeId = badgeId
            }).ToList();

            await _repository.CreateAsync(entity);
            return await GetCraftingRecipeByIdAsync(entity.Id);
        }

        public async Task<CraftingRecipeReturnDto> UpdateCraftingRecipeAsync(int id, CraftingRecipeUpdateDto dto)
        {
            var entity = await _repository.GetEntityAsync(
                predicate: cr => cr.Id == id,
                includes: new Func<IQueryable<CraftingRecipe>, IQueryable<CraftingRecipe>>[] { q => q.Include(cr => cr.Requirements) }
            );

            if (entity == null)
                throw new NotFoundException(nameof(CraftingRecipe), id);

            // Update main properties
            _mapper.Map(dto, entity);

            // Let EF Core handle the join table updates
            // It will compare the new list with the old list and apply changes (delete, insert)
            entity.Requirements.Clear();
            foreach (var badgeId in dto.RequiredBadgeIds)
            {
                entity.Requirements.Add(new CraftingRecipeRequirement
                {
                    RequiredBadgeId = badgeId
                });
            }

            await _repository.UpdateAsync(entity);
            return await GetCraftingRecipeByIdAsync(id);
        }

        // --- Other methods remain the same ---

        public async Task<bool> DeleteCraftingRecipeAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            return await _repository.DeleteAsync(entity);
        }

        public async Task<CraftingRecipeReturnDto> GetCraftingRecipeByIdAsync(int id)
        {
            var entity = await _repository.GetEntityAsync(
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
            var query = _repository.GetQuery(asNoTracking: true).Include(cr => cr.ResultBadge);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

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
