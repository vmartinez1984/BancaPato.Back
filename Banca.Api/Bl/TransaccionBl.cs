using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Entities;
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

        internal async Task<string> Depositar(string idGuid, DepositoDtoIn deposito)
        {
            if(deposito.Referencia == null)
                deposito.Referencia = Guid.NewGuid();
            
            await _repositorioMongo.Ahorro.DepositarAsync(idGuid,new Entities.MovimientoDuckBank
            {
                Cantidad = deposito.Cantidad,
                Concepto = deposito.Concepto,
                Referencia = deposito.Referencia.ToString(),
                FechaDeRegistro = DateTime.Now
            });

            return deposito.Referencia.ToString();
        }

        internal async Task<string> Retirar(string idGuid, RetiroDtoIn retiro)
        { 
            Ahorro ahorro;

            if (retiro.Guid == null)
                retiro.Guid = Guid.NewGuid().ToString();
            ahorro = await _repositorioMongo.Ahorro.ObtenerAsync(idGuid);
            if(retiro.Cantidad > ahorro.Total)
                throw new Exception("No hay suficientes fondos");            
            await _repositorioMongo.Ahorro.RetirarAsync(idGuid, new Entities.MovimientoDuckBank
            {
                Cantidad = retiro.Cantidad,
                Concepto = retiro.Concepto,
                Referencia = retiro.Guid,
                FechaDeRegistro = DateTime.Now
            });

            return retiro.Guid;
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