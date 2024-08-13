using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class VersionBl : BaseBl
    {
        public VersionBl(DuckBankContext context, IMapper mapper, IGastosRepository gastosRepository)
        : base(context, mapper, gastosRepository)
        {
        }

        internal async Task ActualizarAsync(string versionIdGuid, VersionDtoIn version)
        {
                        
            VersionDePresupuesto entity;
            
            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            version.Guid = entity.Guid;
            entity = _mapper.Map(version, entity);
            
            throw new NotImplementedException();
        }

        private int ObtenerId(string versionIdGuid)
        {
            return int.Parse(versionIdGuid);
        }

        internal async Task<IdDto> AgregarAsync(VersionDtoIn version)
        {
            VersionDePresupuesto entity;

            if (version.Guid == null)
                version.Guid = Guid.NewGuid().ToString();
            entity = _mapper.Map<VersionDePresupuesto>(version);
            await _repositorioMongo.Version.AgregarAsync(entity);

            return new IdDto { Guid = entity.Guid.ToString(), Id = entity.Id };
        }

        internal async Task<List<VersionDto>> ObtenerAsync()
        {
            List<VersionDto> dtos;
            List<VersionDePresupuesto> entities;

            entities = await _repositorioMongo.Version.ObtenerTodosAsync();
            dtos = _mapper.Map<List<VersionDto>>(entities);

            return dtos;
        }

        internal async Task BorrarAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;

            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            entity.EstaActivo = false;

            await _repositorioMongo.Version.ActualizarAsync(entity);
        }

        internal async Task<VersionDto> ObtenerAsync(string versionIdGuid)
        {
            VersionDePresupuesto entity;
            VersionDto dto;

            entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
            dto = _mapper.Map<VersionDto>(entity);

            return dto;
        }
    }
}
