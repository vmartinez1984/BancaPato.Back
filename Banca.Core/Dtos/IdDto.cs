namespace Banca.Core.Dtos
{
    public class IdDto
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}
