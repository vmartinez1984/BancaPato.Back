namespace Gastos.ReglasDeNegocio.Repositories;

public class Repositorio
{
    public CategoriaRepositorio Categoria { get; }

    public SubcategoriaRepo Subcategoria { get; }

    public TipoDeCuentaRepository TipoDeAhorro { get; }

    public VersionRepository Version { get; }

    public Repositorio(
        CategoriaRepositorio categoriaRepositorio,
        SubcategoriaRepo subcategoriaRepo,
        TipoDeCuentaRepository tipoDeAhorro,
        VersionRepository versionRepository
    )
    {
        Categoria = categoriaRepositorio;
        Subcategoria = subcategoriaRepo;
        TipoDeAhorro = tipoDeAhorro;
        Version = versionRepository;
    }
}