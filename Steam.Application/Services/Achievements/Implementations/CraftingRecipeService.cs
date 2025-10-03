using AutoMapper;
using Steam.Application.DTOs.Achievements.CraftingRecipe;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.Application.Services.Achievements.Implementations
{
    public class CraftingRecipeService : ICraftingRecipeService
    {
        private readonly IRepository<CraftingRecipe> _repository;
        private readonly IMapper _mapper;

        public CraftingRecipeService(IRepository<CraftingRecipe> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CraftingRecipeReturnDto> CreateCraftingRecipeAsync(CraftingRecipeCreateDto dto)
        {
            var entity = _mapper.Map<CraftingRecipe>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<CraftingRecipeReturnDto>(created);
        }

        public async Task<CraftingRecipeReturnDto> UpdateCraftingRecipeAsync(int id, CraftingRecipeUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"CraftingRecipe {id} not found");

            _mapper.Map(dto, entity);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<CraftingRecipeReturnDto>(updated);
        }

        public async Task<bool> DeleteCraftingRecipeAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            return await _repository.DeleteAsync(entity);
        }

        public async Task<CraftingRecipeReturnDto> GetCraftingRecipeByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException($"CraftingRecipe {id} not found");

            return _mapper.Map<CraftingRecipeReturnDto>(entity);
        }

        public async Task<PagedResponse<CraftingRecipeListItemDto>> GetAllCraftingRecipesAsync(int pageNumber, int pageSize)
        {
            var query = _repository.GetQuery(asNoTracking: true);
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var mapped = _mapper.Map<List<CraftingRecipeListItemDto>>(items);

            return new PagedResponse<CraftingRecipeListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mapped
            };
        }
    }
}
