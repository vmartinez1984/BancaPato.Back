using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class PeriodoDtoIn
    {
        public string Guid { get; set; }

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

        public int VersionId { get; set; }
    }

    public class PeriodoDto: PeriodoDtoIn
    {
        public int Id { get; set; }
        //public VersionDto Version { get; set; }
    }

    public class MovimientoDto : MovimientoDtoIn
    {
        public int Id { get; set; }
    }

    public class MovimientoDtoIn
    {
        public string Guid { get; set; }                

        [Required]
        public int PresupuestoId { get; set; }

        [Required]
        public decimal Cantidad { get; set; }
    }
}
