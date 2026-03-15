using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _dbSet.Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .OrderBy(recipe => recipe.CreatedAt);
        }

        public Recipe GetFullRecipe(Guid ID)
        {
            return _dbSet.Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                        .FirstOrDefault(rec => rec.Id == ID);
        }
    }
}
