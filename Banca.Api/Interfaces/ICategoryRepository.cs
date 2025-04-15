using Gastos.ReglasDeNegocio.Entities;

namespace Banca.Api.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Categoria>> ObtenerTodosAsync();
    }
}