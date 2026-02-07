using BancoChu.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace BancoChu.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        
        private readonly string _feriadoUrl = "https://brasilapi.com.br/api/feriados/v1/2025";

        public async Task<bool> VerificaDisponibilidadeTransferencia(string dataAtual)
        {

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(this._feriadoUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var feriados = JsonSerializer.Deserialize<Feriado[]>(jsonResponse);

            var possivel = feriados.FirstOrDefault(x => x.date == dataAtual, null);

            return possivel == null;

            
        }
    }
}
