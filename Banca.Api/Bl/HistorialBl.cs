﻿using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;

namespace Banca.Api.Bl
{
    public class HistorialBl : BaseBl
    {
        public HistorialBl(IMapper mapper, IGastosRepository gastosRepository)
        : base(mapper, gastosRepository)
        {
        }

        internal Task<IdDto> AgregarAsync(HistorialDtoIn historial)
        {
            throw new NotImplementedException();
        }

        internal Task<List<HistorialDto>> Obtener(string ahorroId = null)
        {
            throw new NotImplementedException();
        }

        private int ObtenerId(string ahorroId)
        {
            return int.Parse(ahorroId);
        }
    }
}
