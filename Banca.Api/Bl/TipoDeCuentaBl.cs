using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class TipoDeCuentaBl : BaseBl
    {
        public TipoDeCuentaBl(DuckBankContext context, IMapper mapper, IGastosRepository gastosRepository) 
        : base(context, mapper, gastosRepository)
        {
        }

        internal async Task<List<TipoDeCuentaDto>> ObtenerTodosAsync()
        {
            List<TipoDeCuentaDto> dtos;
            //List<TipoDeCuenta> entitites;

            dtos = await _repositorio.TipoDeCuenta.Select(x => new TipoDeCuentaDto { Id = x.Id, Nombre = x.Nombre }).ToListAsync();

            return dtos;
        }
    }
}
