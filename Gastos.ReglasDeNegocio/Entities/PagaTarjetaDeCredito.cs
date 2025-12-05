using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class PagaTarjetaDeCredito
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }

        public string Encodekey { get; set; }

        public string CompraTarjetaDeCreditoEncodedkey { get; set; }
        public int CompraTarjetaDeCreditoId { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaDeRegsitro { get; set; } = DateTime.Now;
    }
}
