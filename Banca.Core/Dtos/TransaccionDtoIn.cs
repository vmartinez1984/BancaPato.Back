using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class TransaccionDtoIn
    {
        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int PresupuestoId { get; set; }

        [Required]
        public decimal Cantidad { get; set; }
    }
}
