using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Version
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public DateTime FechaInicial { get; set; }

    public DateTime FechaFinal { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public bool EstaActivo { get; set; }

    public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
