using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class PresupuestoRepositorio : BaseRepositorio
    {
        private readonly IMongoCollection<Presupuesto> _collection;

        public PresupuestoRepositorio(IConfiguration configurations) : base(configurations)
        {
            _collection = _mongoDatabase.GetCollection<Presupuesto>("Presupuestos");
        }

        internal async Task ActualizarAsync(Presupuesto entidad) => await _collection.ReplaceOneAsync(x => x._id == entidad._id, entidad);

        internal async Task<int> AgregarAsync(Presupuesto entidad)
        {
            if (entidad.Id == 0)
                entidad.Id = await ObtenerId();

            await _collection.InsertOneAsync(entidad);

            return entidad.Id;
        }

        internal async Task<Presupuesto> ObtenerAsync(int presupuestoId) => await _collection.Find(x => x.Id == presupuestoId).FirstOrDefaultAsync();

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

        internal async Task<List<Presupuesto>> ObtenerPorVersionIdAsync(int versionId) => await _collection.Find(x => x.VersionId == versionId).ToListAsync();
    }
}
