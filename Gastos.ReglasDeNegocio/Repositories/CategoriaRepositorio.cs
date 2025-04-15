using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class CategoriaRepositorio
    {
        private readonly IMongoCollection<Categoria> _collection;

        public CategoriaRepositorio(IConfiguration configurations)
        {
            var conectionString = configurations.GetConnectionString("MongoDb");
            var mongoClient = new MongoClient(conectionString);
            var nombreDeLaDb = conectionString.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(nombreDeLaDb);
            _collection = mongoDatabase.GetCollection<Categoria>("Categorias");
        }

        public async Task<List<Categoria>> ObtenerTodosAsync() => await _collection.Find(_ => true).ToListAsync();
    }
}
