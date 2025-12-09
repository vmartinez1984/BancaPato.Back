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

        public string Nota { get; set; }
    }

    public class TransaccionDto
    {
        public int Id { get; set; }
        public string EncodedKey { get; set; }                
        public int PresupuestoId { get; set; }                
        public decimal Cantidad { get; set; }
        public string Nota { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}
