

namespace Banca.Api.Interfaces
{
    public interface IGastosRepository
    {
        public ICategoryRepository Categoria { get;  }
        public ISubcategoriaRepository Subcategoria { get; }
        public IAhorroRepository Ahorro { get; }
        public ITipoDeCuentaRepository TipoDeCuenta { get; }

        public IVersionRepository Version { get; }
    }
}