using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Achievements.CraftingRecipe
{
    public record CraftingRecipeListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}
