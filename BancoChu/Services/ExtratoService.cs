using BancoChu.Data;
using BancoChu.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Services
{
    public class ExtratoService : IExtratoService
    {
        private readonly ITransferenciaService _transferenciaService;
        private readonly IUserService _UserService;
        private readonly AppDbContext _appDbContext;

        public ExtratoService(ITransferenciaService transferenciaService, IUserService userService, AppDbContext appDbContext)
        {
            _transferenciaService = transferenciaService;
            _UserService = userService;
            _appDbContext = appDbContext;
        }
        public async Task<List<Transferencia>> GerarExtrato(string cpf, string dataInicial, string dataFinal)
        {
            var conta = await _UserService.GetContaByCpf(cpf);

            var inicio = DateTime.Parse(dataInicial).Date;
            var fim = DateTime.Parse(dataFinal);

            var extrato = await _appDbContext.Transferencias
                .Where(t => t.CpfDestino == cpf || t.CpfOrigem == cpf)
                .Where(t => t.Data >= inicio && t.Data <= fim)
                .OrderByDescending(t => t.Data)
                .ToListAsync();

            return extrato;

        }
    }
}
