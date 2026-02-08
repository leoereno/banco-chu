using BancoChu.Models;

namespace BancoChu.Services
{
    public interface IExtratoService
    {
        public Task<List<Transferencia>> GerarExtrato(string cpf, string dataInicial, string dataFinal);
    }
}
