using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class PresupuestoBl : BaseBl
    {
        public PresupuestoBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
        {
        }

        internal Task ActualizarAsync(string versionIdGuid, string presupuestoIdGuid, PresupuestoDtoIn presupuesto)
        {
            throw new NotImplementedException();
        }

        internal async Task<IdDto> AgregarAsync(string versionIdGuid, PresupuestoDtoIn presupuesto)
        {
            Presupuesto entity;

            if(presupuesto.Guid == null)
                presupuesto.Guid = Guid.NewGuid();
            entity = _mapper.Map<Presupuesto>(presupuesto);
            entity.VersionId = ObtenerIdAsync(versionIdGuid);
            await _repositorio.Presupuesto.AddAsync(entity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Id = entity.Id, Guid = entity.Guid };
        }

        internal Task BorrarAsync(string versionIdGuid, string presupuestoIdGuid)
        {
            throw new NotImplementedException();
        }

        internal async Task<List<PresupuestoDto>> ObtenerAsync(string versionIdGuid)
        {
            int id;

            id = ObtenerIdAsync(versionIdGuid);
            var entities = await _repositorio.Presupuesto.Include(x => x.Subcategoria).Where(x => x.VersionId == id).ToListAsync();

            return _mapper.Map<List<PresupuestoDto>>(entities);
        }

        internal async Task<PresupuestoDto> ObtenerPorIdAsync(string presupuestoIdGuid)
        {
            return _mapper.Map<PresupuestoDto>(
                await _repositorio.Presupuesto
                .Include(x=> x.Subcategoria)
                .ThenInclude(x=> x.Categoria)
                .Where(x=> x.Id == ObtenerIdAsync(presupuestoIdGuid)).FirstOrDefaultAsync()
            );
        }

        private int ObtenerIdAsync(string versionIdGuid)
        {
            return int.Parse(versionIdGuid);

        }
    }    
}
