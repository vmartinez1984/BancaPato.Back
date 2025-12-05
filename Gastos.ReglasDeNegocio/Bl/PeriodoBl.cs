using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;
using DuckBank.Persistence.Interfaces;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class PeriodoBl
    {
        private readonly Repositorio _repositorioMongo;
        private readonly IRepositorio _repositorio;

        public PeriodoBl(Repositorio gastosRepository, IRepositorio repositorio)
        {
            _repositorioMongo = gastosRepository;
            _repositorio = repositorio;
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

            return dtos.OrderByDescending(x => x.Id).ToList();
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
                    PresupuestoId = presupuesto.Id,

                });
            }

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }

        public async Task<List<PresupuestoDelPeriodoDto>> ObtenerPresupestosDelPeriodoAsync(int periodoId)
        {
            List<PresupuestoDelPeriodo> lista;
            List<PresupuestoDelPeriodoDto> dtos;
            List<Presupuesto> presupestos;
            List<TipoDeCuenta> tipoDeAhorros;
            Periodo peridodo;


            lista = await _repositorioMongo.PresupuestoDelPeriodo.ObtenerPorPeriodoIdAsync(periodoId);
            dtos = lista.ToDtos();
            peridodo = await _repositorioMongo.Periodo.ObtenerAsync(periodoId.ToString());
            presupestos = await _repositorioMongo.Presupuesto.ObtenerPorVersionIdAsync(peridodo.VersionId);
            tipoDeAhorros = await _repositorioMongo.TipoDeAhorro.ObtenerTodosAsync();
            foreach (var item in dtos)
            {
                Presupuesto presupuesto;

                presupuesto = presupestos.Where(x => x.Id == item.PresupuestoId).FirstOrDefault();
                if (presupuesto.Subcategoria is null)
                {
                    presupuesto.Subcategoria = await _repositorioMongo.Subcategoria.ObtenerPorIdAsync(presupuesto.SubcategoriaId);
                    await _repositorioMongo.Presupuesto.ActualizarAsync(presupuesto);
                }
                if (presupuesto.AhorroId is not null || string.IsNullOrEmpty(presupuesto.AhorroTipo))
                {
                    Ahorro ahorro;
                    string key = "TipoDeCuentaId";

                    ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(presupuesto.AhorroId.ToString());

                    if (ahorro is not null)
                    {
                        var data = (ahorro.Otros.Where(x => x.Key == key).FirstOrDefault());
                        if (data.Value != null)
                        {
                            item.TipoDeAhorro = ahorro.Otros[key];
                            var tipoDeCuenta = tipoDeAhorros.Where(x => x.Id == int.Parse(item.TipoDeAhorro)).FirstOrDefault();
                            item.TipoDeAhorro = tipoDeCuenta.Nombre;
                        }
                    }
                }
                else
                {
                    item.TipoDeAhorro = presupuesto.AhorroTipo;
                }
                item.SubcategoriaNombre = presupuesto is null ? string.Empty : presupuesto.Subcategoria.Nombre;
            }

            return dtos.ToList();
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
