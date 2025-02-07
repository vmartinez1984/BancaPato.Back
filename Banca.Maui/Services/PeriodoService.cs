using Banca.Core.Dtos;

namespace Banca.Maui.Services
{
    public class PeriodoService : BaseHttpService
    {
        public string _endpoint { get; }

        public PeriodoService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _endpoint = "Periodos";
        }

        public async Task<List<PeriodoDto>> ObtenerTodosAsync() => await Get<List<PeriodoDto>>(_endpoint);
        
    }
}