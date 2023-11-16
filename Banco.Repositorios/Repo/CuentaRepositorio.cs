using Banco.Repositorios.Entities;
using Banco.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Banco.Repositorios.Repo
{
    public class Repo : IRepositorio
    {
        public Repo(ICuentaRepositorio cuenta)
        {
            Cuenta = cuenta;
        }
        public ICuentaRepositorio Cuenta { get; }
    }

    public class BaseRepositorio
    {
        public readonly DuckBankContext _context;

        public BaseRepositorio(DuckBankContext context)
        {
            this._context = context;
        }
    }

    public class CuentaRepositorio : BaseRepositorio, ICuentaRepositorio
    {
        public CuentaRepositorio(DuckBankContext context) : base(context)
        {
        }

        public async Task<int> AgregarAsync(Cuentum cuenta)
        {
            await _context.Cuenta.AddAsync(cuenta);
            await _context.SaveChangesAsync();

            return cuenta.Id;
        }

        public Task<Cuentum> ObtenerAsync(string idGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cuentum>> ObtenerAsync()
        {
            return await _context.Cuenta.ToListAsync<Cuentum>();
        }
    }

    public class TransaccionRepositorio : BaseRepositorio
    {
        public TransaccionRepositorio(DuckBankContext context) : base(context)
        {
        }

        public async Task<int> Agregar(Transaccion transaccion)
        {
            _context.Transaccions.Add(transaccion);
            await _context.SaveChangesAsync();

            return transaccion.Id;
        }        
    }
}
