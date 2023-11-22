using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class MovimientoBl : BaseBl
    {
        private readonly TransaccionBl _transaccionBl;

        public MovimientoBl(DuckBankContext context, IMapper mapper, TransaccionBl transaccionBl) : base(context, mapper)
        {
            this._transaccionBl = transaccionBl;
        }

        internal async Task<IdDto> AgregarAsync(string periodoIdGuid, MovimientoDtoIn movimiento)
        {
            int concentradoraEntradaId = 1006;
            int idRetiro;
            int idDeposito;
            Presupuesto presupuesto;
            Movimiento movimientoEntity;

            idRetiro = await _transaccionBl.Retirar(concentradoraEntradaId.ToString(), new RetiroDtoIn
            {
                Cantidad = movimiento.Cantidad,
                Guid = movimiento.Guid,
                Nota = movimiento.Nota
            });
            presupuesto = await _repositorio.Presupuesto.Include(x => x.Subcategoria).Where(x => x.Id == movimiento.PresupuestoId).FirstOrDefaultAsync();
            idDeposito = await _transaccionBl.Depositar(presupuesto.AhorroId.ToString(), new DepositoDtoIn
            {
                Cantidad = movimiento.Cantidad,
                Guid = movimiento.Guid,
                Nota = movimiento.Nota,
                Concepto = presupuesto.Subcategoria.Nombre
            });
            movimientoEntity = new Movimiento
            {
                Guid = movimiento.Guid,
                Nota = movimiento.Nota,
                PeriodoId = movimiento.PeriodoId,
                PresupuestoId = movimiento.PresupuestoId,
                TransaccionId = idDeposito
            };
            _repositorio.Movimiento.Add(movimientoEntity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Guid = movimiento.Guid, Id = movimientoEntity.Id };
        }

        internal Task<List<Dtos.MovimientoDto>> ObtenerTodosAsync(string periodoIdGuid)
        {
            throw new NotImplementedException();
        }
    }
}
