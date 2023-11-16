using AutoMapper;
using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.Api.Bl
{
    public class TransaccionBl : BaseBl
    {
        public TransaccionBl(DuckBankContext context, IMapper mapper) : base(context, mapper)
        {
        }

        internal async Task<int> Agregar(DepositoDtoIn deposito, string idGuid)
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
            await _repositorio.SaveChangesAsync();

            return transaccion.Id;
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
            cuentum.Balance -= transaccion.Cantidad;
            await _repositorio.SaveChangesAsync();

            return transaccion.Id;
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