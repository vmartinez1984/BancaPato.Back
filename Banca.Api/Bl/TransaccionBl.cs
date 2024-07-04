using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class TransaccionBl : BaseBl
    {
        public TransaccionBl(DuckBankContext context, IMapper mapper,IGastosRepository gastosRepository) 
        : base(context, mapper, gastosRepository)
        {
        }

        internal async Task<int> Depositar(string idGuid, DepositoDtoIn deposito)
        {
            Transaccion transaccion;
            Cuentum cuentum;
            if(deposito.Guid == null)
                deposito.Guid = Guid.NewGuid();
            transaccion = _mapper.Map<Transaccion>(deposito);
            transaccion.Tipo = Tipo.Deposito;
            transaccion.CuentaId = ObtenerAhorroId(idGuid);
            _repositorio.Transaccion.Add(transaccion);
            cuentum = await _repositorio.Cuenta.Where(x=> x.Id == transaccion.CuentaId).FirstOrDefaultAsync();
            cuentum.Balance = cuentum.Balance == null ? 0 : cuentum.Balance;            
            cuentum.Balance += transaccion.Cantidad;
            await ActualizarAhorroPadre(cuentum, deposito);
            await _repositorio.SaveChangesAsync();

            return transaccion.Id;
        }

        private async Task ActualizarAhorroPadre(Cuentum cuenta, DepositoDtoIn deposito)
        {
            if (cuenta.CuentaDeReferenciaId != null)
            {
                Cuentum ahorroPadre = await _repositorio.Cuenta.FindAsync(cuenta.CuentaDeReferenciaId);

                await Depositar(ahorroPadre.Id.ToString(), deposito);
            }
        }

        internal async Task<int> Retirar(string idGuid, RetiroDtoIn retiro)
        {
            Transaccion transaccion;
            Cuentum cuentum;

            if (retiro.Guid == null)
                retiro.Guid = Guid.NewGuid();
            await VerificarFondos(idGuid, retiro.Cantidad);
            transaccion = _mapper.Map<Transaccion>(retiro);
            transaccion.Tipo = Tipo.Retiro;
            transaccion.CuentaId = ObtenerAhorroId(idGuid);
            _repositorio.Transaccion.Add(transaccion);
            cuentum = await _repositorio.Cuenta.Where(x => x.Id == transaccion.CuentaId).FirstOrDefaultAsync();
            await ActualizarAhorroPadre(cuentum, retiro);
            cuentum.Balance -= transaccion.Cantidad;
            await _repositorio.SaveChangesAsync();

            return transaccion.Id;
        }

        private async Task ActualizarAhorroPadre(Cuentum cuenta, RetiroDtoIn retiro)
        {
            if(cuenta.CuentaDeReferenciaId != null)
            {
                Cuentum ahorroPadre = await _repositorio.Cuenta.FindAsync(cuenta.CuentaDeReferenciaId);

                await Retirar(ahorroPadre.Id.ToString(), retiro);
            }
        }

        private async Task VerificarFondos(string idGuid, decimal cantidad)
        {
            Cuentum cuenta;

            cuenta = await _repositorio.Cuenta.Where(x => x.Id == ObtenerAhorroId(idGuid)).FirstOrDefaultAsync();
            if(cuenta.Balance >= cantidad)
            { }
            else
            {
                throw new Exception("No hay suficientes fondos");
            }
        }

        private int ObtenerAhorroId(string idGuid)
        {
            return int.Parse(idGuid);
        }

        internal async Task<List<TransaccionDto>> ObtenerPorAhorroId(string ahorroId)
        {
            List<Transaccion> entities;
            List<TransaccionDto> dtos;

            entities = await _repositorio.Transaccion.Where(x => x.CuentaId == ObtenerAhorroId(ahorroId)).ToListAsync();
            dtos = _mapper.Map<List<TransaccionDto>>(entities);

            return dtos;
        }
    }
}