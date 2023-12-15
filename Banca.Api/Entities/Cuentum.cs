using Banca.Api.Entities;

namespace Banco.Repositorios.Entities;

public partial class Cuentum
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Clabe { get; set; }

    public string? Nota { get; set; }

    public decimal? Interes { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }

    public virtual ICollection<HistorialDeApartado> HistorialDeApartados { get; set; } = new List<HistorialDeApartado>();

    public virtual List<Transaccion> Transaccions { get; set; } = new List<Transaccion>();

    public int? TipoDeCuentaId { get; set; }

    public virtual TipoDeCuenta TipoDeCuenta { get; internal set; }

    public int? CuentaDeReferenciaId { get; set; }

    public bool EstaActivo { get; set; } = true;

}
