using Gastos.ReglasDeNegocio.Bl;

namespace Gastos.ReglasDeNegocio
{
    public class UnitOfWork
    {
        public AhorroBl Ahorro { get; }

        public CategoriaBl Categoria { get;  }

        public SubcategoriaBl Subcategoria { get; }

        public TipoDeAhorroBl TipoDeAhorro { get; }

        public UnitOfWork(
            AhorroBl   ahorroBl,
            CategoriaBl categoriaBl,
            SubcategoriaBl subcategoriaBl,
            TipoDeAhorroBl tipoDeAhorroBl
        )
        {
            Ahorro = ahorroBl;
            Categoria = categoriaBl;
            Subcategoria = subcategoriaBl;
            TipoDeAhorro = tipoDeAhorroBl;
        }
    }
}
