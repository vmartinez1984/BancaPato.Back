using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Banco.Repositorios.Entities;

public partial class VersionDePresupuesto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }

    public string  Nombre { get; set; }
    
    public string Guid { get; set; }

    public DateTime FechaInicial { get; set; }

    public DateTime FechaFinal { get; set; }

    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

    public bool EstaActivo { get; set; } = true;

    public List<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();
}
