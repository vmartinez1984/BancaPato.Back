using Banca.Core.Dtos;

namespace Banca.Maui.Services
{
    public class TipoDeCuentaService : BaseHttpService
    {
        private readonly string _url;

        public TipoDeCuentaService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _url = "TipoDeCuentas/";
        }

        public async Task<List<TipoDeCuentaDto>> ObtenerTodosAsync()
        {
            List<TipoDeCuentaDto> lista;

            lista = await Get<List<TipoDeCuentaDto>>(_url);

            return lista;
        }
    }
}