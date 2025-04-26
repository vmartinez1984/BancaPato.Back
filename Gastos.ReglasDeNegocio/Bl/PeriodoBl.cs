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
            int periodoId;
            Periodo entity;
            List<Presupuesto> presupuestos;

            if (periodo.Guid == null)
                periodo.Guid = Guid.NewGuid().ToString();
            entity = periodo.ToEntity();
            presupuestos = await _repositorioMongo.Presupuesto.ObtenerPorVersionIdAsync(periodo.VersionId);
            periodoId = await _repositorioMongo.Periodo.AgregarAsync(entity);
            foreach (var presupuesto in presupuestos)
            {
                await _repositorioMongo.PresupuestoDelPeriodo.AgregarAsync(new PresupuestoDelPeriodo
                {
                    AhorroId = presupuesto.AhorroId,
                    Cantidad = presupuesto.Cantidad,
                    EstaActivo = true,
                    Gastado = 0,
                    PeriodoId = periodoId,
                    PresupuestoId = presupuesto.Id
                });
            }

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }

        public async Task<List<PresupuestoDelPeriodoDto>> ObtenerPresupestosDelPeriodoAsync(int periodoId)
        {
            List<PresupuestoDelPeriodo> lista;
            List<PresupuestoDelPeriodoDto> dtos;
            List<Presupuesto> presupestos;
            Periodo peridodo;

            lista = await _repositorioMongo.PresupuestoDelPeriodo.ObtenerPorPeriodoIdAsync(periodoId);
            dtos = lista.ToDtos();
            peridodo = await _repositorioMongo.Periodo.ObtenerAsync(periodoId.ToString());
            presupestos = await _repositorioMongo.Presupuesto.ObtenerPorVersionIdAsync(peridodo.VersionId);
            foreach (var item in dtos)
            {
                Presupuesto presupuesto;

                presupuesto = presupestos.Where(x => x.Id == item.PresupuestoId).FirstOrDefault();

                item.SubcategoriaNombre = presupuesto is null? string.Empty: presupuesto.Subcategoria.Nombre;
            }
            return dtos;
        }

        public async Task BorrarAsync(string periodoId)
        {
            Periodo periodo;

            periodo = await _repositorioMongo.Periodo.ObtenerAsync(periodoId);
            periodo.EstaActivo = false;

            await _repositorioMongo.Periodo.ActualizarAsinc(periodo);
        }
    }
}
