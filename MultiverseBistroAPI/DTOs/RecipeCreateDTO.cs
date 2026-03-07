using System.ComponentModel.DataAnnotations;

namespace MultiverseBistroAPI.DTOs
{
    public class RecipeCreateDTO
    {
        [Required]
        public string Title { get; set; }

        public string Category { get; set; } = "misc";

        [Required]
        public List<IngredientCreateDTO> Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }
    }

    public class IngredientCreateDTO
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
    }
}
