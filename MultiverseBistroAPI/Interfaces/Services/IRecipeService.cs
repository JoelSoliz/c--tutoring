using MultiverseBistroAPI.DTOs;

namespace MultiverseBistroAPI.Interfaces.Services
{
    public interface IRecipeService
    {
        public RecipePaginatedResponseDTO GetRecipes(int limit, int page);
        public RecipeResponseDTO GetRecipe(Guid ID);
        public RecipeResponseDTO CreateRecipe(RecipeCreateDTO recipeCreateDTO);
        public bool DeleteRecipe(Guid ID);
    }
}
