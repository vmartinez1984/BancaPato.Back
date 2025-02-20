using DuckBank.Persistence.Interfaces;

namespace Banca.Api.Interfaces
{
    public interface IGastosRepository
    {
        public ICategoryRepository Categoria { get;  }
        
        public ISubcategoriaRepository Subcategoria { get; }
        
        public IAhorroRepositorio Ahorro { get; }

        public ITipoDeCuentaRepository TipoDeCuenta { get; }

        public IVersionRepository Version { get; }

        public IPeriodoRepository Periodo { get; }
    }
}