using AutoMapper;
using Banca.Comun.Dtos;
using Banco.Repositorios.Entities;
using Banco.Repositorios.Interfaces;

namespace Banca.BusinessLayer.Bl
{
    //public interface IUnitOfWork
    //{
    //    public ICuentaBl
    //}


    public class UnitOfWork
    {
        public UnitOfWork(CuentaBl cuentaBl)
        {
            Cuenta = cuentaBl;
        }

        public CuentaBl Cuenta { get; }
    }

    public class CuentaBl
    {
        private readonly IRepositorio _repositorio;
        private readonly IMapper _mapper;

        public CuentaBl(IRepositorio repositorio, IMapper mapper) {
            this._repositorio = repositorio;
            this._mapper = mapper;
        }

        public async Task<int> AgregarAsync(CuentaDtoIn cuenta)
        {
            Cuentum entity;

            entity = _mapper.Map<Cuentum>(cuenta);
            await _repositorio.Cuenta.AgregarAsync(entity);

            return entity.Id;
        }

        public async Task<List<CuentaDto>> Obtener()
        {
            List<CuentaDto> dtos;
            List<Cuentum> entidades;

            entidades = await _repositorio.Cuenta.ObtenerAsync();
            dtos = _mapper.Map<List<CuentaDto>>(entidades); 

            return dtos;
        }
    }
}
