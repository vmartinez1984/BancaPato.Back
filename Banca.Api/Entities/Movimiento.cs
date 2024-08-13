namespace Banco.Repositorios.Entities;

public partial class Movimiento
{
    public int Id { get; set; }
    public string Guid { get; set; }
    public decimal Cantidad { get; set; }    
    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
}
