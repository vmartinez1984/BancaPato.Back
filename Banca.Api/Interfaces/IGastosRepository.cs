using Banco.Repositorios.Entities;

namespace Banca.Api.Interfaces
{
    public interface IGastosRepository
    {
        public ICategoryRepository Categoria { get; set; }        
    }
}