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

            services.AgregarDuckBank();
            services.AddScoped<CategoriaRepositorio>();
            services.AddScoped<SubcategoriaRepo>();
            services.AddScoped<TipoDeCuentaRepository>();            
            services.AddScoped<VersionRepository>();

            services.AddScoped<Repositorio>();

            services.AddScoped<UnitOfWork>();
        }
    }
}
