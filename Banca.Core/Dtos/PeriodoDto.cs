using System.ComponentModel.DataAnnotations;

namespace Banca.Core.Dtos
{
    public class PeriodoDtoIn
    {
        public string Guid { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha inicial es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha inicial")]
        public DateTime FechaInicial { get; set; }

        [Required(ErrorMessage = "La fecha final es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }

        public int VersionId { get; set; }
    }

    public class PeriodoDto : PeriodoDtoIn
    {
        public int Id { get; set; }
        public VersionDto Version { get; set; }

        //public decimal TotalGastado
        //{
        //    get
        //    {
        //        decimal total = 0;
        //        if(Version is not null)
        //        foreach (var item in Version.Presupuestos)
        //            total += item.Movimientos.Sum(x => x.Cantidad);

        //        return total;
        //    }
        //}

        //public decimal TotalPresupuesto
        //{
        //    get
        //    {
        //        return Version is null ? 0: Version.Presupuestos.Sum(x => x.Cantidad);
        //    }
        //}

        //public decimal TotalPendiente
        //{
        //    get
        //    {
        //        return Version is null ? 0 : Version.Presupuestos.Where(x => x.Movimientos.Count == 0).Sum(x => x.Cantidad);
        //    }
        //}
    }

    public class MovimientoDto : MovimientoDtoIn
    {        
        public string Guid { get; set; }

        public decimal Cantidad { get; set; }
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
        public string Concepto { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Tipo { get; set; }
    }

    public class MovimientoDtoIn
    {
        //public string Guid { get; set; }

        //[Required]
        public int PresupuestoId { get; set; }

        [Required]
        public decimal Cantidad { get; set; }

        [Required]
        public string Referencia { get; set; }
        public string Concepto { get; set; }
    }
}
