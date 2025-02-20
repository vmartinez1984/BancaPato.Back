using Banca.Api.Entities;
using Banca.Api.Interfaces;
using MongoDB.Driver;

namespace Banca.Api.Repositories
{
    public class TipoDeCuentaRepository : BaseRepo, ITipoDeCuentaRepository
    {
        private readonly IMongoCollection<TipoDeCuenta> _collection;

        public TipoDeCuentaRepository(IConfiguration configurations) : base(configurations)
        {
            _collection = _mongoDatabase.GetCollection<TipoDeCuenta>("TipoDeAhorros");
        }

        public async Task AgregarAsync(TipoDeCuenta item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<List<TipoDeCuenta>> ObtenerTodosAsync()
        {
            List<TipoDeCuenta> lista;

            lista = (await _collection.FindAsync(_ => true)).ToList();

            return lista;
        }
    }
}
