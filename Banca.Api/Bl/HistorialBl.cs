using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class HistorialBl : BaseBl
    {
        public HistorialBl(DuckBankContext context, IMapper mapper, IGastosRepository gastosRepository) 
        : base(context, mapper, gastosRepository)
        {
        }

        internal async Task<IdDto> AgregarAsync(HistorialDtoIn historial)
        {
            HistorialDeApartado entity;

            if (historial.Guid == null)
                historial.Guid = Guid.NewGuid();
            entity = _mapper.Map<HistorialDeApartado>(historial);
            _repositorio.HistorialDeApartados.Add(entity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Id = entity.Id, Guid = entity.Guid };
        }

        internal async Task<List<HistorialDto>> Obtener(string ahorroId = null)
        {
            List<HistorialDeApartado> entities;
            List<HistorialDto> dtos;

            if (string.IsNullOrEmpty(ahorroId))
                entities = await _repositorio.HistorialDeApartados.Include(x=> x.Cuenta).ToListAsync();
            else
                entities = await _repositorio.HistorialDeApartados.Include(x => x.Cuenta).Where(x => x.CuentaId == ObtenerId(ahorroId)).ToListAsync();
            dtos = _mapper.Map<List<HistorialDto>>(entities);

            return dtos;
        }

        private int ObtenerId(string ahorroId)
        {
            return int.Parse(ahorroId);
        }
    }
}
