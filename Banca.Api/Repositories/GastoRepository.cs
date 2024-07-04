using Banca.Api.Interfaces;

namespace Banca.Api.Repositories
{
    public class GastoRepository : IGastosRepository
    {
        public ICategoryRepository Categoria { get ; set; }

        public GastoRepository(ICategoryRepository categoryRepository){
            this.Categoria = categoryRepository;
        }
    }
}