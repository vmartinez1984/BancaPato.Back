using Banco.Repositorios.Entities;

namespace Banca.Api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categorium>> ObtenerTodosAsync();
    }
}