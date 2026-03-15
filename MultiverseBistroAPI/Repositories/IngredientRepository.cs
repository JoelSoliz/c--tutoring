using Microsoft.EntityFrameworkCore;
using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }

        public Ingredient FindByName(string name)
        {
            return _dbSet.FirstOrDefault(i => i.Name == name);
        }
    }
}
