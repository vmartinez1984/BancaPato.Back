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
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
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