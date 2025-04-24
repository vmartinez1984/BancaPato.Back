using AutoMapper;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;
using Banco.Repositorios.Entities;
using DuckBank.Persistence.Entities;
using Gastos.ReglasDeNegocio.Entities;

namespace Banca.Api.Bl
{
    public class MovimientoBl : BaseBl
    {
        private readonly TransaccionBl _transaccionBl;
        private readonly string _ahorroFondeador;
        public MovimientoBl(IMapper mapper, TransaccionBl transaccionBl, IGastosRepository gastosRepository, IConfiguration configuration)
        : base( mapper, gastosRepository)
        {
            _transaccionBl = transaccionBl;
            _ahorroFondeador = configuration.GetSection("AhorroFondeadorGuid").Value;
        }

        internal async Task<IdDto> AgregarAsync(string periodoIdGuid, MovimientoDtoIn movimiento)
        {            
            int movimientoId;
            Presupuesto presupuesto;
            Periodo periodo;

            if (string.IsNullOrEmpty(movimiento.Referencia))
                movimiento.Referencia = Guid.NewGuid().ToString();
            periodo = await _repositorioMongo.Periodo.ObtenerAsync(periodoIdGuid);
            presupuesto = periodo.Version.Presupuestos.FirstOrDefault(x => x.Id == movimiento.PresupuestoId);
           await _repositorioMongo.Ahorro.DepositarAsync(presupuesto.AhorroId.ToString(), new Movimiento
            {
                Cantidad = movimiento.Cantidad,                
                Concepto = periodo.Nombre,
                EncodedKey = movimiento.Referencia
            });
            await _repositorioMongo.Ahorro.RetirarAsync(_ahorroFondeador, new Movimiento
            {
                Cantidad = movimiento.Cantidad,                
                Concepto = $"{periodo.Nombre} - {presupuesto.Subcategoria.Nombre}",
                EncodedKey = movimiento.Referencia
            });
            movimientoId = presupuesto.Movimientos.Count() + 1;            
            presupuesto.Movimientos.Add(new MovimientoDePeridodo
            {
                Id = movimientoId,
                Guid = movimiento.Referencia,
                Cantidad = movimiento.Cantidad
            });
            await _repositorioMongo.Periodo.ActualizarAsinc(periodo);

            return new IdDto { Guid = movimiento.Referencia.ToString() };
        }

        internal  Task<List<MovimientoDto>> ObtenerTodosAsync(string periodoIdGuid)
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
