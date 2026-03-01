using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Services;
using MultiverseBistroAPI.Models;
using System.Linq;

namespace MultiverseBistroAPI.Interfaces
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDBContext _dbContext;

        public RecipeService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RecipeResponseDTO CreateRecipe(RecipeCreateDTO recipeCreateDTO)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = recipeCreateDTO.Title,
                Category = recipeCreateDTO.Category,
                CreatedAt = DateTime.UtcNow,
                Ingredients = recipeCreateDTO.Ingredients,
                Instructions = recipeCreateDTO.Instructions
            };

            _dbContext.Recipes.Add(newRecipe);
            return new RecipeResponseDTO
            {
                Id = newRecipe.Id,
                Title = newRecipe.Title,
                Category = newRecipe.Category,
                CreatedAt = newRecipe.CreatedAt,
                Ingredients = newRecipe.Ingredients,
                Instructions = newRecipe.Instructions,
            };
        }

        public bool DeleteRecipe(Guid ID)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(rec => rec.Id == ID);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe {ID} not found");
            }

            _dbContext.Recipes.Remove(recipe);
            return true;
        }

        public RecipeResponseDTO GetRecipe(Guid ID)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(rec => rec.Id == ID);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe {ID} not found");
            }

            return new RecipeResponseDTO
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Category = recipe.Category,
                CreatedAt = recipe.CreatedAt,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions,
            };
        }

        public RecipePaginatedResponseDTO GetRecipes(int limit, int page)
        {
            var recipes = _dbContext.Recipes.OrderBy(recipe => recipe.CreatedAt);
            //var totalPages = Math.Ceiling((double)recipes.Count() / limit);
            
            var response = recipes.Skip((page - 1) * limit)
                .Take(limit)
                .Select(recipe => new RecipeResponseDTO
                 {
                     Id = recipe.Id,
                     Title = recipe.Title,
                     Category = recipe.Category,
                     CreatedAt = recipe.CreatedAt,
                     Ingredients = recipe.Ingredients,
                     Instructions = recipe.Instructions,
                 });
            return new RecipePaginatedResponseDTO
            {
                Data = response,
                Limit = limit,
                Page = page,
                TotalCount = recipes.Count()
            };
        }
    }
}
