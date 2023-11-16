using System.ComponentModel.DataAnnotations;

namespace Banca.Api.Dtos
{
    public class PeriodoDtoIn
    {
        public Guid Guid { get; set; }

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
    }

    public class PeriodoDto: PeriodoDtoIn
    {
        public int Id { get; set; }
    }

    public class Movimiento : MovimientoIn
    {
        public int Id { get; set; }
    }

    public class MovimientoIn
    {

        public Guid Guid { get; set; }

        public string? Nota { get; set; }

        public int PeriodoId { get; set; }

        public int TransaccionId { get; set; }

        public int PresupuestoId { get; set; }
    }
}
