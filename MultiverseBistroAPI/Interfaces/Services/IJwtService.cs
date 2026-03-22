using MultiverseBistroAPI.Models;

namespace MultiverseBistroAPI.Interfaces.Services
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
