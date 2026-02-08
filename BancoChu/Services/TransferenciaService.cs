using BancoChu.Data;
using BancoChu.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace BancoChu.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        
        
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly IDisponibilidadeService _disponibilidadeService;
        public TransferenciaService(IUserService userService, AppDbContext context, IDistributedCache cache, IDisponibilidadeService feriadoService)
        {
            _userService = userService;
            _context = context;
            _cache = cache;
            _disponibilidadeService = feriadoService;
        }




        public async Task<OperationResult> ProcessarTransferencia(string cpfOrigem, string cpfDestino, decimal valor)
        {
            if (valor <= 0) return OperationResult.Fail("Valor deve ser maior que zero.");

            bool feriado = await _disponibilidadeService.VerificaDisponibilidadeTransferencia(DateTime.Now.ToString("yyyy-MM-dd"));

            if (!feriado)
                return OperationResult.Fail("Operação não permitida fora de dia útil.");

            var contaOrigem = await _userService.GetContaByCpf(cpfOrigem);
            var contaDestino = await _userService.GetContaByCpf(cpfDestino);

            if(contaOrigem == null || contaDestino == null)
            {
                return OperationResult.Fail("Uma das contas não existe.");
            }

            if (contaOrigem.Saldo < valor)
                return OperationResult.Fail("Conta com saldo insuficiente para fazer a transferencia.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                contaOrigem.Saldo -= valor;
                contaDestino.Saldo += valor;

                var transferencia = new Transferencia
                {
                    CpfDestino = cpfDestino,
                    CpfOrigem = cpfOrigem,
                    Id = UuidServices.NewId(),
                    Valor = valor,
                    Data = DateTime.UtcNow
                };

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                await _cache.RemoveAsync($"conta:{contaOrigem.Id}");
                await _cache.RemoveAsync($"conta:{contaDestino.Id}");

                return OperationResult.Ok();

            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return OperationResult.Fail(ex.Message);
            }


            
        }

    }
}
