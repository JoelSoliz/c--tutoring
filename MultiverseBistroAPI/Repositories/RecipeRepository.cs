using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Models;
using System.Collections.Generic;

namespace MultiverseBistroAPI.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }

        public IEnumerable<Recipe> GetAllRecipes(int limit, int page)
        {
            return _dbSet.Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Creator)
                .AsNoTracking()
                .OrderBy(recipe => recipe.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit);
        }

        public Recipe GetFullRecipe(Guid ID)
        {
            return _dbSet.Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                        .Include(r => r.Creator)
                        .AsNoTracking()
                        .FirstOrDefault(rec => rec.Id == ID);
        }
    }
}
