using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Periodo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaInicial { get; set; }

    public DateTime FechaFinal { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public bool EstaActivo { get; set; }

    public string? Nota { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
