using Banca.Core.Dtos;

namespace Banca.Maui.Services
{
    public class AhorroService : BaseHttpService
    {
        private readonly string _url;

        public AhorroService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
            _url = "Cuentas/";
        }

        public async Task<List<AhorroDto>> ObtenerTodosAsync() =>
             await Get<List<AhorroDto>>(_url);

        public async Task<AhorroDto> ObtenerPorIdAsync(int ahorroId) => await ObtenerPorIdAsync<AhorroDto>(_url + ahorroId);

        internal async Task AgregarAsync(AhorroDtoIn ahorro)
        {
            var data = await Post<IdDto>(ahorro, _url);

        }
    }
}
