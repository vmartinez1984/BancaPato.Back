namespace Gastos.ReglasDeNegocio.Entities;

public class Transaccion
{
    public int Id { get; set; }

    public int PeriodoId { get; set; }

    public int PresupuestoId { get; set; }

    public decimal Cantidad { get; set; }
}