using System.ComponentModel.DataAnnotations;

namespace Banca.Api.Dtos
{
    public class SubcategoriaDto
    {
        public int Id { get; set; }

        public CategoriaDto Categoria { get; set; }
        public string Nombre { get; set; }
                
        public decimal Presupuesto { get; set; }

        public Guid Guid { get; set; }

        public bool EstaActivo { get; set; } = true;

        public bool EsPrimario { get; set; } = false;
    }
    public class SubcategoriaDtoIn
    {
        [Required(ErrorMessage = "Seleccione una categoria")]
        //[CategoriaIdExiste("Seleccione una categoria valida")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe estar entre {0} y {1}")]
        public decimal Presupuesto { get; set; }
                
        public string Guid { get; set; }

        public bool EsPrimario { get; set; } = false;

    }
}
