using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Cuentum
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Clabe { get; set; }

    public string? Nota { get; set; }

    public decimal? Interes { get; set; }

    public virtual ICollection<HistorialDeApartado> HistorialDeApartados { get; set; } = new List<HistorialDeApartado>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
