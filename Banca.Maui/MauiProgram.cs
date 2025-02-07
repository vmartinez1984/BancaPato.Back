using Banca.Maui.Pages;
using Banca.Maui.Services;
using Microsoft.Extensions.Logging;

namespace Banca.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            string baseUrl;

            baseUrl = "https://gastos-api-v4.vmartinez84.xyz/api/";
            //baseUrl = "https://localhost:7275/api/";
            builder.Services.AddHttpClient(string.Empty, client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
            builder.Services.AddSingleton<TipoDeCuentaService>();            
            builder.Services.AddSingleton<AhorroService>();
            builder.Services.AddSingleton<Servicio>();
            builder.Services.AddSingleton<PeriodoService>();

            builder.Services.AddSingleton<AhorrosPage>();
            builder.Services.AddSingleton<PeriodosPage>();

            return builder.Build();
        }
    }
}
