using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class EntidadBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public bool EstaActivo { get; set; } = true;
    }
}
