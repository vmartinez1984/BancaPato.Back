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

        public GastoRepository(
            ICategoryRepository categoryRepository, 
            ISubcategoriaRepository subcategoriaRepository,
            ITipoDeCuentaRepository tipoDeCuentaRepository,
            IAhorroRepository ahorroRepository,
            IVersionRepository version
            )
        {
            Categoria = categoryRepository;
            Subcategoria = subcategoriaRepository;
            TipoDeCuenta = tipoDeCuentaRepository;
            Ahorro = ahorroRepository;
            Version = version;
        }
    }
}