using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class MovimientoBl : BaseBl
    {
        private readonly TransaccionBl _transaccionBl;

        public MovimientoBl(DuckBankContext context, IMapper mapper, TransaccionBl transaccionBl, IGastosRepository gastosRepository)
        : base(context, mapper, gastosRepository)
        {
            this._transaccionBl = transaccionBl;
        }

        internal async Task<IdDto> AgregarAsync(string periodoIdGuid, MovimientoDtoIn movimiento)
        {
            string ahorroEntradaId = "39";
            int movimientoId;
            Presupuesto presupuesto;
            Periodo periodo;

            if (string.IsNullOrEmpty(movimiento.Guid))
                movimiento.Guid = Guid.NewGuid().ToString();
            periodo = await _repositorioMongo.Periodo.ObtenerAsync(periodoIdGuid);
            presupuesto = periodo.Version.Presupuestos.FirstOrDefault(x => x.Id == movimiento.PresupuestoId);
           await _repositorioMongo.Ahorro.DepositarAsync(presupuesto.AhorroId.ToString(), new Entities.MovimientoDuckBank
            {
                Cantidad = movimiento.Cantidad,
                FechaDeRegistro = DateTime.Now,
                Concepto = periodo.Nombre,
                Referencia = movimiento.Guid
            });
            await _repositorioMongo.Ahorro.RetirarAsync(ahorroEntradaId, new Entities.MovimientoDuckBank
            {
                Cantidad = movimiento.Cantidad,
                FechaDeRegistro = DateTime.Now,
                Concepto = $"{periodo.Nombre} - {presupuesto.Subcategoria.Nombre}",
                Referencia = movimiento.Guid
            });
            movimientoId = presupuesto.Movimientos.Count() + 1;            
            presupuesto.Movimientos.Add(new Movimiento
            {
                Id = movimientoId,
                Guid = movimiento.Guid,
                Cantidad = movimiento.Cantidad
            });
            await _repositorioMongo.Periodo.ActualizarAsinc(periodo);

            return new IdDto { Guid = movimiento.Guid.ToString() };
        }

        private int ObtenerPeriodoId(string periodoIdGuid)
        {
            return int.Parse(periodoIdGuid);
        }

        internal async Task<List<MovimientoDto>> ObtenerTodosAsync(string periodoIdGuid)
        {
            //List<MovimientoDto> dtos;
            //List<Movimiento> entities;

            //entities = await _repositorio.Movimiento
            //    .Include(x => x.Presupuesto)
            //    .Include(x => x.Transaccion)
            //    .Where(x => x.PeriodoId == ObtenerPeriodoId(periodoIdGuid)).ToListAsync();
            //dtos = entities.Select(x => new MovimientoDto
            //{
            //    Id = x.Id,
            //    Cantidad = x.Transaccion.Cantidad,
            //    Guid = x.Guid,
            //    Nota = x.Nota,
            //    PresupuestoId = x.PresupuestoId
            //}).ToList();

            return null;
        }
    }
}
