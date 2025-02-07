using AutoMapper;
using Banca.Api.Entities;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;

namespace Banca.Api.Bl
{
    public class TipoDeCuentaBl : BaseBl
    {
        public TipoDeCuentaBl(IMapper mapper, IGastosRepository gastosRepository)
        : base(mapper, gastosRepository)
        {
        }

        internal async Task<List<TipoDeCuentaDto>> ObtenerTodosAsync()
        {
            List<TipoDeCuentaDto> dtos;
            List<TipoDeCuenta> entitites;

            entitites = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
            dtos = entitites.Select(x => new TipoDeCuentaDto { Id = x.Id, Nombre = x.Nombre }).ToList();

            return dtos;
        }
    }
}
