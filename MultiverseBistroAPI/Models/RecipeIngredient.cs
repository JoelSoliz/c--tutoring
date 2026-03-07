namespace MultiverseBistroAPI.Models
{
    public class RecipeIngredient
    {
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null;

        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null;

        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}
