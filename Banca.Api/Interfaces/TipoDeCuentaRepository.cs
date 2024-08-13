using Banca.Api.Entities;

namespace Banca.Api.Interfaces
{
    public interface ITipoDeCuentaRepository
    {
        Task AgregarAsync(TipoDeCuenta item);
        Task<List<TipoDeCuenta>> ObtenerTodosAsync();
    }
}
