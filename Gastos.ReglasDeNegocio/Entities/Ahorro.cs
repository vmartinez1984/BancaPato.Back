using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class Ahorro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }

        public string Guid { get; set; }

        public string Nombre { get; set; }

        public decimal Total { get; set; }

        [BsonElement("ClienteId")]
        public string ClienteEncodedKey { get; set; }

        public Dictionary<string, string> Otros { get; set; } = new Dictionary<string, string>();

        public decimal Interes { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "Activo";


        public object Depositos { get; set; }

        public object Retiros { get; set; }

        public int? TipoDeAhorroId { get; set; }

        public DateOnly? FechaInicial { get; set; }

        public DateOnly? FechaFinal { get; set; }
    }
}
