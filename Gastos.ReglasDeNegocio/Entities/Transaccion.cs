using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gastos.ReglasDeNegocio.Entities;

public class Transaccion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }
    public int Id { get; set; }

    public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

    public int PresupuestoId { get; set; }

    public decimal Monto { get; set; }

    public decimal SaldoFinal { get; set; }

    public decimal SaldoInicial { get; set; }

    public int PeriodoId { get; set; }

    public string RetiroEncodedKey { get; set; }

    public string DepositoEncodedKey { get; set; }

}