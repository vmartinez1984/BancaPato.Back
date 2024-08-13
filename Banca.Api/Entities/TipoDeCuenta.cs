﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banca.Api.Entities
{
    public class TipoDeCuenta
    {
        [NotMapped]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }

        [NotMapped]
        public bool EstaActivo { get; set; } = true;
    }
}
