using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class CompraTarjetaDeCredito
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }

        public string Encodedkey { get; set; }


        public string Nombre { get; set; }

        public string Nota { get; set; }

        public int MesesSinIntereses { get; set; } = 0;

        public decimal Monto { get; set; }

        public decimal Saldo { get; set; }

        public bool EstaActivo { get; set; } = true;

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public DateOnly FechaDeCompra { get; set; }

        public DateOnly FechaDePago { get;  set; }
    }
}