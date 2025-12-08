using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities;

public class Subcategoria//: EntidadBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public bool EstaActivo { get; set; } = true;

    public int Id { get; set; }           

    public string Guid { get; set; }        

    public string Nombre { get; set; } = null!;

    public decimal Presupuesto { get; set; }
       
    public bool EsPrimario { get; set; }

    public int CategoriaId { get; set; }
       
    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

    public object Categoria { get; set; }
}
