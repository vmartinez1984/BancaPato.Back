using System.ComponentModel.DataAnnotations;

namespace Banca.Api.Dtos
{
    public class TransaccionDto
    {
        public int Id { get; set; }

        public int CuentaId { get; set; }

        public Guid Guid { get; set; }

        public decimal Cantidad { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public string Tipo { get; set; }
        public string Concepto { get; set; }
        public string Nota { get; set; }
    }

    public class TransaccionDtoIn
    {
        public int CuentaId { get; set; }

        public Guid Guid { get; set; }

        public decimal Cantidad { get; set; }

        public DateTime FechaDeRegistro { get; set; }        
    }

    public class DepositoDtoIn
    {       
        public Guid? Guid { get; set; }

        public Guid? Referencia { get; set; }

        public decimal Cantidad { get; set; }

        [StringLength(20)]
        public string Concepto { get; set; }

        [StringLength(50)]
        public string Nota { get; set; }
    }

    public class RetiroDtoIn
    {      

        public string Guid { get; set; }

        public decimal Cantidad { get; set; }

        [StringLength(50)]
        public string Nota { get; set; }
        public string Concepto { get; set; }
    }

    public class Tipo
    {
        public const string Deposito = "Deposito";
        public const string Retiro = "Retiro";

    }

    public class IdDto
    {
        public int Id { get; set; }

        public string Guid { get; set; }
    }
}
