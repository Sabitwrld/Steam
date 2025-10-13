using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Repositories.Interfaces.Achievements
{
    public interface ICraftingRecipeRepository : IRepository<CraftingRecipe>
    {
        Task<(IEnumerable<CraftingRecipe> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
