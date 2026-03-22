using MultiverseBistroAPI.DTOs;

namespace MultiverseBistroAPI.Interfaces.Services
{
    public interface IAuthService
    {
        public TokenDTO Register(RegisterDTO registerDTO);
        public TokenDTO Login(LoginDTO loginDTO);
    }
}
