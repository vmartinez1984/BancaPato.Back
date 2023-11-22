namespace Banca.Api.Dtos
{
    public class PresupuestoDto
    {
        public int Id { get; set; }

        public SubcategoriaDto Subcategoria { get; set; }

        public decimal Cantidad { get; set; }

        public decimal CantidadMeta { get; set; }

        public int VersionId { get; set; }

        public int? AhorroId { get; set; }
    }

    public class PresupuestoDtoIn
    {
        public int SubcategoriaId { get; set; }

        public decimal Cantidad { get; set; }

        public decimal CantidadMeta { get; set; }

        public int VersionId { get; set; }
               
        public Guid? Guid { get; set; }

        public int? AhorroId { get; set; }
    }
}
