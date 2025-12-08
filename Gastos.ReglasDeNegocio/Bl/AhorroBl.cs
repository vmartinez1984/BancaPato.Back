using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;
using System.Globalization;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class AhorroBl
    {
        private readonly Repositorio _repositorio;
        private readonly MovimientoRepositorio movimientoRepositorio;
        const string FechaFinal = "FechaFinal";
        const string FechaInicial = "FechaInicial";

        public AhorroBl(Repositorio repositorio1, MovimientoRepositorio movimientoRepositorio)
        {
            _repositorio = repositorio1;
            this.movimientoRepositorio = movimientoRepositorio;
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

            tipos = await _repositorio.TipoDeAhorro.ObtenerTodosAsync();
            otros.Add("Nota", cuenta.Nota);
            otros.Add("FechaInicial", cuenta.FechaInicial is null ? null : ((DateTime)cuenta.FechaInicial).ToString("d"));
            otros.Add("FechaFinal", cuenta.FechaFinal is null ? null : ((DateTime)cuenta.FechaFinal).ToString("d"));
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

            tipoDeCuentas = await _repositorio.TipoDeAhorro.ObtenerTodosAsync();
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

            tipoDeCuentas = await _repositorio.TipoDeAhorro.ObtenerTodosAsync();
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
                TipoDeAhorro = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas)
            };

            return ahorroDto;
        }

        public async Task<IdDto> RetirarAsync(string idGuid, MovimientoDtoIn retiro)
        {
            Ahorro ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(idGuid);

            if (ahorro.Total < retiro.Monto)
                throw new Exception("No hay suficiente camarón");
            Movimiento movimiento = retiro.ToEntity();
            movimiento.SaldoInicial = ahorro.Total;
            movimiento.SaldoFinal = ahorro.Total - movimiento.Cantidad;
            movimiento.AhorroEncodedkey = ahorro.Guid;
            movimiento.AhorroId = ahorro.Id;
            movimiento.Tipo = "Retiro";
            var id = await movimientoRepositorio.AgregarAsync(movimiento);
            ahorro.Total = ahorro.Total - retiro.Monto;
            await _repositorio.Ahorro.ActualizarAsync(ahorro);

            return new IdDto
            {
                Id = id,
                Guid = retiro.Encodedkey
            };
        }

        public async Task<IdDto> DepositarAsync(string idGuid, MovimientoDtoIn deposito)
        {
            Ahorro ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(idGuid);                        
            Movimiento movimiento = deposito.ToEntity();
            movimiento.SaldoInicial = ahorro.Total;
            movimiento.SaldoFinal = ahorro.Total + movimiento.Cantidad;
            movimiento.AhorroEncodedkey = ahorro.Guid;
            movimiento.AhorroId = ahorro.Id;
            movimiento.Tipo = "Deposito";
            var id = await movimientoRepositorio.AgregarAsync(movimiento);
            ahorro.Total = ahorro.Total + deposito.Monto;
            await _repositorio.Ahorro.ActualizarAsync(ahorro);

            return new IdDto
            {
                Id = id,
                Guid = deposito.Encodedkey
            };
        }

        public async Task<List<MovimientoDto>> ObtenerMovimientosAsync(string ahorroId)
        {
            List<Movimiento> movimientos;

            movimientos = await movimientoRepositorio.ObtenerTodosPorAhorroIdAsync(ahorroId);

            return movimientos.ToDtos().OrderByDescending(x => x.FechaDeRegistro).ToList();
        }
    }
}
