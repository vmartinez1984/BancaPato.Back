using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class VersionDto : VersionDtoIn
    {
        public int Id { get; set; }

        //public List<PresupuestoDto> Presupuestos { get; set; } = new List<PresupuestoDto>();      
    }

    public class VersionDtoIn
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        public DateTime FechaInicial { get; set; }

        [Required]
        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        public DateTime FechaFinal { get; set; }
        public string Guid { get; set; }
    }
}
