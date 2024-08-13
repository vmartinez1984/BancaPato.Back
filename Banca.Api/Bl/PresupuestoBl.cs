using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace Banca.Api.Bl
{
    public class PresupuestoBl : BaseBl
    {
        public PresupuestoBl(DuckBankContext context, IMapper mapper, IGastosRepository gastosRepository) 
        : base(context, mapper, gastosRepository)
        {
        }

        internal async Task ActualizarAsync(string versionIdGuid, string presupuestoIdGuid, PresupuestoDtoIn presupuesto)
        {
            throw new NotImplementedException();
            //Presupuesto presupuestoEntity;

            //presupuestoEntity = await _repositorio.Presupuesto.FindAsync(ObtenerIdAsync(presupuestoIdGuid));
            //presupuesto.Guid = presupuestoEntity.Guid;
            //presupuestoEntity = _mapper.Map(presupuesto, presupuestoEntity);
            //_repositorio.Presupuesto.Update(presupuestoEntity);

            //await _repositorio.SaveChangesAsync();
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

        internal Task BorrarAsync(string versionIdGuid, string presupuestoIdGuid)
        {
            throw new NotImplementedException();
        }

        private int ObtenerIdAsync(string versionIdGuid)
        {
            return int.Parse(versionIdGuid);

        }
    }    
}
