using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class CompraTarjetaDeCreditoBl
    {
        private readonly CompraTarjetaDeCreditoRepositorio _repositorio;
        private readonly DateOnly fechaDeCorte;
        private readonly DateOnly fechaDePago;

        public CompraTarjetaDeCreditoBl(CompraTarjetaDeCreditoRepositorio compraTarjetaDeCreditoRepositorio)
        {
            this._repositorio = compraTarjetaDeCreditoRepositorio;
            DateTime fechaActual = DateTime.Now;
            fechaDeCorte = new DateOnly(fechaActual.Year, fechaActual.Month, 3);
            fechaDePago = new DateOnly(fechaActual.Year, fechaActual.Month, 23);
            DateOnly fechaActual2 = new DateOnly(fechaActual.Year, fechaActual.Month, fechaActual.Day);
            if(fechaActual2 >= fechaDeCorte)
            {
                fechaActual = fechaActual.AddMonths(1);
                fechaDeCorte = new DateOnly(fechaActual.Year, fechaActual.Month, 3);
                fechaDePago = new DateOnly(fechaActual.Year, fechaActual.Month, 23);
            }
        }


        public async Task<IdDto> AgregarAsync(CompraTdcDtoIn compra)
        {
            CompraTarjetaDeCredito compraTarjetaDeCredito;

            compraTarjetaDeCredito = compra.ToEntity();
            if (compraTarjetaDeCredito.MesesSinIntereses == 0)
            {
                compraTarjetaDeCredito.FechaDePago = fechaDePago;
                compraTarjetaDeCredito.Id = await _repositorio.AgregarAsync(compraTarjetaDeCredito);
            }
            else
            {
                decimal monto = compraTarjetaDeCredito.Monto / compraTarjetaDeCredito.MesesSinIntereses;
                compraTarjetaDeCredito.Saldo = 0;
                compraTarjetaDeCredito.FechaDePago = fechaDePago.AddMonths(compraTarjetaDeCredito.MesesSinIntereses);
                compraTarjetaDeCredito.Id = await _repositorio.AgregarAsync(compraTarjetaDeCredito);
                int mesesSinIntereses = compraTarjetaDeCredito.MesesSinIntereses;
                for (int i = 0; i < mesesSinIntereses; i++)
                {
                    compraTarjetaDeCredito._id = null;
                    compraTarjetaDeCredito.FechaDeCompra = fechaDeCorte.AddMonths(i);
                    compraTarjetaDeCredito.FechaDePago = fechaDePago.AddMonths(i);
                    compraTarjetaDeCredito.MesesSinIntereses = 0;
                    compraTarjetaDeCredito.Monto = decimal.Round(monto, 2, System.MidpointRounding.AwayFromZero);
                    compraTarjetaDeCredito.Saldo = compraTarjetaDeCredito.Monto;
                    compraTarjetaDeCredito.Id = await _repositorio.AgregarAsync(compraTarjetaDeCredito);
                }
            }

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
