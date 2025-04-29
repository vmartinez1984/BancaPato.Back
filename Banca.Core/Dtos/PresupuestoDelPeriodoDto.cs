namespace Banca.Core.Dtos
{
    public class PresupuestoDelPeriodoDto
    {
        public int Id { get; set; }

        public int PeriodoId { get; set; }

        public int PresupuestoId { get; set; }

        public decimal Cantidad { get; set; }

        public int? AhorroId { get; set; }
        
        public string SubcategoriaNombre { get; set; }

        public decimal Gastado { get; set; }

        public string TipoDeAhorro { get; set; }
    }
}
