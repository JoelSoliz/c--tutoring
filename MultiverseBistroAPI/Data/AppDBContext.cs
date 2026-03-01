using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Data
{
    public class AppDBContext
    {
        public AppDBContext()
        {
            Recipes = new List<Recipe> {
                new Recipe()
                {
                    Id = Guid.NewGuid(),
                    Title = "Sopa de Mani",
                    Category = "sopa",
                    Ingredients = new List<string> { "mani", "carne res", "papa", "fideo", "verduras varias"},
                    Instructions = "Picar verduras y papa, picar carne, hervir el mani por 2 horas, hervir la carne, poner verduras en la carne, tostar fideo, poner el fideo en la carne, poner el mani, dejar cocer y listo.",
                    CreatedAt = new DateTime(2026, 3, 1, 10, 50, 00)
                }
            };
        }

        public List<Recipe> Recipes;
    }
}
