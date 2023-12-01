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

}