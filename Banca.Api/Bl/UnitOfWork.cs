namespace Banca.Api.Bl
{
    public class UnitOfWork
    {
        public UnitOfWork(
            TransaccionBl transaccionBl,
            HistorialBl historialBl,
            VersionBl versionBl,
            PresupuestoBl presupuestoBl,
            PeriodoBl periodoBl,
            MovimientoBl movimientoBl
        )
        {
            Transaccion = transaccionBl;
            Historial = historialBl;
            Version = versionBl;
            Presupuesto = presupuestoBl;
            Periodo = periodoBl;
            Movimiento = movimientoBl;
        }

        public TransaccionBl Transaccion { get; }
        public HistorialBl Historial { get; }
        public VersionBl Version { get; }
        public PresupuestoBl Presupuesto { get; }
        public PeriodoBl Periodo { get; }
        public MovimientoBl Movimiento { get; }
    }
}