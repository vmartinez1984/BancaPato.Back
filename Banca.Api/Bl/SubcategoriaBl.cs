using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;

namespace Banca.Api.Bl
{
    public class SubcategoriaBl : BaseBl
    {
        public SubcategoriaBl(DuckBankContext context, IMapper mapper, IGastosRepository repository)
        : base(context, mapper, repository)
        {
        }

        internal async Task ActualizarAsync(SubcategoriaDtoIn subcategoria, string idGuid)
        {
            Subcategorium subcategorium;

            subcategorium = await _repositorioMongo.Subcategoria.ObtenerAsync(idGuid);
            subcategoria.Guid = subcategorium?.Guid;
            subcategorium = _mapper.Map(subcategoria, subcategorium);
            
            await _repositorioMongo.Subcategoria.ActualizarAsync(subcategorium);
        }

        internal async Task<IdDto> AgregarAsync(SubcategoriaDtoIn subcategoria)
        {
            Subcategorium subcategorium;
            if (subcategoria.Guid == null)
                subcategoria.Guid = Guid.NewGuid().ToString();
            subcategorium = _mapper.Map<Subcategorium>(subcategoria);
            await AgregarAMongoDbAsync(subcategorium);

            return new IdDto { Id = subcategorium.Id, Guid = subcategorium.Guid };
        }

        private async Task AgregarAMongoDbAsync(Subcategorium subcategoria)
        {
            var categorias = await _repositorioMongo.Categoria.ObtenerTodosAsync();
            var categoria = categorias.FirstOrDefault(x => x.Id == subcategoria.CategoriaId);
            subcategoria.Categoria = categoria;

            await _repositorioMongo.Subcategoria.AgregarAsync(subcategoria);
        }

        internal Task BorrarAsync(string idGuid)
        {
            throw new NotImplementedException();
        }

        internal async Task<List<SubcategoriaDto>> ObtenerAsync()
        {
            List<Subcategorium> entities;
            List<SubcategoriaDto> subcategorias;

            entities = await _repositorioMongo.Subcategoria.ObtenerTodosAsync();
            subcategorias = _mapper.Map<List<SubcategoriaDto>>(entities);

            return subcategorias;
        }

        internal async Task<SubcategoriaDto> ObtenerAsync(string idGuid)
        {
            return _mapper.Map<SubcategoriaDto>(await _repositorioMongo.Subcategoria.ObtenerAsync(idGuid));
        }

        private int ObtenerId(string idGuid)
        {
            return int.Parse(idGuid);
        }
    }
}
