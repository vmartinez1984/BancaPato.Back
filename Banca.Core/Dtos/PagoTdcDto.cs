namespace Banca.Core.Dtos
{
    public class PagoTdcDto
    {
        public int Id { get; set; }
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        public string CompraTdcIdEndodedkey { get; set; }

        public decimal Monto { get; set; }

        public string Nota { get; set; }
        
    }
}
