using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class CategoriaBl : BaseBl
    {
        public CategoriaBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
        {
        }

        internal async Task<List<CategoriaDto>> ObtenerAsync()
        {
            return _mapper.Map<List<CategoriaDto>>(await _repositorio.Categoria.ToListAsync());
        }
    }

    public class SubcategoriaBl : BaseBl
    {
        public SubcategoriaBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
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

            entities = await _repositorio.Subcategoria
                .Include(x=> x.Categoria)
                .Where(x => x.EstaActivo).ToListAsync();
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
