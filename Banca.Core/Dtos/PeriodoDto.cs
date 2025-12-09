using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class PeriodoDtoIn
    {
        public string Guid { get; set; }// = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha inicial es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha inicial")]
        public DateTime FechaInicial { get; set; }

        [Required(ErrorMessage = "La fecha final es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        [Required(ErrorMessage = "La version es requerida")]
        public int VersionId { get; set; }
    }

    public class PeriodoDto : PeriodoDtoIn
    {
        public int Id { get; set; }        
    }

    public class MovimientoDto : MovimientoDtoIn
    {        
        public string Guid { get; set; }

        public decimal Monto { get; set; }
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
        public string Concepto { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Tipo { get; set; }
    }

    public class MovimientoDtoIn
    {
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();
                
        public int PresupuestoId { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Monto { get; set; }
              
        public string Concepto { get; set; }
    }
}
