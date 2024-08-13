using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco.Repositorios.Entities;

public partial class Subcategorium
{
    [NotMapped]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }    

    //private Guid guid;
    //[BsonIgnore]
    //public Guid Guid { get { return guid == Guid.Empty ? Guid.Parse(EncodedKey) : Guid; } set { guid = value; } 

    public string Guid { get; set; }

    //private string encodedKey;
    //[NotMapped]
    //[BsonElement("Guid")]
    //public string EncodedKey { get { return Guid.ToString(); } set { encodedKey = value; } }    

    public string Nombre { get; set; } = null!;

    public decimal Presupuesto { get; set; }

    public bool EstaActivo { get; set; } = true;

    public bool EsPrimario { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    //public virtual ICollection<Presupuesto> Presupuestos { get; set; } = new List<Presupuesto>();

    [NotMapped]
    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
}
