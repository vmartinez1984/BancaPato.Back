using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class Categoria//: EntidadBase
    {       
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public bool EstaActivo { get; set; } = true;
    }
}
