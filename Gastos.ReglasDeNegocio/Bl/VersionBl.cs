using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class VersionBl //: BaseBl
    {
        private readonly Repositorio _repositorioMongo;

        public VersionBl(Repositorio repositorio)
        {
            _repositorioMongo = repositorio;
        }

        public async Task ActualizarAsync(string versionIdGuid, VersionDtoIn version)
        {
            VersionDePresupuesto entity;

            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);            
            entity.FechaInicial = version.FechaInicial;
            entity.FechaFinal = version.FechaFinal;
            entity.Nombre = version.Nombre;

            await _repositorioMongo.Version.ActualizarAsync(entity);
        }

        public async Task<IdDto> AgregarAsync(VersionDtoIn version)
        {
            int id;

            if (version.Guid == null)
                version.Guid = Guid.NewGuid().ToString();            
           id = await _repositorioMongo.Version.AgregarAsync(version.ToEntity());

            return new IdDto { Guid = version.Guid.ToString(), Id = id };
        }

        public async Task<List<VersionDto>> ObtenerAsync()
        {            
            List<VersionDePresupuesto> entities;

            entities = await _repositorioMongo.Version.ObtenerTodosAsync();            

            return entities.ToDtos();
        }

        public async Task BorrarAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;

            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            entity.EstaActivo = false;

            await _repositorioMongo.Version.ActualizarAsync(entity);
        }

        internal async Task<VersionDto> ObtenerAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;         

            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);

            return entity.ToDto();
        }

        public async Task<List<PresupuestoDto>> ObtenerPresupuestosAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;
            List<PresupuestoDto> lista;
            
            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            lista = entity.Presupuestos.ToDtos();

            return lista;
        }

        public async Task<VersionDto> ObtenerPorIdAsync(int versionId)
        {
            VersionDePresupuesto entity;

            entity = await _repositorioMongo.Version.ObtenerAsync(versionId.ToString());

            return entity.ToDto();
        }

        public async Task<IdDto> AgregarPresupuestoAsync(string versionIdGuid, PresupuestoDtoIn presupuesto)
        {
            Presupuesto entity;
            VersionDePresupuesto version;            

            if (presupuesto.Guid == null)
                presupuesto.Guid = Guid.NewGuid().ToString();
            entity = presupuesto.ToEntity();

            entity.Subcategoria = await _repositorioMongo.Subcategoria.ObtenerPorIdAsync(entity.SubcategoriaId);             
            version = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            entity.Id = version.Presupuestos.Count() + 1;
            entity._id = ObjectId.GenerateNewId().ToString();
            version.Presupuestos.Add(entity);
            await _repositorioMongo.Version.ActualizarAsync(version);

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }
    }
}
