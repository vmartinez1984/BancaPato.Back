﻿namespace Banco.Repositorios.Entities;

public partial class Presupuesto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public int SubcategoriaId { get; set; }

    public int VersionId { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual Subcategorium Subcategoria { get; set; } = null!;

    public virtual VersionDePresupuesto Version { get; set; } = null!;

    public int? AhorroId { get; set; }
}
