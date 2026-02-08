using BancoChu.Models;

namespace BancoChu.Services
{
    public interface ITransferenciaService
    {
        public Task<OperationResult> ProcessarTransferencia(string cpfOrigem, string cpfDestino, decimal valor);
    }
}
