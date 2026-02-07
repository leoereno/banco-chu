using BancoChu.Models;
using BancoChu.Models.DTO;

namespace BancoChu.Services
{
    public interface IUserService
    {
        public Task<Conta> CreateUser(CreateAccountDto createAccountDto);
        public Task<List<Conta>> GetUsers();
        public Task<Conta?> GetContaById(Guid id);
    }
}
