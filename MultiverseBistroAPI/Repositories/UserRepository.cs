using MultiverseBistroAPI.Data;
using MultiverseBistroAPI.Interfaces.Repositories;
using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }

        public User GetUserByEmail(string email)
        {
            return _dbSet.Where(user => user.Email == email).FirstOrDefault();
        }
    }
}
