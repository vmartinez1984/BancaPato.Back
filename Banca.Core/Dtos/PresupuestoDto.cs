namespace Banca.Core.Dtos
{
    public class PresupuestoDto
    {
        public int Id { get; set; }
       
        public decimal Cantidad { get; set; }
                
        public int? AhorroId { get; set; }

        public int SubcategoriaId { get; set; }

        public string Guid { get; set; }

        public int VersionId { get; set; }

    }

    public class PresupuestoDtoIn
    {
        public int SubcategoriaId { get; set; }

        public decimal Cantidad { get; set; }
               
        public string Guid { get; set; }

        public int? AhorroId { get; set; }

        public int VersionId { get; set; }
    }

    public class EstadoDelPresupuesto
    {
        public const string Ok = "Ok";
        public const string Warning = "Warning";
        public const string Danger = "Danger";
        public const string SinProcesar = "Sin procesar";
        public const string EnProceso = "En proceso";
    }
}