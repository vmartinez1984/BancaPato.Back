using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class TransaccionDtoIn
    {
        //[Required]
        //public int PeriodoId { get; set; }

        [Required]
        public int PresupuestoId { get; set; }

        [Required]
        public decimal Cantidad { get; set; }
    }
}
