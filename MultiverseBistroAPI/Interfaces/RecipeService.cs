using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Services;
using MultiverseBistroAPI.Models;
using System.Linq;
using System.Threading.Tasks;

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
                Instructions = recipeCreateDTO.Instructions
            };
            foreach (var ingr in recipeCreateDTO.Ingredients)
            {
                var ingredient = _dbContext.Ingredients.FirstOrDefault(i => i.Name == ingr.Name);
                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        ID = Guid.NewGuid(),
                        Name = ingr.Name
                    };
                    _dbContext.Ingredients.Add(ingredient);
                }

                newRecipe.RecipeIngredients.Add(new RecipeIngredient()
                {
                    Recipe = newRecipe,
                    Ingredient = ingredient,
                    Quantity = ingr.Quantity,
                    Unit = ingr.Unit
                });
            }

            _dbContext.Recipes.Add(newRecipe);

            _dbContext.SaveChanges();

            return new RecipeResponseDTO
            {
                Id = newRecipe.Id,
                Title = newRecipe.Title,
                Category = newRecipe.Category,
                CreatedAt = newRecipe.CreatedAt,
                Ingredients = newRecipe.RecipeIngredients.Select(ri => new RecipeIngredientDTO()
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit
                }).ToList(),
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

            _dbContext.SaveChanges();
            
            return true;
        }

        public RecipeResponseDTO GetRecipe(Guid ID)
        {
            var recipe = _dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefault(rec => rec.Id == ID);
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
                Ingredients = recipe.RecipeIngredients.Select(ri => new RecipeIngredientDTO()
                {
                    Name = ri.Ingredient.Name,
                    Quantity = ri.Quantity,
                    Unit = ri.Unit
                }).ToList(),
                Instructions = recipe.Instructions,
            };
        }

        public RecipePaginatedResponseDTO GetRecipes(int limit, int page)
        {
            var recipes = _dbContext.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .OrderBy(recipe => recipe.CreatedAt);
            //var totalPages = Math.Ceiling((double)recipes.Count() / limit);
            
            var response = recipes.Skip((page - 1) * limit)
                .Take(limit)
                .Select(recipe => new RecipeResponseDTO
                 {
                     Id = recipe.Id,
                     Title = recipe.Title,
                     Category = recipe.Category,
                     CreatedAt = recipe.CreatedAt,
                     Ingredients = recipe.RecipeIngredients.Select(ri => new RecipeIngredientDTO()
                     {
                         Name = ri.Ingredient.Name,
                         Quantity = ri.Quantity,
                         Unit = ri.Unit
                     }).ToList(),
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

        public RecipeResponseDTO UploadImage(Guid ID, IFormFile file)
        {
            var recipe = GetRecipe(ID);
            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("Invalid file");
            }

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine("uploads", filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            recipe.Image = $"/uploads/{filename}";

            _dbContext.SaveChanges();

            return recipe;
        }
    }
}
