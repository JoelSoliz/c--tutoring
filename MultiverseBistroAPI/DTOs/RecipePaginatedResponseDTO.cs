namespace MultiverseBistroAPI.DTOs
{
    public class RecipePaginatedResponseDTO
    {
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public IEnumerable<RecipeResponseDTO> Data { get; set; }
    }
}
