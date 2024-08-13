using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banco.Repositorios.Entities;

public partial class Subcategorium
{
    [NotMapped]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public int Id { get; set; }    
       

    public string Guid { get; set; }
        

    public string Nombre { get; set; } = null!;

    public decimal Presupuesto { get; set; }

    public bool EstaActivo { get; set; } = true;

    public bool EsPrimario { get; set; }

    public int CategoriaId { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;
        
    public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
}
