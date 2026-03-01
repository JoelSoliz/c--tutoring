namespace MultiverseBistroAPI.DTOs
{
    public class RecipeResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
