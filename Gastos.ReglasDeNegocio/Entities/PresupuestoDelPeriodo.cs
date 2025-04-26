using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gastos.ReglasDeNegocio.Entities
{
    public class PresupuestoDelPeriodo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int Id { get; set; }

        public int PeriodoId { get; set; }

        public int PresupuestoId { get; set; }

        public string CategoriaNombre { get; set; }

        public decimal Cantidad { get; set; }

        public int? AhorroId { get; set; }

        public decimal Gastado { get; set; }

        public bool EstaActivo { get; set; } = true;
    }
}
