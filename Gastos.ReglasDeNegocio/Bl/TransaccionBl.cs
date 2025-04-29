using Banca.Core.Dtos;
using DuckBank.Persistence.Interfaces;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Repositories;
using Microsoft.Extensions.Configuration;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class TransaccionBl
    {
        private readonly IRepositorio _duckBanck;
        private readonly string ahorroEje;
        private readonly Repositorio _repositorio;

        public TransaccionBl(Repositorio repositorio, IRepositorio duckBanck, IConfiguration configuration)
        {
            _repositorio = repositorio;
            _duckBanck = duckBanck;
            ahorroEje = configuration.GetSection("AhorroFondeadorGuid").Value;
        }

        public async Task<IdDto> AgregarAsync(int periodoId, TransaccionDtoIn movimiento)
        {
            Periodo periodo;
            Presupuesto presupuesto;
            Transaccion transaccion;
            PresupuestoDelPeriodo presupuestoDelPeriodo;
            List<Transaccion> transaccions;

            periodo = await _repositorio.Periodo.ObtenerAsync(periodoId.ToString());
            presupuesto = await _repositorio.Presupuesto.ObtenerAsync(movimiento.PresupuestoId);
            if (presupuesto.Subcategoria is null)
                presupuesto.Subcategoria = await _repositorio.Subcategoria.ObtenerAsync(presupuesto.SubcategoriaId.ToString());
            presupuestoDelPeriodo = await _repositorio.PresupuestoDelPeriodo.ObtenerPorPresupuestoIdAsync(movimiento.PresupuestoId);
            //Retiro del ahorro eje
            await _duckBanck.Ahorro.RetirarAsync(ahorroEje, new DuckBank.Persistence.Entities.Movimiento
            {
                Cantidad = movimiento.Cantidad,
                Concepto = $"{periodo.Nombre} {presupuesto.Subcategoria.Nombre}",
                EncodedKey = movimiento.EncodedKey,
                FechaDeRegistro = DateTime.Now
            });
            //Deposito en el ahorro destino en caso de que haya
            if (presupuesto.AhorroId is not null)
            {
                await _duckBanck.Ahorro.DepositarAsync(presupuesto.AhorroId.ToString(), new DuckBank.Persistence.Entities.Movimiento
                {
                    Cantidad = movimiento.Cantidad,
                    Concepto = $"Periodo {periodo.Id} {periodo.Nombre}",
                    EncodedKey = movimiento.EncodedKey,
                    FechaDeRegistro = DateTime.Now
                });
            }
            transaccion = new Transaccion
            {
                Cantidad = movimiento.Cantidad,
                PeriodoId = periodo.Id,
                PresupuestoId = movimiento.PresupuestoId
            };
            await _repositorio.Transaccion.AgregarAsync(transaccion);
            transaccions = await _repositorio.Transaccion.ObtenerAsync(periodo.Id, presupuesto.Id);
            presupuestoDelPeriodo.Gastado = transaccions.Sum(x => x.Cantidad);
            await _repositorio.PresupuestoDelPeriodo.ActualizarAsync(presupuestoDelPeriodo);

            return new IdDto
            {
                FechaDeRegistro = DateTime.Now,
                Guid = movimiento.EncodedKey,
            };
        }
    }
}
