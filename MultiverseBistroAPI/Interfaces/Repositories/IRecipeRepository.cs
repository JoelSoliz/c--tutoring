using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Interfaces.Repositories
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        public IEnumerable<Recipe> GetAllRecipes(int limit, int page);
        public Recipe GetFullRecipe(Guid ID);
    }
}
