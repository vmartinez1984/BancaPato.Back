using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Entities;
using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;

namespace Banca.Api.Bl
{
    public class TipoDeCuentaBl : BaseBl
    {
        public TipoDeCuentaBl(DuckBankContext context, IMapper mapper, IGastosRepository gastosRepository)
        : base(context, mapper, gastosRepository)
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
