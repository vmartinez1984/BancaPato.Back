using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;
using Microsoft.Extensions.Configuration;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class TransaccionBl
    {
        private readonly string ahorroEje;
        private readonly Repositorio _repositorio;
        private readonly AhorroBl _ahorroBl;

        public TransaccionBl(Repositorio repositorio, IConfiguration configuration, AhorroBl ahorroBl)
        {
            _repositorio = repositorio;
            _ahorroBl = ahorroBl;
            ahorroEje = configuration.GetSection("AhorroFondeadorGuid").Value;
        }

        public async Task<IdDto> AgregarAsync(int periodoId, TransaccionDtoIn movimiento)
        {
            Periodo periodo;
            Presupuesto presupuesto;
            Transaccion transaccion;
            List<Transaccion> transaccions;
            Subcategoria subcategoria;
            IdDto retiroId;
            IdDto depositoId = null;

            presupuesto = await _repositorio.Presupuesto.ObtenerAsync(movimiento.PresupuestoId);
            subcategoria = await _repositorio.Subcategoria.ObtenerAsync(presupuesto.SubcategoriaId.ToString());
            periodo = await _repositorio.Periodo.ObtenerAsync(periodoId.ToString());
            //Retiro del ahorro eje
            retiroId = await _ahorroBl.RetirarAsync(ahorroEje, new MovimientoDtoIn
            {
                Monto = movimiento.Cantidad,
                Concepto = $"{periodo.Nombre} {subcategoria.Nombre}",
                Encodedkey = movimiento.EncodedKey,
                PresupuestoId = presupuesto.Id
            });
            //Deposito en el ahorro destino en caso de que haya
            if (presupuesto.AhorroId is not null)
            {
                depositoId = await _ahorroBl.DepositarAsync(presupuesto.AhorroId.ToString(), new MovimientoDtoIn
                {
                    Monto = movimiento.Cantidad,
                    Concepto = $"{periodo.Nombre} {subcategoria.Nombre}",
                    Encodedkey = movimiento.EncodedKey,
                    PresupuestoId = presupuesto.Id
                });
            }
            transaccions = await _repositorio.Transaccion.ObtenerAsync(periodo.Id, presupuesto.Id);
            transaccion = new Transaccion
            {
                Monto = movimiento.Cantidad,
                PresupuestoId = movimiento.PresupuestoId,
                PeriodoId = periodo.Id,
                SaldoInicial = transaccions.Sum(x => x.Monto),
                SaldoFinal = presupuesto.Cantidad - movimiento.Cantidad,
                RetiroEncodedKey = retiroId.Guid,
                DepositoEncodedKey = depositoId is null ? null : depositoId.Guid
            };
            transaccion.Id = await _repositorio.Transaccion.AgregarAsync(transaccion);


            return new IdDto
            {
                Id = transaccion.Id,
                FechaDeRegistro = DateTime.Now,
                Guid = movimiento.EncodedKey,
            };
        }

        public async Task<List<TransaccionDto>> ObtenerTodosAsync(int periodoId)
        {
            List<Transaccion> lista;
                        
            lista = await _repositorio.Transaccion.ObtenerTodosAsync(periodoId);

            return lista.ToDtos();
        }
    }
}
