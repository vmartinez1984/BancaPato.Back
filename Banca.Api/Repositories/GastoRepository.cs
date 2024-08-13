using Banca.Api.Interfaces;

namespace Banca.Api.Repositories
{
    public class GastoRepository : IGastosRepository
    {
        public ICategoryRepository Categoria { get ; }

        public ISubcategoriaRepository Subcategoria { get; }

        public ITipoDeCuentaRepository TipoDeCuenta { get; }

        public IAhorroRepository Ahorro { get; }

        public IVersionRepository Version { get; }

        public IPeriodoRepository Periodo { get; }

        public GastoRepository(
            ICategoryRepository categoryRepository, 
            ISubcategoriaRepository subcategoriaRepository,
            ITipoDeCuentaRepository tipoDeCuentaRepository,
            IAhorroRepository ahorroRepository,
            IVersionRepository version,
            IPeriodoRepository periodoRepository
            )
        {
            Categoria = categoryRepository;
            Subcategoria = subcategoriaRepository;
            TipoDeCuenta = tipoDeCuentaRepository;
            Ahorro = ahorroRepository;
            Version = version;
            Periodo = periodoRepository;
        }
    }
}