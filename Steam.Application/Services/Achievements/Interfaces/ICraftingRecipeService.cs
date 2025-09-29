using Steam.Application.DTOs.Achievements.CraftingRecipe;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface ICraftingRecipeService
    {
        Task<CraftingRecipeReturnDto> CreateCraftingRecipeAsync(CraftingRecipeCreateDto dto);
        Task<CraftingRecipeReturnDto> UpdateCraftingRecipeAsync(int id, CraftingRecipeUpdateDto dto);
        Task<bool> DeleteCraftingRecipeAsync(int id);
        Task<CraftingRecipeReturnDto> GetCraftingRecipeByIdAsync(int id);
        Task<PagedResponse<CraftingRecipeListItemDto>> GetAllCraftingRecipesAsync(int pageNumber, int pageSize);
    }
}
