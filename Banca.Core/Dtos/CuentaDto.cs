using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class AhorroDto : AhorroDtoIn
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public TipoDeCuentaDto TipoDeCuenta { get; set; }

        //public List<Calculo> Calculos { get; set; }

        public List<MovimientoDeAhorroDto> Depositos { get; set; } = new List<MovimientoDeAhorroDto>();
        public List<MovimientoDeAhorroDto> Retiros { get; set; } = new List<MovimientoDeAhorroDto>();
    }

    public class Calculo
    {
        public decimal Subtotal { get; set; }
        public decimal InteresCalculado { get; set; }

        public decimal Total { get; set; }

        public decimal Transaccion { get; set; }

        public DateTime Fecha { get; set; }
    }

    public class AhorroDtoIn
    {
        public string Guid { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        public string Nota { get; set; }

        [Required]
        public decimal Interes { get; set; }

        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }

        public int? TipoDeCuentaId { get; set; }

        public int? CuentaDeReferenciaId { get; set; }
    }

    public class MovimientoDeAhorroDto
    {
        public decimal Cantidad { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public string Concepto { get; set; }

        public string Referencia { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal SaldoFin { get; set; }
    }
}
