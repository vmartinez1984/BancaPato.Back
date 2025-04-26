using Banca.Core.Dtos;
using DuckBank.Persistence.Interfaces;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;
using Microsoft.Extensions.Configuration;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class PresupuestoBl
    {
        private readonly Repositorio _repositorioMongo;        

        public PresupuestoBl(Repositorio repositorio)
        {
            _repositorioMongo = repositorio;            
        }

        public async Task<IdDto> AgregarAsync(PresupuestoDtoIn presupuesto)
        {
            Presupuesto entity;

            if (presupuesto.Guid == null)
                presupuesto.Guid = Guid.NewGuid().ToString();
            entity = presupuesto.ToEntity();
            await _repositorioMongo.Presupuesto.AgregarAsync(entity);
            
            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }

        public async Task<List<PresupuestoDto>> ObtenerTodosAsync(int versionId)
            => (await _repositorioMongo.Presupuesto.ObtenerPorVersionIdAsync(versionId)).ToDtos();

        public async Task<PresupuestoDto> ObtenerAsync(int presupuestoId) => (await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId)).ToDto();

        public async Task ActualizarAsync(int presupuestoId, PresupuestoDtoIn presupuesto)
        {
            Presupuesto presupuesto1;

            presupuesto1 = await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId);
            presupuesto1.AhorroId = presupuesto.AhorroId;
            presupuesto1.Cantidad = presupuesto.Cantidad;
            presupuesto1.VersionId = presupuesto.VersionId;
            presupuesto1.SubcategoriaId = presupuesto.SubcategoriaId;

            await _repositorioMongo.Presupuesto.ActualizarAsync(presupuesto1);
        }

        public async Task BorrarPresupuestoAsync(int presupuestoId)
        {
            Presupuesto presupuesto1;

            presupuesto1 = await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId);
            presupuesto1.EstaActivo = false;

            await _repositorioMongo.Presupuesto.ActualizarAsync(presupuesto1);
        }
    }
}
