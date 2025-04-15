using Gastos.ReglasDeNegocio.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gastos.ReglasDeNegocio.Repositories
{
    public class SubcategoriaRepo : BaseRepositorio
    {
        private readonly IMongoCollection<Subcategoria> _collection;

        public SubcategoriaRepo(IConfiguration configurations) : base(configurations)
        {
            _collection = _mongoDatabase.GetCollection<Subcategoria>("Subcategorias");
        }

        public async Task ActualizarAsync(Subcategoria subcategorium) => await _collection.ReplaceOneAsync(x => x._id == subcategorium._id, subcategorium);


        public async Task<int> AgregarAsync(Subcategoria subcategoria)
        {
            subcategoria.Id = await ObtenerId();

            await _collection.InsertOneAsync(subcategoria);

            return subcategoria.Id;
        }

        public async Task<Subcategoria> ObtenerAsync(string idGuid)
        {
            int id;

            if (int.TryParse(idGuid, out id))
                return (await _collection.FindAsync(x => x.Id == id)).FirstOrDefault();
            else
                return (await _collection.FindAsync(x => x.Guid == idGuid)).FirstOrDefault();
        }

        public async Task<List<Subcategoria>> ObtenerTodosAsync()
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

            return item.Id + 1;
        }
    }
}
