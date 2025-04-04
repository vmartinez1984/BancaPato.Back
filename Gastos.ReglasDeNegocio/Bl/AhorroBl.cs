using Banca.Core.Dtos;
using DuckBank.Persistence.Entities;
using DuckBank.Persistence.Interfaces;
using Gastos.ReglasDeNegocio.Helpers;
using System.Globalization;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class AhorroBl : BaseBl
    {
        public AhorroBl(IRepositorio repositorio) : base(repositorio) { }

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

            tipos = await _repositorio.TipoDeCuenta.ObtenerTodosAsync();
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

            tipoDeCuentas = await _repositorio.TipoDeCuenta.ObtenerTodosAsync();
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

    }
}
