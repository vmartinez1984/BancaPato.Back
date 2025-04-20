using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;
using Gastos.ReglasDeNegocio.Entities;

namespace Gastos.ReglasDeNegocio.Helpers
{
    internal static class Mapeador
    {
        public static TipoDeAhorroDto ToDto(this TipoDeCuenta entidad) => entidad is null ? null : new TipoDeAhorroDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        public static List<TipoDeAhorroDto> ToDtos(this List<TipoDeCuenta> entidades) => entidades.Select(x=> x.ToDto()).ToList();

        public static CategoriaDto ToDto(this Categoria entidad) => entidad is null ? null : new CategoriaDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        public static List<CategoriaDto> ToDtos(this List<Categoria> entidades) => entidades.Select ( x=> x.ToDto()).ToList();

        public static Categoria ToEntity(this CategoriaDto dto) => new Categoria
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
        };

        public static Subcategoria ToEntity(this SubcategoriaDtoIn dto) => new Subcategoria
        {            
            EsPrimario = dto.EsPrimario,
            CategoriaId = dto.CategoriaId,            
            Guid = dto.Guid.ToString(),            
            Nombre = dto.Nombre,
            Presupuesto = dto.Presupuesto
        };

        public static SubcategoriaDto ToDto(this Subcategoria entity) => entity is null ? null : new SubcategoriaDto
        {
            Id = entity.Id,
            CategoriaId = entity.CategoriaId,
            EsPrimario = entity.EsPrimario,
            Guid = entity.Guid,
            Nombre = entity.Nombre,
            Presupuesto = entity.Presupuesto
        };

        public static List<SubcategoriaDto> ToDtos(this List<Subcategoria> entidades)=> entidades.Select(x=> x.ToDto()).ToList();
    }
}
