using Banco.Repositorios.Entities;
using Gastos.ReglasDeNegocio.Entities;

namespace Banca.Api.Interfaces
{
    public interface ISubcategoriaRepository
    {
        Task ActualizarAsync(Subcategoria subcategorium);
        Task AgregarAsync(Subcategoria subcategoria);
        Task<Subcategoria> ObtenerAsync(string idGuid);
        Task<List<Subcategoria>> ObtenerTodosAsync();
    }
}
