using Banco.Repositorios.Entities;

namespace Banco.Repositorios.Interfaces
{
    public interface IRepositorio
    {
        public ICuentaRepositorio Cuenta { get; }
    }

    public interface ICuentaRepositorio
    {
        Task<int> AgregarAsync(Cuentum cuenta);

        Task<Cuentum> ObtenerAsync(string idGuid);

        Task<List<Cuentum>> ObtenerAsync();
    }
}
