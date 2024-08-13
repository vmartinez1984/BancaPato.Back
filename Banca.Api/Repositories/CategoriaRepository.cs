using MongoDB.Driver;
using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;

namespace Banca.Api.Repositories
{
    public class CategoriaRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Categorium> _collection;

        public CategoriaRepository(IConfiguration configurations)
        {
            var mongoClient = new MongoClient(configurations.GetConnectionString("mongoDb"));
            var mongoDatabase = mongoClient.GetDatabase(configurations.GetConnectionString("mongoDbNombre"));
            _collection = mongoDatabase.GetCollection<Categorium>("Categorias");
        }
        public async Task<List<Categorium>> ObtenerTodosAsync()
        {
            List<Categorium> lista;
           
            lista = (await _collection.FindAsync(_ => true)).ToList();

            return lista;
        }
    }
}