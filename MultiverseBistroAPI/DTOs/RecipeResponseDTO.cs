namespace MultiverseBistroAPI.DTOs
{
    public class RecipeResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<RecipeIngredientDTO> Ingredients { get; set; }
        public string Instructions { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class RecipeIngredientDTO
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}
