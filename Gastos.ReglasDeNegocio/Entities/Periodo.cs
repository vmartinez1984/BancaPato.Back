using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities;

public class Periodo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaInicial { get; set; }

    public DateTime FechaFinal { get; set; }

    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

    public bool EstaActivo { get; set; } = true;

    public string Nota { get; set; }

    public string Guid { get; set; }    

    public int VersionId { get; set; }

    public VersionDePresupuesto Version { get; set; }
}
