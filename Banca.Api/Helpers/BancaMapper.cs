using AutoMapper;
using Banca.Api.Dtos;
using Banca.Core.Dtos;
using Banco.Repositorios.Entities;
using DuckBank.Persistence.Entities;
using Gastos.ReglasDeNegocio.Entities;

namespace Banca.BusinessLayer.Mappers
{
    public class BancaMapper: Profile
    {
        public BancaMapper() {
            CreateMap<TipoDeCuenta,TipoDeAhorroDto>(); 
            
            CreateMap<DepositoDtoIn, Transaccion>();
            CreateMap<RetiroDtoIn, Transaccion>();

            CreateMap<HistorialDtoIn, HistorialDeApartado>();
            CreateMap<HistorialDeApartado, HistorialDto>();

            CreateMap<SubcategoriaDtoIn, Subcategoria>();
            CreateMap<Subcategoria, SubcategoriaDto>();

            CreateMap<Categoria, CategoriaDto>();

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
