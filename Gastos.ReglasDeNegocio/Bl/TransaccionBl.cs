using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class TransaccionBl
    {
        private readonly Repositorio _repositorio;

        public TransaccionBl(Repositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IdDto> AgregarAsync(string periodoIdGuid, TransaccionDtoIn movimiento)
        {
            //Periodo periodo;
            //Transaccion transaccion;
            //VersionDePresupuesto versionDePresupuesto;
            //Presupuesto presupuesto;
            //List<Transaccion> transaccions;

            //periodo = await _repositorio.Periodo.ObtenerAsync(periodoIdGuid);
            //versionDePresupuesto = await _repositorio.Version.ObtenerAsync(periodo.VersionId.ToString());
            //presupuesto = versionDePresupuesto.Presupuestos.FirstOrDefault(x => x.Id == movimiento.PresupuestoId);
            //transaccions = await _repositorio.Transaccion.ObtenerAsync(periodo.Id, presupuesto.Id);
            //presupuesto.Gastado = transaccions.Sum(x => x.Cantidad);

            //transaccion = new Transaccion
            //{
            //    Cantidad = movimiento.Cantidad,
            //     PeriodoId = periodo.Id,
            //     PresupuestoId = movimiento.PresupuestoId
            //};

           throw new NotImplementedException();
        }
    }
}
