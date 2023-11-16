using AutoMapper;
using Banca.Comun.Dtos;
using Banco.Repositorios.Entities;

namespace Banca.BusinessLayer.Mappers
{
    public class BancaMapper: Profile
    {
        public BancaMapper() {
            CreateMap<CuentaDtoIn, Cuentum>();
            CreateMap<Cuentum, CuentaDto>();
        }
    }
}
