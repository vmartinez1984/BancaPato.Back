using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class PeriodoBl : BaseBl
    {
        public PeriodoBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
        {
        }

        internal async Task<List<PeriodoDto>> ObtenerTodosAsync()
        {
            List<PeriodoDto> dtos;

            dtos = await _repositorio.Periodo.Where(x => x.EstaActivo)
                .Select(x => new PeriodoDto
                {
                    Id = x.Id,
                    FechaFinal = x.FechaFinal,
                    FechaInicial = x.FechaInicial,
                    Guid = x.Guid,
                    Nombre = x.Nombre,
                    VersionId = x.VersionId,
                }
            ).ToListAsync();

            return dtos;
        }

        internal async Task<PeriodoDto> ObtenerAsync(int periodoId)
        {
            return await _repositorio.Periodo.Where(x => x.Id == periodoId)
                .Select(x => new PeriodoDto
                {
                    Id = x.Id,
                    FechaFinal = x.FechaFinal,
                    FechaInicial = x.FechaInicial,
                    Guid = x.Guid,
                    Nombre = x.Nombre
                }
            ).FirstOrDefaultAsync();
        }

        internal async Task<IdDto> AgregarAsync(PeriodoDtoIn periodo)
        {
            Periodo entity;

            if (periodo.Guid == null)
                periodo.Guid = Guid.NewGuid();
            entity = _mapper.Map<Periodo>(periodo);
            await _repositorio.Periodo.AddAsync(entity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Id = entity.Id, Guid = entity.Guid };
        }
    }
}
