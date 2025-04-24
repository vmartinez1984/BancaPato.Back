using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;
using DuckBank.Persistence.Interfaces;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;
using System;
using System.Globalization;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class AhorroBl : BaseBl
    {
        private readonly Repositorio _repositorio1;


        const string FechaFinal = "FechaFinal";
        const string FechaInicial = "FechaInicial";

        public AhorroBl(IRepositorio repositorio, Repositorio repositorio1) : base(repositorio)
        {
            _repositorio1 = repositorio1;
        }

        public async Task<IdDto> AgregarAsync(AhorroDtoIn cuenta)
        {
            int id;

            if (cuenta.Guid == null)
                cuenta.Guid = Guid.NewGuid().ToString();
            Ahorro ahorro = await ObtenerAhorroAsync(cuenta);
            id = await _repositorio.Ahorro.AgregarAsync(ahorro);

            return new IdDto { Id = id, Guid = cuenta.Guid };
        }

        private async Task<Ahorro> ObtenerAhorroAsync(AhorroDtoIn cuenta)
        {
            Ahorro ahorro;
            Dictionary<string, string> otros = new Dictionary<string, string>();
            List<TipoDeCuenta> tipos;

            tipos = await _repositorio1.TipoDeAhorro.ObtenerTodosAsync();
            otros.Add("Nota", cuenta.Nota);
            otros.Add("FechaInicial", cuenta.FechaInicial is null? null: cuenta.FechaInicial.ToString());
            otros.Add("FechaFinal", cuenta.FechaFinal is null ? null : cuenta.FechaFinal.ToString());
            otros.Add("TipoDeCuentaId", cuenta.TipoDeAhorroId.ToString());
            ahorro = new Ahorro
            {
                Id = 0,
                Guid = cuenta.Guid.ToString(),
                ClienteEncodedKey = "148318",
                Total = 0,
                Interes = cuenta.Interes,
                Nombre = cuenta.Nombre,
                FechaDeRegistro = DateTime.Now,
                Estado = "Activo",
                Otros = otros
            };

            return ahorro;
        }

        public async Task<List<AhorroDto>> ObtenerAsync()
        {
            List<AhorroDto> dtos;
            List<Ahorro> entidades;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorio1.TipoDeAhorro.ObtenerTodosAsync();
            entidades = await _repositorio.Ahorro.ObtenerAsync();
            dtos = entidades.Select(x => new AhorroDto
            {
                Balance = x.Total,
                FechaFinal = ObtenerFecha(x.Otros, "FechaFinal"),
                FechaInicial = ObtenerFecha(x.Otros, "FechaInicial"),
                Nombre = x.Nombre,
                Guid = x.Guid,
                Id = x.Id,
                Interes = x.Interes,
                Nota = ObtnerCadena(x.Otros, "Nota"),
                TipoDeAhorroId = ObtnerNumero(x.Otros, "TipoDeCuentaId"),
                TipoDeAhorro = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas),
            }).ToList();

            return dtos;
        }

        private TipoDeAhorroDto ObtnerTipoDeCuenta(int? v, List<TipoDeCuenta> tipoDeCuentas)
        {
            if (v == null) return null;

            TipoDeCuenta tipoDeCuenta;

            tipoDeCuenta = tipoDeCuentas.FirstOrDefault(x => x.Id == v);

            return tipoDeCuenta.ToDto();
        }

        private int? ObtnerNumero(Dictionary<string, string> otros, string key)
        {
            var data = otros.Where(x => x.Key == key).FirstOrDefault();
            if (data.Value == null)
                return null;

            return int.Parse(data.Value);
        }

        private string ObtnerCadena(Dictionary<string, string> otros, string key)
        {
            var data = otros.Where(x => x.Key == key).FirstOrDefault();
            if (data.Value == null)
                return null;

            return data.Value;
        }

        private DateTime? ObtenerFecha(Dictionary<string, string> otros, string key)
        {
            var data = otros.Where(x => x.Key == key).FirstOrDefault();
            if (data.Value == null)
                return null;
            string fechaEnCadena = data.Value.Replace(" 12:00:00 a. m.", string.Empty);
            DateTime fecha = DateTime.ParseExact(fechaEnCadena, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return fecha;
        }

        public async Task ActualizarAsync(string ahorroId, AhorroDtoIn ahorro)
        {
            Ahorro ahorro1 = await _repositorio.Ahorro.ObtenerPorIdAsync(ahorroId);
            ahorro1.Nombre = ahorro.Nombre;
            if (ahorro1.Otros.ContainsKey(FechaFinal))
                ahorro1.Otros[FechaFinal] = ahorro.FechaFinal is null ? null : ahorro.FechaFinal.ToString();
            if (ahorro1.Otros.ContainsKey(FechaInicial))
                ahorro1.Otros[FechaInicial] = ahorro.FechaInicial is null ? null : ahorro.FechaInicial.ToString();

            await _repositorio.Ahorro.ActualizarAsync(ahorro1);
        }

        public async Task BorrarAsync(string ahorroId)
        {
            Ahorro ahorro;

            ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(ahorroId);
            ahorro.Estado = "Inactivo";

            await _repositorio.Ahorro.ActualizarAsync(ahorro);
        }

        public async Task<AhorroDto> ObtenerAsync(string ahorroId)
        {
            Ahorro x;
            AhorroDto ahorroDto;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorio1.TipoDeAhorro.ObtenerTodosAsync();
            x = await _repositorio.Ahorro.ObtenerPorIdAsync(ahorroId);
            ahorroDto = new AhorroDto
            {
                Balance = x.Total,
                FechaFinal = ObtenerFecha(x.Otros, "FechaFinal"),
                FechaInicial = ObtenerFecha(x.Otros, "FechaInicial"),
                Nombre = x.Nombre,
                Guid = x.Guid,
                Id = x.Id,
                Interes = x.Interes,
                Nota = ObtnerCadena(x.Otros, "Nota"),
                TipoDeAhorroId = ObtnerNumero(x.Otros, "TipoDeCuentaId"),
                TipoDeAhorro = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas),
                //Retiros = x.Retiros.Select(x => new MovimientoDeAhorroDto
                //{
                //    Cantidad = x.Cantidad,
                //    Concepto = x.Concepto,
                //    FechaDeRegistro = x.FechaDeRegistro,
                //    Referencia = x.EncodedKey
                //}).OrderByDescending(x => x.FechaDeRegistro).ToList(),
                //Depositos = x.Depositos.Select(x => new MovimientoDeAhorroDto
                //{
                //    Cantidad = x.Cantidad,
                //    Concepto = x.Concepto,
                //    FechaDeRegistro = x.FechaDeRegistro,
                //    Referencia = x.EncodedKey
                //}).OrderByDescending(x => x.FechaDeRegistro).ToList(),
            };

            return ahorroDto;
        }

        public async Task<IdDto> RetirarAsync(string idGuid, MovimientoDtoIn retiro)
        {
            if (retiro.Referencia == null)
                retiro.Referencia = Guid.NewGuid().ToString();

            var id = await _repositorio.Ahorro.RetirarAsync(idGuid, new Movimiento
            {
                Cantidad = retiro.Cantidad,
                Concepto = retiro.Concepto,
                EncodedKey = retiro.Referencia.ToString(),
                FechaDeRegistro = DateTime.Now
            });

            return new IdDto
            {
                Id = id,
                Guid = retiro.Referencia
            };
        }

        public async Task<IdDto> DepositarAsync(string idGuid, MovimientoDtoIn deposito)
        {
            if (deposito.Referencia == null)
                deposito.Referencia = Guid.NewGuid().ToString();

            var id = await _repositorio.Ahorro.DepositarAsync(idGuid, new Movimiento
            {
                Cantidad = deposito.Cantidad,
                Concepto = deposito.Concepto,
                EncodedKey = deposito.Referencia.ToString()
            });

            return new IdDto
            {
                Id = id,
                Guid = deposito.Referencia
            };
        }

        public async Task<List<MovimientoDto>> ObtenerMovimientosAsync(string ahorroId)
        {
            Ahorro ahorro;
            List<MovimientoDto> movimientos;

            ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(ahorroId);
            movimientos = new List<MovimientoDto>();
            movimientos.AddRange(ahorro.Depositos.ToDtos("Deposito"));
            movimientos.AddRange(ahorro.Retiros.ToDtos("Retiro"));

            return movimientos.OrderByDescending(x => x.FechaDeRegistro).ToList();
        }
    }
}
