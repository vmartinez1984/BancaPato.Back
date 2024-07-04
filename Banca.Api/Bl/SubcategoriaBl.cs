using System.Text.Json.Serialization;
using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

            subcategorium = await _repositorio.Subcategoria.Where(x => x.Id == ObtenerId(idGuid)).FirstOrDefaultAsync();
            subcategoria.Guid = subcategorium?.Guid;
            subcategorium = _mapper.Map(subcategoria, subcategorium);
            _repositorio.Subcategoria.Update(subcategorium);

            await _repositorio.SaveChangesAsync();
        }

        internal async Task<IdDto> AgregarAsync(SubcategoriaDtoIn subcategoria)
        {
            Subcategorium subcategorium;
            if(subcategoria.Guid == null)
                subcategoria.Guid = Guid.NewGuid();
            subcategorium = _mapper.Map<Subcategorium>(subcategoria);
            _repositorio.Subcategoria.Add(subcategorium);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Id =  subcategorium.Id, Guid = subcategorium.Guid };
        }

        internal Task BorrarAsync(string idGuid)
        {
            throw new NotImplementedException();
        }

        internal async Task<List<SubcategoriaDto>> ObtenerAsync()
        {
            List<Subcategorium> entities;
            List<SubcategoriaDto> subcategorias;
            List<Categorium> categorias;

            entities = await _repositorio.Subcategoria
                //.Include(x=> x.Categoria)
                .Where(x => x.EstaActivo).ToListAsync();
            categorias = await _repositorioMongo.Categoria.ObtenerTodosAsync();
            entities.ForEach(x=>{
                var categoria = categorias.FirstOrDefault(categoria => categoria.Id == x.CategoriaId);
                x.Categoria= categoria;
            });
            Console.Write(JsonConvert.SerializeObject(entities));
            subcategorias = _mapper.Map<List<SubcategoriaDto>>(entities);

            return subcategorias;
        }

        internal async Task<SubcategoriaDto> ObtenerAsync(string idGuid)
        {
            return _mapper.Map<SubcategoriaDto>(await _repositorio.Subcategoria.Where(x => x.Id == ObtenerId(idGuid)).FirstOrDefaultAsync());
        }

        private int ObtenerId(string idGuid)
        {
            return int.Parse(idGuid);
        }
    }
}
