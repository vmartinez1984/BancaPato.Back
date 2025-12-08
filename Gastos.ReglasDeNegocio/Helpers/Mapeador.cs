using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;

namespace Gastos.ReglasDeNegocio.Helpers
{
    internal static class Mapeador
    {
        public static TipoDeAhorroDto ToDto(this TipoDeCuenta entidad) => entidad is null ? null : new TipoDeAhorroDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        public static List<TipoDeAhorroDto> ToDtos(this List<TipoDeCuenta> entidades) => entidades.Select(x => x.ToDto()).ToList();

        public static CategoriaDto ToDto(this Categoria entidad) => entidad is null ? null : new CategoriaDto
        {
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        public static List<CategoriaDto> ToDtos(this List<Categoria> entidades) => entidades.Select(x => x.ToDto()).ToList();

        public static Categoria ToEntity(this CategoriaDto dto) => new Categoria
        {
            Id = dto.Id,
            Nombre = dto.Nombre,
        };

        public static Subcategoria ToEntity(this SubcategoriaDtoIn dto) => new Subcategoria
        {
            EsPrimario = dto.EsPrimario,
            CategoriaId = dto.CategoriaId,
            Guid = dto.Guid.ToString(),
            Nombre = dto.Nombre,
            Presupuesto = dto.Presupuesto
        };

        public static SubcategoriaDto ToDto(this Subcategoria entity) => entity is null ? null : new SubcategoriaDto
        {
            Id = entity.Id,
            CategoriaId = entity.CategoriaId,
            EsPrimario = entity.EsPrimario,
            Guid = entity.Guid,
            Nombre = entity.Nombre,
            Presupuesto = entity.Presupuesto
        };

        public static List<SubcategoriaDto> ToDtos(this List<Subcategoria> entidades) => entidades.Select(x => x.ToDto()).ToList();

        internal static MovimientoDto ToDto(this Movimiento entidad) => entidad is null ? null : new MovimientoDto
        {
            Monto = entidad.Cantidad,
            Concepto = entidad.Concepto,
            FechaDeRegistro = entidad.FechaDeRegistro,
            SaldoFinal = entidad.SaldoFinal,
            SaldoInicial = entidad.SaldoInicial,
            Guid = entidad.EncodedKey,
            Tipo = entidad.Tipo,
        };

        internal static List<MovimientoDto> ToDtos(this List<Movimiento> entidades) => entidades.Select(x => x.ToDto()).ToList();

        internal static Movimiento ToEntity(this MovimientoDtoIn movimiento) => new Movimiento
        {
            Cantidad = movimiento.Monto,
            Concepto = movimiento.Concepto,
            EncodedKey = movimiento.Encodedkey,
            FechaDeRegistro = DateTime.Now,            
        };

        internal static VersionDto ToDto(this VersionDePresupuesto entidad) => entidad is null ? null : new VersionDto
        {
            FechaFinal = entidad.FechaFinal,
            FechaInicial = entidad.FechaInicial,
            Guid = entidad.Guid,
            Id = entidad.Id,
            Nombre = entidad.Nombre
        };

        internal static List<VersionDto> ToDtos(this List<VersionDePresupuesto> entidades)
            => entidades.Select(x => x.ToDto()).ToList();

        internal static VersionDePresupuesto ToEntity(this VersionDtoIn dto) => new VersionDePresupuesto
        {
            FechaFinal = dto.FechaFinal,
            FechaDeRegistro = DateTime.Now,
            EstaActivo = true,
            FechaInicial = dto.FechaInicial,
            Guid = dto.Guid,
            Nombre = dto.Nombre
        };

        internal static PresupuestoDto ToDto(this Presupuesto entity) => entity is null ? null : new PresupuestoDto
        {
            AhorroId = entity.AhorroId,
            //AhorroTipo = entity.AhorroTipo,
            Cantidad = entity.Cantidad,
            Guid = entity.Guid,
            Id = entity.Id,
            SubcategoriaId = entity.SubcategoriaId,
            //Subcategoria = entity.Subcategoria.ToDto(),
            VersionId = entity.VersionId
        };

        internal static List<PresupuestoDto> ToDtos(this List<Presupuesto> entities) => entities.Select(x => x.ToDto()).ToList();

        internal static Presupuesto ToEntity(this PresupuestoDtoIn dtos) => new Presupuesto
        {
            AhorroId = dtos.AhorroId,
            Cantidad = dtos.Cantidad,
            SubcategoriaId = dtos.SubcategoriaId,
            Guid = dtos.Guid,
            VersionId = dtos.VersionId
        };

        internal static PeriodoDto ToDto(this Periodo entity) => entity is null ? null : new PeriodoDto
        {
            FechaFinal = entity.FechaFinal,
            FechaInicial = entity.FechaInicial,
            Guid = entity.Guid,
            Id = entity.Id,
            Nombre = entity.Nombre,
            VersionId = entity.VersionId
        };

        internal static Periodo ToEntity(this PeriodoDtoIn dtoIn) => new Periodo
        {
            FechaInicial = dtoIn.FechaInicial,
            FechaFinal = dtoIn.FechaFinal,
            VersionId = dtoIn.VersionId,
            EstaActivo = true,
            FechaDeRegistro = DateTime.Now,
            Guid = dtoIn.Guid,
            Nombre = dtoIn.Nombre
        };

        internal static PresupuestoDelPeriodoDto ToDto(this PresupuestoDelPeriodo entity) => entity is null ? null : new PresupuestoDelPeriodoDto
        {
            AhorroId = entity.AhorroId,
            Cantidad = entity.Cantidad,
            Gastado = entity.Gastado,
            Id = entity.Id,
            PeriodoId = entity.PeriodoId,
            PresupuestoId = entity.PresupuestoId
        };

        internal static List<PresupuestoDelPeriodoDto> ToDtos(this List<PresupuestoDelPeriodo> entities) => entities.Select(x => x.ToDto()).ToList();


        internal static CompraTarjetaDeCredito ToEntity(this CompraTdcDtoIn dto) => new CompraTarjetaDeCredito
        {
            Encodedkey = dto.Encodedkey,
            EstaActivo = true,
            FechaDeRegistro = DateTime.Now,
            MesesSinIntereses = dto.MesesSinIntereses,
            Monto = dto.Monto,
            Nombre = dto.Nombre,
            Nota = dto.Nota,
            Saldo = dto.Monto,
            FechaDeCompra = dto.FechaDeCompra
        };

        internal static CompraTdcDto ToDto(this CompraTarjetaDeCredito entity) => entity is null ? null : new CompraTdcDto
        {
            Encodedkey = entity.Encodedkey,
            MesesSinIntereses = entity.MesesSinIntereses,
            Monto = entity.Monto,
            Nombre = entity.Nombre,
            Nota = entity.Nota,
            Saldo = entity.Saldo,
            FechaDeCompra = entity.FechaDeCompra,
            FechaDeRegistro = entity.FechaDeRegistro,
            FechaDePago = entity.FechaDePago,
            Id = entity.Id
        };

        internal static List<CompraTdcDto> ToDtos(this List<CompraTarjetaDeCredito> entities) => entities.Select(x => x.ToDto()).ToList();

        internal static PagoTarjetaDeCredito ToEntity(this PagoTdcDtoIn dto) => new PagoTarjetaDeCredito
        {
            CompraTarjetaDeCreditoEncodedkey = dto.CompraTdcIdEndodedkey,
            Encodekey = dto.Encodedkey,
            FechaDeRegistro = DateTime.Now,
            Monto = dto.Monto
        };

        internal static PagoTdcDto ToDto(this PagoTarjetaDeCredito entity) => new PagoTdcDto
        {
            CompraTdcEndodedkey = entity.CompraTarjetaDeCreditoEncodedkey,
            CompraTdcId = entity.CompraTarjetaDeCreditoId,
            Encodedkey = entity.Encodekey,
            Monto = entity.Monto,
            FechaDeRegistro = entity.FechaDeRegistro,
            Id = entity.Id,
            Nota = entity.Nota
        };

        internal static List<PagoTdcDto> ToDtos(this List<PagoTarjetaDeCredito> entities) => entities.Select(x => x.ToDto()).ToList();
    }
}
