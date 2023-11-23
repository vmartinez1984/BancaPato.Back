using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Movimiento
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string? Nota { get; set; }

    public int PeriodoId { get; set; }

    public int? TransaccionId { get; set; }

    public int PresupuestoId { get; set; }

    public virtual Periodo Periodo { get; set; } = null!;

    public virtual Presupuesto Presupuesto { get; set; } = null!;

    public virtual Transaccion Transaccion { get; set; } = null!;
}
