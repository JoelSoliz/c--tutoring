using System.ComponentModel.DataAnnotations;

namespace MultiverseBistroAPI.Models
{
    public class Ingredient
    {
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
