using AutoMapper;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
using DuckBank.Persistence.Entities;
using Ahorro = DuckBank.Persistence.Entities.Ahorro;

namespace Banca.Api.Bl
{
    public class TransaccionBl : BaseBl
    {
        public TransaccionBl(IMapper mapper,IGastosRepository gastosRepository) 
        : base(mapper, gastosRepository)
        {
        }

        internal async Task<string> Depositar(string idGuid, DepositoDtoIn deposito)
        {
            if(deposito.Referencia == null)
                deposito.Referencia = Guid.NewGuid();
            
            await _repositorioMongo.Ahorro.DepositarAsync(idGuid,new Movimiento
            {
                Cantidad = deposito.Cantidad,
                Concepto = deposito.Concepto,
                EncodedKey = deposito.Referencia.ToString(),
                FechaDeRegistro = DateTime.Now
            });

            return deposito.Referencia.ToString();
        }

        internal async Task<string> Retirar(string idGuid, RetiroDtoIn retiro)
        { 
            Ahorro ahorro;

            if (retiro.Guid == null)
                retiro.Guid = Guid.NewGuid().ToString();
            ahorro = await _repositorioMongo.Ahorro.ObtenerPorIdAsync(idGuid);
            if(retiro.Cantidad > ahorro.Total)
                throw new Exception("No hay suficientes fondos");            
            await _repositorioMongo.Ahorro.RetirarAsync(idGuid, new Movimiento
            {
                Cantidad = retiro.Cantidad,
                Concepto = retiro.Concepto,
                EncodedKey = retiro.Guid,
                FechaDeRegistro = DateTime.Now
            });

            return retiro.Guid;
        }        
    }
}