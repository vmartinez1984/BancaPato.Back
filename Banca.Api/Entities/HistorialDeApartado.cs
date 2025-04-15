namespace Banco.Repositorios.Entities;

public partial class HistorialDeApartado
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public decimal Cantidad { get; set; }
    public decimal Interes { get; set; }

    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;  

    public string Nota { get; set; }

    public int CuentaId { get; set; }

   
}
