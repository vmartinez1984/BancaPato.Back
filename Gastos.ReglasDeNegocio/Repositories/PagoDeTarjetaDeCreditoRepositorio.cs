using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class PagoDeTarjetaDeCreditoRepositorio
    {
        private readonly IMongoCollection<PagoTarjetaDeCredito> _collection;

        public PagoDeTarjetaDeCreditoRepositorio(IConfiguration configurations)
        {
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
            _collection = mongoDatabase.GetCollection<PagoTarjetaDeCredito>("PagosTarjetaDeCredito");
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

        internal async Task<int> AgregarAsync(PagoTarjetaDeCredito entidad)
        {
            if (entidad.Id == 0)
                entidad.Id = await ObtenerId();

            await _collection.InsertOneAsync(entidad);

            return entidad.Id;
        }

        internal async Task<List<PagoTarjetaDeCredito>> ObtenerTodosPorCompraIdEncodedkeyAsync(string idEncodedkey)
        {
            if (int.TryParse(idEncodedkey, out int compraId))
            {
                return await _collection.Find(x => x.CompraTarjetaDeCreditoId == compraId).ToListAsync();
            }
            else
            {
                return await _collection.Find(x => x.CompraTarjetaDeCreditoEncodedkey == idEncodedkey).ToListAsync();
            }
        }
    }
}
