using Banca.BusinessLayer.Bl;

namespace Banca.Api.Bl
{
    public class UnitOfWork
    {
        public UnitOfWork(
            CuentaBl cuentaBl,
            TransaccionBl transaccionBl,
            HistorialBl historialBl,
            CategoriaBl categoriaBl,
            SubcategoriaBl subcategoriaBl,
            VersionBl versionBl,
            PresupuestoBl presupuestoBl,
            TipoDeCuentaBl tipoDeCuentaBl,
            PeriodoBl periodoBl,
            MovimientoBl movimientoBl
        )
        {
            Categoria = categoriaBl;
            Cuenta = cuentaBl;
            Transaccion = transaccionBl;
            Historial = historialBl;
            Subcategoria = subcategoriaBl;
            Version = versionBl;
            Presupuesto = presupuestoBl;
            TipoDeCuenta = tipoDeCuentaBl;
            Periodo = periodoBl;
            Movimiento = movimientoBl;
        }

        public CuentaBl Cuenta { get; }
        public TransaccionBl Transaccion { get; }
        public HistorialBl Historial { get; internal set; }
        public SubcategoriaBl Subcategoria { get; internal set; }
        public CategoriaBl Categoria { get; set; }
        public VersionBl Version { get; set; }
        public PresupuestoBl Presupuesto { get; internal set; }
        public TipoDeCuentaBl TipoDeCuenta { get; internal set; }
        public PeriodoBl Periodo { get; internal set; }
        public MovimientoBl Movimiento { get; internal set; }
    }
}