using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class CompraTarjetaDeCreditoBl
    {
        private readonly CompraTarjetaDeCreditoRepositorio _repositorio;

        public CompraTarjetaDeCreditoBl(CompraTarjetaDeCreditoRepositorio compraTarjetaDeCreditoRepositorio)
        {
            this._repositorio = compraTarjetaDeCreditoRepositorio;
        }


        public async Task<IdDto> AgregarAsync(CompraTdcDtoIn compra)
        {
            CompraTarjetaDeCredito compraTarjetaDeCredito;

            compraTarjetaDeCredito = compra.ToEntity();
            compraTarjetaDeCredito.Id = await _repositorio.AgregarAsync(compraTarjetaDeCredito);

            return new IdDto { Id = compraTarjetaDeCredito.Id, Guid = compraTarjetaDeCredito.Encodedkey };
        }

        public async Task<List<CompraTdcDto>> ObtenerTodosAsync()
        {
            List<CompraTarjetaDeCredito> compras;

            compras = await _repositorio.ObtenerTodosAsync();

            return compras.ToDtos();
        }
    }

}
