using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class TipoDeCuentaRepository : BaseRepositorio
    {
        private readonly IMongoCollection<TipoDeCuenta> _collection;

        public TipoDeCuentaRepository(IConfiguration configuration): base(configuration)
        {
            _collection = _mongoDatabase.GetCollection<TipoDeCuenta>("TipoDeAhorros");
        }

        public async Task AgregarAsync(TipoDeCuenta item)
        {
            item.Id = await ObtenerId();
            await _collection.InsertOneAsync(item);
        }
        
        public async Task ActualizarAsync(TipoDeCuenta entidad) => await _collection.ReplaceOneAsync(x => x._id == entidad._id, entidad);
        
        public async Task<List<TipoDeCuenta>> ObtenerTodosAsync() => await _collection.Find(_ => true).ToListAsync();
               
        private async Task<int> ObtenerId()
        {
            var item = await
            _collection
            .Find(new BsonDocument()) // Puedes agregar filtros si es necesario
            .SortByDescending(r => r.Id) // Ordenar por fecha de forma descendente
            .FirstOrDefaultAsync();
            ;
            if (item == null)
                return 1;

            return item.Id + 1;
        }
    }
}
