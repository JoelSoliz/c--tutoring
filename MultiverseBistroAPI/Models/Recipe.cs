namespace MultiverseBistroAPI.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
