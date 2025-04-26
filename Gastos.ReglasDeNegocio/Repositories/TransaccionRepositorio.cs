using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class TransaccionRepositorio: BaseRepositorio
    {
        private readonly IMongoCollection<Transaccion> _collection;

        public TransaccionRepositorio(IConfiguration configurations) : base(configurations)
        {
            _collection = _mongoDatabase.GetCollection<Transaccion>("Transacciones");
        }

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

        internal async Task<int> AgregarAsync(Transaccion entidad)
        {
            if (entidad.Id == 0)
                entidad.Id = await ObtenerId();

            await _collection.InsertOneAsync(entidad);

            return entidad.Id;
        }

        internal async Task<List<Transaccion>> ObtenerAsync(int periodoId, int presupuestoId)
        =>  await _collection.Find(x => x.PeriodoId == periodoId && x.PresupuestoId == presupuestoId).ToListAsync();
        
    }
}
