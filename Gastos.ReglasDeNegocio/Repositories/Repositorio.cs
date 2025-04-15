namespace Gastos.ReglasDeNegocio.Repositories
{
    public class Repositorio
    {
        public CategoriaRepositorio Categoria { get; }

        public SubcategoriaRepo Subcategoria { get; }

        public TipoDeCuentaRepository TipoDeAhorro { get; }
        public Repositorio(
            CategoriaRepositorio categoriaRepositorio,
            SubcategoriaRepo subcategoriaRepo,
            TipoDeCuentaRepository tipoDeAhorro
        ) { 
            Categoria = categoriaRepositorio;
            Subcategoria = subcategoriaRepo;
            TipoDeAhorro = tipoDeAhorro;
        }    
    }
}
