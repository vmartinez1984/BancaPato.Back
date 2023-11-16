using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class VersionBl : BaseBl
    {
        public VersionBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
        {
        }

        internal async Task ActualizarAsync(string versionIdGuid, VersionDtoIn version)
        {
            int id;
            VersionDePresupuesto entity;

            id = ObtenerId(versionIdGuid);
            entity = await _repositorio.VersionDePresupuesto.Where(x => x.Id == id).FirstOrDefaultAsync();
            version.Guid = entity.Guid;
            entity = _mapper.Map(version, entity);
            _repositorio.VersionDePresupuesto.Update(entity);
            await _repositorio.SaveChangesAsync();
        }

        private int ObtenerId(string versionIdGuid)
        {
            return int.Parse(versionIdGuid);
        }

        internal async Task<IdDto> AgregarAsync(VersionDtoIn version)
        {
            VersionDePresupuesto entity;

            if (version.Guid == null)
                version.Guid = Guid.NewGuid();
            entity = _mapper.Map<VersionDePresupuesto>(version);
            _repositorio.VersionDePresupuesto.Add(entity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Guid = entity.Guid, Id = entity.Id };
        }

        internal async Task<List<VersionDto>> ObtenerAsync()
        {
            return _mapper.Map<List<VersionDto>>(await _repositorio.VersionDePresupuesto.Where(x => x.EstaActivo).ToListAsync());
        }

        internal async Task BorrarAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;

            entity = await _repositorio.VersionDePresupuesto.Where(x => x.Id == ObtenerId(versionIdGuid)).FirstOrDefaultAsync();
            entity.EstaActivo = false;
            _repositorio.VersionDePresupuesto.Update(entity);
            await _repositorio.SaveChangesAsync();
        }
    }
}
