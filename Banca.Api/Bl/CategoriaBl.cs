using AutoMapper;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;

namespace Banca.Api.Bl
{
    public class CategoriaBl : BaseBl
    {
        public CategoriaBl(IMapper mapper, IGastosRepository repository) 
        : base(mapper, repository)
        {            
        }

        internal async Task<List<CategoriaDto>> ObtenerAsync()
        {
            var entidades = await _repositorioMongo.Categoria.ObtenerTodosAsync();

            return _mapper.Map<List<CategoriaDto>>(entidades);
        }
    }
}