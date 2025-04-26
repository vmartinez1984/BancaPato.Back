using Banca.Api.Repositories;

namespace Gastos.ReglasDeNegocio.Repositories;

public class Repositorio
{
    public CategoriaRepositorio Categoria { get; }

    public SubcategoriaRepo Subcategoria { get; }

    public TipoDeCuentaRepository TipoDeAhorro { get; }

    public VersionRepository Version { get; }

    public PeriodoRepo Periodo { get; }

    public PresupuestoRepositorio Presupuesto { get;  }

    public PresupuestoDelPeriodoRepositorio PresupuestoDelPeriodo { get;  }

    public TransaccionRepositorio Transaccion { get; }
    public Repositorio(
        CategoriaRepositorio categoriaRepositorio,
        SubcategoriaRepo subcategoriaRepo,
        TipoDeCuentaRepository tipoDeAhorro,
        VersionRepository versionRepository,
        PeriodoRepo periodoRepo,
        PresupuestoRepositorio presupuestoRepositorio,
        PresupuestoDelPeriodoRepositorio presupuestoDelPeriodoRepositorio,
        TransaccionRepositorio transaccionRepositorio
    )
    {
        Categoria = categoriaRepositorio;
        Subcategoria = subcategoriaRepo;
        TipoDeAhorro = tipoDeAhorro;
        Version = versionRepository;
        Periodo = periodoRepo;
        Presupuesto = presupuestoRepositorio;
        PresupuestoDelPeriodo = presupuestoDelPeriodoRepositorio;
        Transaccion = transaccionRepositorio;
    }
}