using Banca.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banco.Repositorios.Entities;

public partial class DuckBankContext : DbContext
{
    private readonly string connectionString;

    public DuckBankContext(IConfiguration configuration)
    {
        this.connectionString = configuration.GetConnectionString("SqlServer");
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<HistorialDeApartado> HistorialDeApartados { get; set; }

    public virtual DbSet<Movimiento> Movimiento { get; set; }

    public virtual DbSet<Periodo> Periodo { get; set; }

    public virtual DbSet<Presupuesto> Presupuesto { get; set; }

    public virtual DbSet<Subcategorium> Subcategoria { get; set; }

    public virtual DbSet<TipoDeCuenta> TipoDeCuenta { get; set; }
    public virtual DbSet<Transaccion> Transaccion { get; set; }

    public virtual DbSet<VersionDePresupuesto> VersionDePresupuesto { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //string connectionString;

        //connectionString = "Data Source=192.168.1.86;Initial Catalog=DuckBank; Persist Security Info=True;User ID=sa;Password=Macross#2012; TrustServerCertificate=True;";
        //connectionString = "Data Source=wdb4.my-hosting-panel.com;Initial Catalog=vmartinez_DuckBank; Persist Security Info=True;User ID=vmartinez_DuckBank;Password=Macross#2012; TrustServerCertificate=True;";
        
        optionsBuilder.UseSqlServer(this.connectionString);
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Categorium>(entity =>
    //    {
    //        entity.Property(e => e.Nombre)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Cuentum>(entity =>
    //    {
    //        entity.Property(e => e.Clabe)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Interes).HasColumnType("decimal(2, 2)");
    //        entity.Property(e => e.Nombre)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Nota)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<HistorialDeApartado>(entity =>
    //    {
    //        entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
    //        entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
    //        entity.Property(e => e.Nota)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.Cuenta).WithMany(p => p.HistorialDeApartados)
    //            .HasForeignKey(d => d.CuentaId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_HistorialDeApartados_Cuenta");
    //    });

    //    modelBuilder.Entity<Movimiento>(entity =>
    //    {
    //        entity.ToTable("Movimiento");

    //        entity.Property(e => e.Nota)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.Periodo).WithMany(p => p.Movimientos)
    //            .HasForeignKey(d => d.PeriodoId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Movimiento_Periodo");

    //        entity.HasOne(d => d.Presupuesto).WithMany(p => p.Movimientos)
    //            .HasForeignKey(d => d.PresupuestoId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Movimiento_Presupuesto");

    //        entity.HasOne(d => d.Transaccion).WithMany(p => p.Movimientos)
    //            .HasForeignKey(d => d.TransaccionId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Movimiento_Transaccion");
    //    });

    //    modelBuilder.Entity<Periodo>(entity =>
    //    {
    //        entity.ToTable("Periodo");

    //        entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
    //        entity.Property(e => e.FechaFinal).HasColumnType("date");
    //        entity.Property(e => e.FechaInicial).HasColumnType("date");
    //        entity.Property(e => e.Nombre)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Nota)
    //            .HasMaxLength(255)
    //            .IsUnicode(false);
    //    });

    //    modelBuilder.Entity<Presupuesto>(entity =>
    //    {
    //        entity.ToTable("Presupuesto");

    //        entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
    //        entity.Property(e => e.Nombre)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.Subcategoria).WithMany(p => p.Presupuestos)
    //            .HasForeignKey(d => d.SubcategoriaId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Presupuesto_Subcategoria");

    //        entity.HasOne(d => d.Version).WithMany(p => p.Presupuestos)
    //            .HasForeignKey(d => d.VersionId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Presupuesto_Version");
    //    });

    //    modelBuilder.Entity<Subcategorium>(entity =>
    //    {
    //        entity.Property(e => e.Nombre)
    //            .HasMaxLength(50)
    //            .IsUnicode(false);
    //        entity.Property(e => e.Presupuesto).HasColumnType("decimal(10, 2)");

    //        entity.HasOne(d => d.Categoria).WithMany(p => p.Subcategoria)
    //            .HasForeignKey(d => d.CategoriaId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Subcategoria_Categoria");
    //    });

    //    modelBuilder.Entity<Transaccion>(entity =>
    //    {
    //        entity.ToTable("Transaccion");

    //        entity.Property(e => e.Id).ValueGeneratedNever();
    //        entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
    //        entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
    //        entity.Property(e => e.Tipo)
    //            .HasMaxLength(20)
    //            .IsUnicode(false);

    //        entity.HasOne(d => d.Cuenta).WithMany(p => p.Transaccions)
    //            .HasForeignKey(d => d.CuentaId)
    //            .OnDelete(DeleteBehavior.ClientSetNull)
    //            .HasConstraintName("FK_Transaccion_Cuenta");
    //    });

    //    modelBuilder.Entity<Version>(entity =>
    //    {
    //        entity.ToTable("Version");

    //        entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
    //        entity.Property(e => e.FechaFinal).HasColumnType("date");
    //        entity.Property(e => e.FechaInicial).HasColumnType("date");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
