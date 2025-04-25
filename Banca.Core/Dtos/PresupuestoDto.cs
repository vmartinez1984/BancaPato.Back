namespace Banca.Core.Dtos
{
    public class PresupuestoDto
    {
        public int Id { get; set; }

        public SubcategoriaDto Subcategoria { get; set; }

        public decimal Cantidad { get; set; }
                
        public int? AhorroId { get; set; }

        public int SubcategoriaId { get; set; }

        public string Guid { get; set; }

        //public List<MovimientoDto> Movimientos { get; set; } = new List<MovimientoDto>();

        //public string AhorroTipo { get; set; }

        //public string Estado
        //{
        //    get
        //    {
        //        if (TotalGastado == Cantidad)
        //            return EstadoDelPresupuesto.Ok;
        //        if (Disponible < 0)
        //            return EstadoDelPresupuesto.Danger;
        //        if (TotalGastado == 0)
        //            return EstadoDelPresupuesto.SinProcesar;
        //        decimal diferencia = Cantidad - TotalGastado;
        //        const decimal porcentaje = 0.16m;
        //        decimal tolerancia = Cantidad * porcentaje;
        //        if (diferencia > tolerancia)
        //            return EstadoDelPresupuesto.EnProceso;
        //        return EstadoDelPresupuesto.Ok;
        //    }
        //}

        //public decimal TotalGastado => Movimientos.Sum(x => x.Cantidad);

        //public decimal Disponible => Cantidad - Movimientos.Sum(x => x.Cantidad);
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