using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Banca.Api.Repositories
{
    public class SubcategoriaRepo : ISubcategoriaRepository
    {
        private readonly IMongoCollection<Subcategorium> _collection;

        public SubcategoriaRepo(IConfiguration configurations)
        {
            var mongoClient = new MongoClient(configurations.GetConnectionString("mongoDb"));
            var mongoDatabase = mongoClient.GetDatabase(configurations.GetConnectionString("mongoDbNombre"));
            _collection = mongoDatabase.GetCollection<Subcategorium>("Subcategorias");
        }

        public async Task ActualizarAsync(Subcategorium subcategorium)
        {
            await _collection.ReplaceOneAsync(x => x._id == subcategorium._id, subcategorium);
        }

        public async Task AgregarAsync(Subcategorium subcategoria)
        {
            if (subcategoria.Id == 0)
                subcategoria.Id =await ObtenerId();

            await _collection.InsertOneAsync(subcategoria);
        }

        public async Task<Subcategorium> ObtenerAsync(string idGuid)
        {
            int id;

            if (int.TryParse(idGuid, out id))
                return (await _collection.FindAsync(x => x.Id == id)).FirstOrDefault();
            else
                return (await _collection.FindAsync(x => x.Guid == idGuid)).FirstOrDefault();
        }

        public async Task<List<Subcategorium>> ObtenerTodosAsync()
        {
            return (await _collection.FindAsync(x=> x.EstaActivo==true)).ToList();
        }

        private async Task<int> ObtenerId()
        {
            var item = await
            _collection
             .Find(new BsonDocument()) // Puedes agregar filtros si es necesario
            .SortByDescending(r => r.Id) // Ordenar por fecha de forma descendente
            .FirstOrDefaultAsync();
            ;

            return item.Id + 1;
        }
    }
}
