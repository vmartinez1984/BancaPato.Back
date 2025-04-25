using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Banca.Core.Dtos;

namespace Gastos.ReglasDeNegocio.Entities;

public partial class Presupuesto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }
 
    public decimal Cantidad { get; set; }

    public int SubcategoriaId { get; set; }

    public int VersionId { get; set; }

    public string Guid { get; set; }

    public List<MovimientoDePeridodo> Movimientos { get; set; } = new List<MovimientoDePeridodo>();

    public Subcategoria Subcategoria { get; set; } = null!;

    public int? AhorroId { get; set; }

    public string AhorroTipo { get; set; }

    public decimal Gastado { get; set; }
    public bool EstaActivo { get; set; } = true;

    public static implicit operator Presupuesto(PresupuestoDto v)
    {
        throw new NotImplementedException();
    }
}

public class MovimientoDePeridodo
{
    [BsonElement("_id")]
    public int Id { get; set; }

    public string Guid { get; set; }

    public decimal Cantidad { get; set; }

    public DateTime FechaDeRegistro { get; set; }
}


