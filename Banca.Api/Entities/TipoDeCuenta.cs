namespace Banca.Api.Entities
{
    public class TipoDeCuenta
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
    }
}
