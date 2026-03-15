using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Interfaces.Services;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
        }

        public RecipeResponseDTO CreateRecipe(RecipeCreateDTO recipeCreateDTO)
        {
            var newRecipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = recipeCreateDTO.Title,
                Category = recipeCreateDTO.Category,
                CreatedAt = DateTime.UtcNow,
                Instructions = recipeCreateDTO.Instructions,
                Image = string.Empty,
            };
            foreach (var ingr in recipeCreateDTO.Ingredients)
            {
                var ingredient = _ingredientRepository.FindByName(ingr.Name);
                if (ingredient == null)
                {
                    ingredient = new Ingredient
                    {
                        ID = Guid.NewGuid(),
                        Name = ingr.Name
                    };
                    _ingredientRepository.Add(ingredient);
                }

                newRecipe.RecipeIngredients.Add(new RecipeIngredient()
                {
                    Recipe = newRecipe,
                    Ingredient = ingredient,
                    Quantity = ingr.Quantity,
                    Unit = ingr.Unit
                });
            }

            _recipeRepository.Add(newRecipe);
            _recipeRepository.Save();

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
                Image = newRecipe.Image,
                Instructions = newRecipe.Instructions,
            };
        }

        public bool DeleteRecipe(Guid ID)
        {
            var recipe = _recipeRepository.GetFullRecipe(ID);
            if (recipe == null)
            {
                throw new InvalidOperationException($"Recipe {ID} not found");
            }

            _recipeRepository.Delete(recipe);
            _recipeRepository.Save();

            return true;
        }

        public RecipeResponseDTO GetRecipe(Guid ID)
        {
            var recipe = _recipeRepository.GetFullRecipe(ID);
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
                Image = recipe.Image,
                Instructions = recipe.Instructions,
            };
        }

        public RecipePaginatedResponseDTO GetRecipes(int limit, int page)
        {
            var recipes = _recipeRepository.GetAllRecipes();
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
                    Image = recipe.Image,
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
            var recipe = _recipeRepository.GetFullRecipe(ID);
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
            _recipeRepository.Update(recipe);
            _recipeRepository.Save();

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
                Image = recipe.Image,
                Instructions = recipe.Instructions,
            };
        }
    }
}
