using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class HistorialDeApartado
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public decimal Cantidad { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public string? Nota { get; set; }

    public int CuentaId { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;
}
