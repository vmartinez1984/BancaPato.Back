using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class PagoDeTarjetaDeCreditoBl(CompraTarjetaDeCreditoRepositorio compraTarjetaDeCreditoRepositorio, PagoDeTarjetaDeCreditoRepositorio pagoDeTarjetaDeCreditoRepositorio)
    {
        private readonly CompraTarjetaDeCreditoRepositorio _compraRepositorio = compraTarjetaDeCreditoRepositorio;
        private readonly PagoDeTarjetaDeCreditoRepositorio _pagoRespositorio = pagoDeTarjetaDeCreditoRepositorio;

        public async Task<IdDto> AgregarAsync(PagoTdcDtoIn pagoDto)
        {
            CompraTarjetaDeCredito compra;
            PagoTarjetaDeCredito pago;

            compra = await _compraRepositorio.ObtenerPorIdEncodedkeyAsync(pagoDto.CompraTdcIdEndodedkey);
            pago = pagoDto.ToEntity();
            pago.CompraTarjetaDeCreditoId = compra.Id;
            pago.CompraTarjetaDeCreditoEncodedkey = compra.Encodedkey;
            compra.Saldo = compra.Saldo - pago.Monto;
            await _compraRepositorio.ActualizarAsync(compra);
            pago.Id = await _pagoRespositorio.AgregarAsync(pago);

            return new IdDto { Id = pago.Id, Guid = pago.Encodekey };
        }

        public async Task<List<PagoTdcDto>> ObtenerTodosPorCompraIdEncodedkeyAsync(string idEncodedkey)
        {
            List<PagoTarjetaDeCredito> pagos;

            pagos = await _pagoRespositorio.ObtenerTodosPorCompraIdEncodedkeyAsync(idEncodedkey);

            return pagos.ToDtos();
        }
    }
}
