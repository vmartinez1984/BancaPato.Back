using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Banca.Api.Repositories
{
    public class PeriodoRepo : IPeriodoRepository
    {
        private readonly IMongoCollection<Periodo> _collection;

        public PeriodoRepo(IConfiguration configurations)
        {
            var mongoClient = new MongoClient(configurations.GetConnectionString("mongoDb"));
            var mongoDatabase = mongoClient.GetDatabase(configurations.GetConnectionString("mongoDbNombre"));
            _collection = mongoDatabase.GetCollection<Periodo>("Periodos");
        }

        public async Task<List<Periodo>> ObtenerAsync(bool estaActivo = true)
        {
            var data=  await _collection.FindAsync(x => x.EstaActivo == estaActivo);

            return data.ToList();
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

        public async Task<int> AgregarAsync(Periodo entidad)
        {
            if (entidad.Id == 0)
                entidad.Id = await ObtenerId();

            await _collection.InsertOneAsync(entidad);

            return entidad.Id;
        }

        public async Task<Periodo> ObtenerAsync(string idGuid)
        {
            int id;

            if(int.TryParse(idGuid, out id))
                return (await _collection.FindAsync(x=> x.Id == id)).FirstOrDefault();
            else
                return (await _collection.FindAsync(x => x.Guid == idGuid)).FirstOrDefault();
        }

        public async Task ActualizarAsinc(Periodo periodo)
        {
            await _collection.ReplaceOneAsync(x => x._id == periodo._id, periodo);
        }
    }
}
