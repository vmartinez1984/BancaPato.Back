using Banca.Api.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banca.Comun.Dtos
{
    public class CuentaDto : CuentaDtoIn
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

        public TipoDeCuentaDto TipoDeCuenta { get; set; }

        public List<Calculo> Calculos { get; set; }
    }

    public class Calculo
    {
        public decimal Subtotal { get; set; }
        public decimal InteresCalculado { get; set; }

        public decimal Total { get; set; }

        public decimal Transaccion { get; set; }

        public DateTime Fecha { get; set; }
    }

    public class CuentaDtoIn
    {
        public string Guid { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        public string Clabe { get; set; }

        public string Nota { get; set; }

        [Required]
        public decimal Interes { get; set; }

        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }

        public int? TipoDeCuentaId { get; set; }

        public int? CuentaDeReferenciaId { get; set; }
    }

}
