using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Entities;
using Banca.Core.Dtos;
using Banco.Repositorios.Entities;

namespace Banca.BusinessLayer.Mappers
{
    public class BancaMapper: Profile
    {
        public BancaMapper() {
            CreateMap<TipoDeCuenta,TipoDeCuentaDto>(); 

            CreateMap<AhorroDtoIn, Cuentum>();
            CreateMap<Cuentum, AhorroDto>();

            CreateMap<DepositoDtoIn, Transaccion>();
            CreateMap<RetiroDtoIn, Transaccion>();

            CreateMap<HistorialDtoIn, HistorialDeApartado>();
            CreateMap<HistorialDeApartado, HistorialDto>();

            CreateMap<SubcategoriaDtoIn, Subcategorium>();
            CreateMap<Subcategorium, SubcategoriaDto>();

            CreateMap<Categorium, CategoriaDto>();

            CreateMap<VersionDePresupuesto,VersionDto>();
            CreateMap<VersionDtoIn,VersionDePresupuesto>();

            CreateMap<Presupuesto, PresupuestoDto>();
            CreateMap<PresupuestoDtoIn, Presupuesto>();

            CreateMap<Transaccion, TransaccionDto>();

            CreateMap<PeriodoDtoIn, Periodo>();

            CreateMap<Movimiento, MovimientoDto>();
        }
    }
}
