using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class TipoDeAhorroBl
    {
        private readonly Repositorio _repositorio;

        public TipoDeAhorroBl(Repositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TipoDeCuentaDto>> ObtenerTodosAsync()
        {
            List<TipoDeCuentaDto> dtos;
            List<TipoDeCuenta> entitites;

            entitites = await _repositorio.TipoDeAhorro.ObtenerTodosAsync();
            dtos = entitites.Select(x => new TipoDeCuentaDto { Id = x.Id, Nombre = x.Nombre }).ToList();

            return dtos;
        }
    }
}
