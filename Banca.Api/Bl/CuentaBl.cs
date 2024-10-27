using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Dtos;
using Banca.Api.Entities;
using Banca.Api.Interfaces;
using Banca.Comun.Dtos;
using Banco.Repositorios.Entities;
using System.Globalization;

namespace Banca.BusinessLayer.Bl
{
    public class CuentaBl : BaseBl
    {
        public CuentaBl(IMapper mapper, IGastosRepository repository) :
            base(mapper, repository)
        { }

        public async Task<IdDto> AgregarAsync(CuentaDtoIn cuenta)
        {
            int id;

            if (cuenta.Guid == null)
                cuenta.Guid = Guid.NewGuid().ToString();
            Ahorro ahorro = await ObtenerAhorroAsync(cuenta);
            id = await _repositorioMongo.Ahorro.AgregarAsycn(ahorro);

            return new IdDto { Id = id, Guid = cuenta.Guid };
        }

        private async Task<Ahorro> ObtenerAhorroAsync(CuentaDtoIn cuenta)
        {
            Ahorro ahorro;
            Dictionary<string, string> otros = new Dictionary<string, string>();
            List<TipoDeCuenta> tipos;
            TipoDeCuenta tipoDeCuenta;

            tipos = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
            tipoDeCuenta = tipos.FirstOrDefault(x => x.Id == cuenta.TipoDeCuentaId);
            otros.Add("Nota", cuenta.Nota);
            otros.Add("FechaInicial", cuenta.FechaInicial.ToString());
            otros.Add("FechaFinal", cuenta.FechaFinal.ToString());
            otros.Add("TipoDeCuentaId", tipoDeCuenta.Id.ToString());
            otros.Add("TipoDeCuenta", tipoDeCuenta.Nombre);
            ahorro = new Ahorro
            {
                Id = 0,
                Guid = cuenta.Guid.ToString(),
                ClienteId = "148318",
                Total = 0,
                Interes = cuenta.Interes,
                Nombre = cuenta.Nombre,
                FechaDeRegistro = DateTime.Now,
                Estado = "Activo",
                Otros = otros
            };

            return ahorro;
        }

        public async Task<List<AhorroDto>> Obtener()
        {
            List<AhorroDto> dtos;
            List<Ahorro> entidades;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
            entidades = await _repositorioMongo.Ahorro.ObtenerAsync();
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
                TipoDeCuentaId = ObtnerNumero(x.Otros, "TipoDeCuentaId"),
                TipoDeCuenta = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas),
            }).ToList();

            return dtos;
        }

        private TipoDeCuentaDto ObtnerTipoDeCuenta(int? v, List<TipoDeCuenta> tipoDeCuentas)
        {
            if (v == null) return null;

            TipoDeCuenta tipoDeCuenta;

            tipoDeCuenta = tipoDeCuentas.FirstOrDefault(x => x.Id == v);

            return _mapper.Map<TipoDeCuentaDto>(tipoDeCuenta);
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

        internal async Task ActualizarAsync(string ahorroId, CuentaDtoIn ahorro)
        {
            throw new NotImplementedException();
        }

        internal async Task BorrarAsync(string ahorroId)
        {
            throw new NotImplementedException();
        }

        internal async Task<AhorroDto> ObtenerAsync(string ahorroId)
        {
            Ahorro x;
            AhorroDto ahorroDto;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
            x = await _repositorioMongo.Ahorro.ObtenerAsync(ahorroId);
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
                TipoDeCuentaId = ObtnerNumero(x.Otros, "TipoDeCuentaId"),
                TipoDeCuenta = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas),
                Retiros = x.Retiros.Select(x => new MovimientoDeAhorroDto
                {
                    Cantidad = x.Cantidad,
                    Concepto = x.Concepto,
                    FechaDeRegistro = x.FechaDeRegistro,
                    Referencia = x.Referencia
                }).OrderByDescending(x => x.FechaDeRegistro).ToList(),
                Depositos = x.Depositos.Select(x => new MovimientoDeAhorroDto
                {
                    Cantidad = x.Cantidad,
                    Concepto = x.Concepto,
                    FechaDeRegistro = x.FechaDeRegistro,
                    Referencia = x.Referencia
                }).OrderByDescending(x => x.FechaDeRegistro).ToList(),
            };
            //ahorroDto.Calculos = ObtenerCalculos(ahorro);

            return ahorroDto;
        }

        private List<Calculo> ObtenerCalculos(Cuentum ahorro)
        {
            List<Calculo> calculos;
            int numeroDeDias;
            DateTime fechaInicial;
            DateTime fechaActual;
            decimal cantidad;

            if (ahorro.Transaccions.Count > 0)
                fechaInicial = ahorro.Transaccions.OrderBy(x => x.FechaDeRegistro).FirstOrDefault().FechaDeRegistro;
            else
                fechaInicial = (DateTime)ahorro.FechaInicial;
            if (ahorro.FechaFinal == null)
                fechaActual = DateTime.Now;
            else
                fechaActual = (DateTime)ahorro.FechaFinal;
            numeroDeDias = (fechaActual - fechaInicial).Days + 2;
            calculos = new List<Calculo>();

            if (ahorro.Transaccions.Count > 0)
                cantidad = ahorro.Transaccions[0].Cantidad;
            else
                cantidad = 0;
            for (int i = 1; i < numeroDeDias; i++)
            {
                decimal interesDelDia;
                decimal interesCalculado;
                decimal transaccion;

                interesDelDia = ObtenerInteresDelDia(ahorro);
                interesCalculado = Math.Round((cantidad * interesDelDia / 100 / 365), 2);
                transaccion = ObtenerTransaccion(ahorro, fechaInicial.AddDays(i));
                calculos.Add(new Calculo
                {
                    Subtotal = cantidad,
                    InteresCalculado = interesCalculado,
                    Total = cantidad + interesCalculado + transaccion,
                    Transaccion = transaccion,
                    Fecha = fechaInicial.AddDays(i),
                });
                cantidad = calculos[i - 1].Total;
            }

            return calculos;
        }

        private decimal ObtenerTransaccion(Cuentum ahorro, DateTime fecha)
        {
            decimal movimiento;
            decimal retiros;
            decimal depositos;

            retiros = ahorro.Transaccions.Where(x => x.Tipo == Tipo.Retiro && x.FechaDeRegistro.Date == fecha.Date).Sum(x => x.Cantidad);
            depositos = ahorro.Transaccions.Where(x => x.Tipo == Tipo.Deposito && x.FechaDeRegistro.Date == fecha.Date).Sum(x => x.Cantidad);
            movimiento = depositos - retiros;

            return movimiento;
        }

        private decimal ObtenerInteresDelDia(Cuentum ahorro)
        {
            return (decimal)(ahorro.Interes);
        }

        private int ObtenerAhorroId(string ahorroId)
        {
            return int.Parse(ahorroId);
        }
    }
}
;