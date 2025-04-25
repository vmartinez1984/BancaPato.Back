using Gastos.ReglasDeNegocio.Bl;


namespace Gastos.ReglasDeNegocio
{
    public class UnitOfWork
    {
        public AhorroBl Ahorro { get; }

        public CategoriaBl Categoria { get;  }

        public SubcategoriaBl Subcategoria { get; }

        public TipoDeAhorroBl TipoDeAhorro { get; }

        public VersionBl Version { get; }

        public PeriodoBl Periodo { get; }

        public TransaccionBl Transaccion { get; }

        public PresupuestoBl Presupuesto { get; }

        public UnitOfWork(
            AhorroBl   ahorroBl,
            CategoriaBl categoriaBl,
            SubcategoriaBl subcategoriaBl,
            TipoDeAhorroBl tipoDeAhorroBl,
            VersionBl versionBl,
            PeriodoBl periodoBl,
            TransaccionBl transaccionBl,        
            PresupuestoBl presupuesto
        )
        {
            Ahorro = ahorroBl;
            Categoria = categoriaBl;
            Subcategoria = subcategoriaBl;
            TipoDeAhorro = tipoDeAhorroBl;
            Version = versionBl;
            Periodo = periodoBl;
            Transaccion = transaccionBl;
            Presupuesto = presupuesto;
        }
    }
}
