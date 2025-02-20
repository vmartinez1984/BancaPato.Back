using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Entities;
using Banca.Api.Interfaces;
using Banca.Core.Dtos;
using DuckBank.Persistence.Interfaces;
using System.Globalization;
using Ahorro = DuckBank.Persistence.Entities.Ahorro;

namespace Banca.BusinessLayer.Bl
{
    public class AhorroBl : BaseBl
    {
        private readonly IRepositorio _repositorio;

        public AhorroBl(IMapper mapper, IGastosRepository repository, IRepositorio repositorio) :
            base(mapper, repository)
        {
            this._repositorio = repositorio;
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

        public async Task<List<AhorroDto>> Obtener()
        {
            List<AhorroDto> dtos;
            List<Ahorro> entidades;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
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

        internal Task ActualizarAsync(string ahorroId, AhorroDtoIn ahorro)
        {
            throw new NotImplementedException();
        }

        internal async Task BorrarAsync(string ahorroId)
        {
            Ahorro ahorro;

            ahorro = await _repositorio.Ahorro.ObtenerPorIdAsync(ahorroId);
            ahorro.Estado = "Inactivo";

            await _repositorio.Ahorro.ActualizarAsync(ahorro);
        }

        internal async Task<AhorroDto> ObtenerAsync(string ahorroId)
        {
            Ahorro x;
            AhorroDto ahorroDto;
            List<TipoDeCuenta> tipoDeCuentas;

            tipoDeCuentas = await _repositorioMongo.TipoDeCuenta.ObtenerTodosAsync();
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
                TipoDeCuentaId = ObtnerNumero(x.Otros, "TipoDeCuentaId"),
                TipoDeCuenta = ObtnerTipoDeCuenta(ObtnerNumero(x.Otros, "TipoDeCuentaId"), tipoDeCuentas),
                Retiros = x.Retiros.Select(x => new MovimientoDeAhorroDto
                {
                    Cantidad = x.Cantidad,
                    Concepto = x.Concepto,
                    FechaDeRegistro = x.FechaDeRegistro,
                    Referencia = x.EncodedKey
                }).OrderByDescending(x => x.FechaDeRegistro).ToList(),
                Depositos = x.Depositos.Select(x => new MovimientoDeAhorroDto
                {
                    Cantidad = x.Cantidad,
                    Concepto = x.Concepto,
                    FechaDeRegistro = x.FechaDeRegistro,
                    Referencia = x.EncodedKey
                }).OrderByDescending(x => x.FechaDeRegistro).ToList(),
            };
            //ahorroDto.Calculos = ObtenerCalculos(ahorro);

            return ahorroDto;
        }
    }
}