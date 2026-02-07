namespace BancoChu.Services
{
    public interface ITransferenciaService
    {
        public Task<bool> VerificaDisponibilidadeTransferencia(string dataAtual);
    }
}
