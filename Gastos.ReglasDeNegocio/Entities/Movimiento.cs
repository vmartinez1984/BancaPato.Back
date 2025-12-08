using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class Movimiento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public decimal Cantidad { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public string Concepto { get; set; }

        public string Referencia { get; set; }

        public string EncodedKey { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal SaldoFinal { get; set; }

        public int AhorroId { get; set; }

        public string AhorroEncodedkey { get; set; }

        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}
