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
            int concentradoraEntradaId = 1007;
            int idRetiro;
            int? idDeposito = null;
            Presupuesto presupuesto;
            Movimiento movimientoEntity;

            idRetiro = await _transaccionBl.Retirar(concentradoraEntradaId.ToString(), new RetiroDtoIn
            {
                Cantidad = movimiento.Cantidad,
                Guid = movimiento.Guid,
                Nota = movimiento.Nota
            });
            presupuesto = await _repositorio.Presupuesto
                .Include(x => x.Subcategoria)
                .Where(x => x.Id == movimiento.PresupuestoId).FirstOrDefaultAsync();
            if (presupuesto.AhorroId != null)
            {
                idDeposito = await _transaccionBl.Depositar(presupuesto.AhorroId.ToString(), new DepositoDtoIn
                {
                    Cantidad = movimiento.Cantidad,
                    Guid = movimiento.Guid,
                    Nota = movimiento.Nota,
                    Concepto = presupuesto.Subcategoria.Nombre
                });
            }
            movimientoEntity = new Movimiento
            {
                Guid = movimiento.Guid,
                Nota = movimiento.Nota,
                PeriodoId = ObtenerPeriodoId(periodoIdGuid),
                PresupuestoId = movimiento.PresupuestoId,
                TransaccionId = idDeposito == null ? idRetiro : idDeposito,
            };
            _repositorio.Movimiento.Add(movimientoEntity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Guid = movimiento.Guid, Id = movimientoEntity.Id };
        }

        private int ObtenerPeriodoId(string periodoIdGuid)
        {
            return int.Parse(periodoIdGuid);
        }

        internal async Task<List<MovimientoDto>> ObtenerTodosAsync(string periodoIdGuid)
        {
            List<MovimientoDto> dtos;
            List<Movimiento> entities;

            entities = await _repositorio.Movimiento
                .Include(x => x.Presupuesto)
                .Include(x => x.Transaccion)
                .Where(x => x.PeriodoId == ObtenerPeriodoId(periodoIdGuid)).ToListAsync();
            dtos = entities.Select(x => new MovimientoDto
            {
                Id = x.Id,
                Cantidad = x.Transaccion.Cantidad,
                Guid = x.Guid,
                Nota = x.Nota,
                PresupuestoId = x.PresupuestoId
            }).ToList();

            return dtos;
        }
    }
}
