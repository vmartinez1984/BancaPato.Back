using Banco.Repositorios.Entities;
using System.ComponentModel.DataAnnotations;

namespace Banca.Api.Dtos
{
    public class VersionDto: VersionDtoIn
    {
        public int Id { get; set; }

        public List<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
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
        public string Guid { get;  set; }
    }
}
