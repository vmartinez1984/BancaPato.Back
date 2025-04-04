using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;

namespace Gastos.ReglasDeNegocio.Helpers
{
    internal static class Mapeador
    {
        public static TipoDeCuentaDto ToDto(this TipoDeCuenta entidad) => entidad is null ? null : new TipoDeCuentaDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        public static List<TipoDeCuentaDto> ToDtos(this List<TipoDeCuenta> entidades) => entidades.Select(x=> x.ToDto()).ToList();
    }
}
