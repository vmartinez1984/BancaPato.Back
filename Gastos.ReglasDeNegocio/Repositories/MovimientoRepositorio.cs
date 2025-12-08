using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class MovimientoRepositorio
    {
        private readonly IMongoCollection<Movimiento> _collection;

        public MovimientoRepositorio(IConfiguration configurations)
        {
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
            _collection = mongoDatabase.GetCollection<Movimiento>("Movimientos");
        }

        public async Task<int> AgregarAsync(Movimiento item)
        {
            if (item.Id == 0)
                item.Id = await ObtenerId();
            await _collection.InsertOneAsync(item);

            return item.Id;
        }

        internal async Task<List<Movimiento>> ObtenerTodosPorAhorroIdAsync(string ahorroId)
        {
            if(int.TryParse(ahorroId, out int id))
            {
                return await _collection.Find(x => x.AhorroId == id).ToListAsync();
            }
            else
            {
                return await _collection.Find(x => x.AhorroEncodedkey == ahorroId).ToListAsync();
            }
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
