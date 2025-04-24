using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class SubcategoriaBl
    {
        private readonly Repositorio _repositorioMongo;

        public SubcategoriaBl(Repositorio repositorio)        
        {
            _repositorioMongo = repositorio;
        }

        public async Task ActualizarAsync(SubcategoriaDtoIn subcategoria, string idGuid)
        {
            Subcategoria subcategoria1;

            subcategoria1 = await _repositorioMongo.Subcategoria.ObtenerAsync(idGuid);
            subcategoria1.Nombre = subcategoria.Nombre;
            subcategoria1.Presupuesto = subcategoria.Presupuesto;
            subcategoria1.CategoriaId = subcategoria.CategoriaId;            
            
            await _repositorioMongo.Subcategoria.ActualizarAsync(subcategoria1);
        }

        public async Task<IdDto> AgregarAsync(SubcategoriaDtoIn subcategoria)
        {
            int id;

            if (subcategoria.Guid == null)
                subcategoria.Guid = Guid.NewGuid().ToString();
            
            id = await _repositorioMongo.Subcategoria.AgregarAsync(subcategoria.ToEntity());

            return new IdDto { Id = id, Guid = subcategoria.Guid };
        }

        public async Task BorrarAsync(string idGuid)
        {
            Subcategoria subcategoria;

            subcategoria = await _repositorioMongo.Subcategoria.ObtenerAsync(idGuid);
            subcategoria.EstaActivo = false;

            await _repositorioMongo.Subcategoria.ActualizarAsync(subcategoria);
        }
        
        public async Task<List<SubcategoriaDto>> ObtenerAsync()
        {
            List<Subcategoria> entities;            

            entities = await _repositorioMongo.Subcategoria.ObtenerTodosAsync();            

            return entities.ToDtos();
        }

        public async Task<SubcategoriaDto> ObtenerAsync(string idGuid)=> (await _repositorioMongo.Subcategoria.ObtenerAsync(idGuid)).ToDto();
        
    }
}
