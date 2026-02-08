using BancoChu.Data;
using BancoChu.Models;
using BancoChu.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace BancoChu.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;

        public UserService(AppDbContext appDbContext, IDistributedCache cache)
        {
            _context = appDbContext;
            _cache = cache;
        }

        public async Task<Conta> CreateUser(CreateAccountDto createAccountDto)
        {
            var conta = new Conta
            {
                Id = UuidServices.NewId(),
                Nome = createAccountDto.Nome,
                Sobrenome = createAccountDto.Sobrenome,
                Email = createAccountDto.Email,
                Cpf = createAccountDto.Cpf,
                Saldo = createAccountDto.Saldo, 
            };

            _context.Contas.Add(conta);
            await _context.SaveChangesAsync();

            return conta;
        }

        public async Task<Conta?> GetContaById(Guid id)
        {
            string cacheKey = $"conta:{id}";

            var cachedConta = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedConta))
            {
                return JsonSerializer.Deserialize<Conta>(cachedConta);


            }

            var conta = await _context.Contas.FindAsync(id);

            if (conta != null)
            {
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                var serializedConta = JsonSerializer.Serialize(conta);
                await _cache.SetStringAsync(cacheKey, serializedConta, options);
            }

            return conta;
        }
        
        public async Task<Conta?> GetContaByCpf(string cpf)
        {
            string cacheKey = $"conta:cpf:{cpf}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))
                return JsonSerializer.Deserialize<Conta>(cached);

            var conta = await _context.Contas.FirstOrDefaultAsync(c => c.Cpf == cpf);

            if (conta != null)
            {

                var serialized = JsonSerializer.Serialize(conta);
                await _cache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }

            return conta;
        }

        public async Task<List<Conta>> GetUsers()
        {
            return await _context.Contas.ToListAsync();
        }
    }
}
