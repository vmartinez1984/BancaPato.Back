using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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

    public List<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public Subcategorium Subcategoria { get; set; } = null!;

    public int? AhorroId { get; set; }

    public string AhorroTipo { get; set; }
}
