using Banca.Core.Dtos;

namespace Banca.Api.Dtos
{
    public class HistorialDtoIn
    {
        public Guid? Guid { get; set; }

        public decimal Cantidad { get; set; }
        public decimal Interes { get; set; }

        public string Nota { get; set; }

        public int CuentaId { get; set; }
    }

    public class HistorialDto : HistorialDtoIn
    {
        public int Id { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public AhorroDto    Cuenta { get; set; }
    }
}
