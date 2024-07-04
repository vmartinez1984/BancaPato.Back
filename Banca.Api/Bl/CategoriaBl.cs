using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;

namespace Banca.Api.Bl
{
    public class CategoriaBl : BaseBl
    {
       

        public CategoriaBl(DuckBankContext context, IMapper mapper, IGastosRepository repository) 
        : base(context, mapper, repository)
        {            
        }

        internal async Task<List<CategoriaDto>> ObtenerAsync()
        {
            var entidades = await _repositorioMongo.Categoria.ObtenerTodosAsync();

            return _mapper.Map<List<CategoriaDto>>(entidades);
        }
    }
}