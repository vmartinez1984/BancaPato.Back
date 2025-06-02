using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class PresupuestoDelPeriodoRepositorio: BaseRepositorio
    {
        private readonly IMongoCollection<PresupuestoDelPeriodo> _collection;

        public PresupuestoDelPeriodoRepositorio(IConfiguration configurations) : base(configurations)
        {
            _collection = _mongoDatabase.GetCollection<PresupuestoDelPeriodo>("PresupuestosDelPeriodo");
        }

        public async Task<List<PresupuestoDelPeriodo>> ObtenerAsync(bool estaActivo = true) => await _collection.Find(x => x.EstaActivo == estaActivo).ToListAsync();

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

        public async Task<int> AgregarAsync(PresupuestoDelPeriodo entidad)
        {
            if (entidad.Id == 0)
                entidad.Id = await ObtenerId();

            await _collection.InsertOneAsync(entidad);

            return entidad.Id;
        }

        public async Task<PresupuestoDelPeriodo> ObtenerPorPresupuestoIdAsync(int presupuestoId, int periodoId)
        => (await _collection.FindAsync(x => x.PresupuestoId == presupuestoId && x.PeriodoId == periodoId)).FirstOrDefault();
         

        public async Task ActualizarAsync(PresupuestoDelPeriodo periodo) => await _collection.ReplaceOneAsync(x => x._id == periodo._id, periodo);

        internal async Task<List<PresupuestoDelPeriodo>> ObtenerPorPeriodoIdAsync(int periodoId)
        => await _collection.Find(x => x.PeriodoId == periodoId).ToListAsync();
    }

}
