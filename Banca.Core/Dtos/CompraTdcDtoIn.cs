namespace Banca.Core.Dtos
{
    public class CompraTdcDtoIn
    {
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        public string Nombre { get; set; }

        public string Nota { get; set; }

        public int MesesSinIntereses { get; set; } = 0;

        public decimal Monto { get; set; }

        public DateOnly FechaDeCompra { get; set; }
    }

    public class CompraTdcDto
    {
        public int Id { get; set; }
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        public string Nombre { get; set; }

        public string Nota { get; set; }

        public int MesesSinIntereses { get; set; } = 0;

        public decimal Monto { get; set; }

        public decimal Saldo { get; set; }

        public DateOnly FechaDeCompra { get; set; }

        public DateTime FechaDeRegistro { get; set; }
        public DateOnly FechaDePago { get; set; }
    }
}
