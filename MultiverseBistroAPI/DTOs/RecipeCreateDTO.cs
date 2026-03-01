using System.ComponentModel.DataAnnotations;

namespace MultiverseBistroAPI.DTOs
{
    public class RecipeCreateDTO
    {
        [Required]
        public string Title { get; set; }

        public string Category { get; set; } = "misc";

        [Required]
        public List<string> Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
