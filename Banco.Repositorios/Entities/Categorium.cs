using System;
using System.Collections.Generic;

namespace Banco.Repositorios.Entities;

public partial class Categorium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Subcategorium> Subcategoria { get; set; } = new List<Subcategorium>();
}
