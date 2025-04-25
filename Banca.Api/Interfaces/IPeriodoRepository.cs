using Gastos.ReglasDeNegocio.Entities;

namespace Banca.Api.Interfaces
{
    public interface IPeriodoRepository
    {
        Task ActualizarAsinc(Periodo periodo);
        Task<int> AgregarAsync(Periodo entity);

        Task<List<Periodo>> ObtenerAsync(bool estaActivo = true);

        Task<Periodo> ObtenerAsync(string idGuid);
    }
}