using Banca.Api.Repositories;
using DuckBank.Persistence.Helpers;
using Gastos.ReglasDeNegocio.Bl;
using Gastos.ReglasDeNegocio.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Gastos.ReglasDeNegocio.Helpers
{
    public static class Extensor
    {
        public static void AgregarGastos(this IServiceCollection services)
        {
            services.AddScoped<AhorroBl>();
            services.AddScoped<CategoriaBl>();
            services.AddScoped<SubcategoriaBl>();
            services.AddScoped<TipoDeAhorroBl>();
            services.AddScoped<VersionBl>();
            services.AddScoped<PeriodoBl>();
            services.AddScoped<TransaccionBl>();
            services.AddScoped<PresupuestoBl>();
            services.AddScoped<CompraTarjetaDeCreditoBl>();

            services.AgregarDuckBank();
            services.AddScoped<CategoriaRepositorio>();
            services.AddScoped<SubcategoriaRepo>();
            services.AddScoped<TipoDeCuentaRepository>();            
            services.AddScoped<VersionRepository>();
            services.AddScoped<PeriodoRepo>();
            services.AddScoped<PresupuestoRepositorio>();
            services.AddScoped<PresupuestoDelPeriodoRepositorio>();
            services.AddScoped<TransaccionRepositorio>();
            services.AddScoped<CompraTarjetaDeCreditoRepositorio>();

            services.AddScoped<Repositorio>();

            services.AddScoped<UnitOfWork>();
        }
    }
}
