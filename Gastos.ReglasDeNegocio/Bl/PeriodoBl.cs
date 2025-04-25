using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class PeriodoBl
    {
        private readonly Repositorio _repositorioMongo;

        public PeriodoBl(Repositorio gastosRepository)        
        {
            _repositorioMongo = gastosRepository;
        }

        public async Task<List<PeriodoDto>> ObtenerTodosAsync()
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

        public async Task<PeriodoDto> ObtenerAsync(string periodoId)
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
                VersionId = x.VersionId                
            };

            return dto;
        }
               
        public async Task<IdDto> AgregarAsync(PeriodoDtoIn periodo)
        {
            Periodo entity;

            if (periodo.Guid == null)
                periodo.Guid = Guid.NewGuid().ToString();
            entity = periodo.ToEntity();
            //entity.Version = await _repositorioMongo.Version.ObtenerAsync(entity.VersionId.ToString());
            await _repositorioMongo.Periodo.AgregarAsync(entity);

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }
    }
}
