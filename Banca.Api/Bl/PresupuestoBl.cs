using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;
using Banco.Repositorios.Entities;
using MongoDB.Bson;
using SharpCompress.Common;

namespace Banca.Api.Bl
{
    public class PresupuestoBl : BaseBl
    {
        public PresupuestoBl(IMapper mapper, IGastosRepository gastosRepository) 
        : base(mapper, gastosRepository)
        {
        }

        internal async Task ActualizarAsync(string versionIdGuid, string presupuestoIdGuid, PresupuestoDtoIn presupuesto)
        {

            Presupuesto entity;
            VersionDePresupuesto version;
            List<Subcategorium> subcategorias;
            int presupuestoId = 0;
    
            version = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            if (int.TryParse(presupuestoIdGuid, out presupuestoId))
                entity = version.Presupuestos.Where(x => x.Id == presupuestoId).FirstOrDefault();
            else
                entity = version.Presupuestos.Where(x => x.Guid == presupuestoIdGuid).FirstOrDefault();
            subcategorias = await _repositorioMongo.Subcategoria.ObtenerTodosAsync();
            entity.AhorroId = presupuesto.AhorroId;
            entity.AhorroTipo = presupuesto.AhorroTipo;
            entity.Cantidad = presupuesto.Cantidad;
            entity.SubcategoriaId = presupuesto.SubcategoriaId;
            entity.Subcategoria = subcategorias.FirstOrDefault(x => x.Id == presupuesto.SubcategoriaId);
            await _repositorioMongo.Version.ActualizarAsync(version);
        }

        internal async Task<IdDto> AgregarAsync(string versionIdGuid, PresupuestoDtoIn presupuesto)
        {
            Presupuesto entity;
            VersionDePresupuesto version;
            List<Subcategorium> subcategorias;

            if(presupuesto.Guid == null)
                presupuesto.Guid = Guid.NewGuid().ToString();
            entity = _mapper.Map<Presupuesto>(presupuesto);
            subcategorias = await _repositorioMongo.Subcategoria.ObtenerTodosAsync();
            entity.Subcategoria = subcategorias.FirstOrDefault(x => x.Id == entity.SubcategoriaId);
            version = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            entity.Id = version.Presupuestos.Count() + 1;
            entity._id = ObjectId.GenerateNewId().ToString();
            version.Presupuestos.Add(entity);
            await _repositorioMongo.Version.ActualizarAsync(version);

            return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
        }

        internal async Task BorrarAsync(string versionIdGuid, string presupuestoIdGuid)
        {
            VersionDePresupuesto version;
            Presupuesto presupuesto;            
            int presupuestoId = 0;

            version = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            if (int.TryParse(presupuestoIdGuid, out presupuestoId))
                presupuesto = version.Presupuestos.Where(x => x.Id == presupuestoId).FirstOrDefault();
            else
                presupuesto = version.Presupuestos.Where(x => x.Guid == presupuestoIdGuid).FirstOrDefault();
            version.Presupuestos.Remove(presupuesto);

            await _repositorioMongo.Version.ActualizarAsync(version);
        }
    }    
}
