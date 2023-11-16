using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Transaccion
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public Guid Guid { get; set; }

    public decimal Cantidad { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual Cuentum Cuenta { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
