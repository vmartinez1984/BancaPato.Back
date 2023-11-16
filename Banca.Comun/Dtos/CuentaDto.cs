using System;

namespace Banca.Comun.Dtos
{
    public class CuentaDto: CuentaDtoIn
    {
        public int Id { get; set; }
    }

    public class CuentaDtoIn
    {        
        public Guid Guid { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Clabe { get; set; }

        public string? Nota { get; set; }

        public decimal? Interes { get; set; }
    }

}
