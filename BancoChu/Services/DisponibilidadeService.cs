using BancoChu.Models;
using System.Text.Json;

namespace BancoChu.Services
{
    public class DisponibilidadeService : IDisponibilidadeService
    {
        private readonly string _feriadoUrl = "https://brasilapi.com.br/api/feriados/v1/2025";
        private readonly IHttpClientFactory _httpClientFactory;

        public DisponibilidadeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> VerificaFeriado(string dataAtual)
        {

            var httpClient = _httpClientFactory.CreateClient();
            try
            {
                var response = await httpClient.GetAsync(this._feriadoUrl);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var feriados = JsonSerializer.Deserialize<Feriado[]>(jsonResponse);

                return feriados?.Any(x => x.date == dataAtual) ?? false;

            }
            catch
            {
                return false;
            }


        }

        public async Task<bool> VerificaDisponibilidadeTransferencia(string dataAtual)
        {

            var date = DateTime.Parse(dataAtual);
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return false;

            bool feriado = await VerificaFeriado(dataAtual);
            return !feriado;
        }
    }
}
