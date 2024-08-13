using Banco.Repositorios.Entities;

namespace Banca.Api.Interfaces
{
    public interface IVersionRepository
    {
        Task ActualizarAsync(VersionDePresupuesto version);
        Task AgregarAsync(VersionDePresupuesto subcategoria);
        Task<VersionDePresupuesto> ObtenerAsync(string versionIdGuid);
        Task<List<VersionDePresupuesto>> ObtenerTodosAsync();
    }
}
