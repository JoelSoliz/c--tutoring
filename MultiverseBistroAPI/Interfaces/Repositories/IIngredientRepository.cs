using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Interfaces.Repositories
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        public Ingredient FindByName(string name);
    }
}
