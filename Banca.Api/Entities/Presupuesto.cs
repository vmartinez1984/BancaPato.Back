using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using DuckBank.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Banco.Repositorios.Entities;

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

    public Subcategorium Subcategoria { get; set; } = null!;

    public int? AhorroId { get; set; }

    public string AhorroTipo { get; set; }
}

public class MovimientoDePeridodo
{
    [BsonElement("_id")]
    internal int Id;

    public string Guid { get; set; }

    public decimal Cantidad { get; set; }

    public DateTime FechaDeRegistro { get; set; }
}


