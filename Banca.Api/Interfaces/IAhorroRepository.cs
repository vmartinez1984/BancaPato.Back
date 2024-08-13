using Banca.Api.Entities;

namespace Banca.Api.Interfaces
{
    public interface IAhorroRepository
    {
        Task<int> AgregarAsycn(Ahorro ahorro);
        Task DepositarAsync(string id, MovimientoDuckBank movimiento);
        Task<List<Ahorro>> ObtenerAsync();
        Task<Ahorro> ObtenerAsync(string id);
        Task RetirarAsync(string id, MovimientoDuckBank movimiento);
    }
}
