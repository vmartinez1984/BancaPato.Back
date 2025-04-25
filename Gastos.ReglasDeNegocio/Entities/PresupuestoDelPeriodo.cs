namespace Gastos.ReglasDeNegocio.Entities
{
    internal class PresupuestoDelPeriodo
    {
        public int Id { get; set; }

        public int PeriodoId { get; set; }

        public int PresupuestoId { get; set; }

        public decimal Cantidad { get; set; }

        public int AhorroId { get; set; }

        public decimal Gastado { get; set; }
    }
}
