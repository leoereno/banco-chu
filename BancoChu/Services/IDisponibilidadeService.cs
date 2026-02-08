using BancoChu.Models;
using System.Text.Json;

namespace BancoChu.Services
{
    public interface IDisponibilidadeService
    {
        public Task<bool> VerificaFeriado(string dataAtual);
        public Task<bool> VerificaDisponibilidadeTransferencia(string dataAtual);


    }
}
