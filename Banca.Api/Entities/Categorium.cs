using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Banco.Repositorios.Entities;

public partial class Categorium
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EstaActivo { get; set; }

    public virtual ICollection<Subcategorium> Subcategoria { get; set; } = new List<Subcategorium>();
}
