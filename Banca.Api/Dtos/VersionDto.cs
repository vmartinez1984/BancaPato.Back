using System.ComponentModel.DataAnnotations;

namespace Banca.Api.Dtos
{
    public class VersionDto: VersionDtoIn
    {
        public int Id { get; set; }
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
        [Display(Name = "Fecha inicial")]
        [DataType(DataType.Date)]
        public DateTime FechaFinal { get; set; }
        public Guid? Guid { get; internal set; }
    }
}
