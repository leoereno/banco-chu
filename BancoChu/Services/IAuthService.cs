using BancoChu.Models;

namespace BancoChu.Services
{
    public interface IAuthService
    {
        public string GenerateToken(Conta conta);
    }
}
