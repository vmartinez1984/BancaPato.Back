using Banco.Repositorios.Entities;

namespace Banca.Api.Interfaces
{
    public interface ISubcategoriaRepository
    {
        Task ActualizarAsync(Subcategorium subcategorium);
        Task AgregarAsync(Subcategorium subcategoria);
        Task<Subcategorium> ObtenerAsync(string idGuid);
        Task<List<Subcategorium>> ObtenerTodosAsync();
    }
}
