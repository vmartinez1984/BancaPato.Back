using AutoMapper;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;
using Banco.Repositorios.Entities;

namespace Banca.Api.Bl
{
    public class PeriodoBl : BaseBl
    {
        public PeriodoBl(IMapper mapper, IGastosRepository gastosRepository)
        : base(mapper, gastosRepository)
        {
        }

        internal async Task<List<PeriodoDto>> ObtenerTodosAsync()
        {
            List<PeriodoDto> dtos;

            dtos = (await _repositorioMongo.Periodo.ObtenerAsync())
                .Select(x => new PeriodoDto
                {
                    Id = x.Id,
                    FechaFinal = x.FechaFinal,
                    FechaInicial = x.FechaInicial,
                    Guid = x.Guid,
                    Nombre = x.Nombre,
                    VersionId = x.VersionId,
                }
            ).ToList();

            return dtos;
        }

        internal async Task<PeriodoDto> ObtenerAsync(string periodoId)
        {
            Periodo x;
            PeriodoDto dto;

            x = await _repositorioMongo.Periodo.ObtenerAsync(periodoId);

            dto = new PeriodoDto
            {
                Id = x.Id,
                FechaFinal = x.FechaFinal,
                FechaInicial = x.FechaInicial,
                Guid = x.Guid,
                Nombre = x.Nombre,
                VersionId = x.VersionId,
                Version = new VersionDto
                {
                    Id = x.VersionId,
                    FechaFinal = x.Version.FechaFinal,
                    Guid = x.Version.Guid,
                    Nombre = x.Version.Nombre,
                    FechaInicial = x.Version.FechaInicial,
                    Presupuestos = x.Version.Presupuestos.Select(x => new PresupuestoDto
                    {
                        Cantidad = x.Cantidad,
                        Id = x.Id,
                        AhorroId = x.AhorroId,
                        AhorroTipo = x.AhorroTipo,                        
                        Guid = x.Guid,
                        Movimientos = x.Movimientos.ToDtos(),
                        Subcategoria = x.Subcategoria.ToDto(),
                        SubcategoriaId = x.SubcategoriaId,
                        VersionId = x.VersionId
                    }).ToList(),
                }
            };

            return dto;
        }
               
        public async Task<IdDto> AgregarAsync(PeriodoDtoIn periodo)
        {
            Periodo entity;

            if (periodo.Guid == null)
                periodo.Guid = Guid.NewGuid().ToString();
            entity = _mapper.Map<Periodo>(periodo);
            entity.Version = await _repositorioMongo.Version.ObtenerAsync(entity.VersionId.ToString());
            await _repositorioMongo.Periodo.AgregarAsync(entity);

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }
    }

    public static class Mapeador
    {
        public static List<MovimientoDto> ToDtos(this List<Movimiento> movimientos) 
        => movimientos.Select(x => new MovimientoDto
        {
             Id = x.Id,
             Cantidad = x.Cantidad,
             FechaDeRegistro = x.FechaDeRegistro,
             Guid = x.Guid             
        }).ToList();
        

        public static CategoriaDto ToDto(this Categorium categorium) => new CategoriaDto
        {
            Id = categorium.Id,
            Nombre = categorium.Nombre,
        };

        public static SubcategoriaDto ToDto(this Subcategorium subcategorium)
        {
            return new SubcategoriaDto
            {
                Id = subcategorium.Id,
                EsPrimario = subcategorium.EsPrimario,
                EstaActivo = subcategorium.EstaActivo,
                Guid = Guid.Parse(subcategorium?.Guid),
                Nombre = subcategorium?.Nombre,
                Presupuesto = subcategorium.Presupuesto,
                Categoria = subcategorium.Categoria.ToDto()
            };
        }
    }
}
