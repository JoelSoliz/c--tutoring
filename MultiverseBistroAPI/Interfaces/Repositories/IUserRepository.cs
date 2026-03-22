using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
