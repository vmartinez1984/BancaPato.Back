using MongoDB.Driver;
using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;
using MongoDB.Bson;

namespace Banca.Api.Repositories
{
    public class VersionRepository :BaseRepo, IVersionRepository
    {
        private readonly IMongoCollection<VersionDePresupuesto> _collection;

        public VersionRepository(IConfiguration configurations):base(configurations)
        {            
            _collection = _mongoDatabase.GetCollection<VersionDePresupuesto>("Versiones");
        }

        public async Task ActualizarAsync(VersionDePresupuesto version)
        {
            await _collection.ReplaceOneAsync(x => x._id == version._id, version);
        }

        public async Task AgregarAsync(VersionDePresupuesto subcategoria)
        {
            if (subcategoria.Id == 0)
                subcategoria.Id = await ObtenerId();

            await _collection.InsertOneAsync(subcategoria);
        }

        public async Task<VersionDePresupuesto> ObtenerAsync(string idGuid)
        {
            VersionDePresupuesto entitidad;
            int id;

           // entitidad = (await _collection.FindAsync(
           //    new BsonDocument("$or", new BsonArray
           //    {
           //         new BsonDocument("Id", id),
           //         new BsonDocument("Guid", id)
           //    })
           //)).FirstOrDefault();
           if(int.TryParse(idGuid, out id))
                entitidad = (await _collection.FindAsync(x => x.Id == id)).FirstOrDefault();
           else
                entitidad = (await _collection.FindAsync(x => x.Guid == idGuid)).FirstOrDefault();

            return entitidad;
        }

        public async Task<List<VersionDePresupuesto>> ObtenerTodosAsync()
        {
            return (await _collection.FindAsync(x => x.EstaActivo == true)).ToList();
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
    }
}