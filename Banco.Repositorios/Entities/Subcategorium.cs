using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Subcategorium
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Presupuesto { get; set; }

    public bool EstaActivo { get; set; }

    public bool EsPrimario { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
