﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Banca.Comun.Dtos
{
    public class CuentaDto: CuentaDtoIn
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }

    }

    public class CuentaDtoIn
    {        
        public Guid? Guid { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        public string Clabe { get; set; }

        public string Nota { get; set; }

        [Required]
        public decimal Interes { get; set; }

        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }

        public int TipoDeCuentaId { get; set; }

    }

}