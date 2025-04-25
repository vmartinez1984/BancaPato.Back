namespace Gastos.ReglasDeNegocio.Entities
{
    internal class Movimiento
    {
        public int Id { get; set; }

        public int PeriodoId { get; set; }

        public int PresupuestoId { get; set; }

        public decimal Cantidad { get; set; }
    }
}
