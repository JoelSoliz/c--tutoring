using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(recipeIngredient => new { recipeIngredient.RecipeId, recipeIngredient.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Recipe)
                .WithMany(recipe => recipe.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Ingredient)
                .WithMany(ingredient => ingredient.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.IngredientId);

            modelBuilder.Entity<Recipe>()
                .HasOne(recipe => recipe.Creator)
                .WithMany(user => user.Recipes)
                .HasForeignKey(recipe => recipe.CreatedBy);

            modelBuilder.Entity<User>()
                .HasKey(user => user.UserId);
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
