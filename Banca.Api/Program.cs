using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Helpers;
using Banca.Api.Interfaces;
using Banca.Api.Repositories;
using Banca.BusinessLayer.Bl;
using Banca.BusinessLayer.Mappers;
using Banco.Repositorios.Entities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Lee configuración desde appsettings.json
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Guarda logs en un archivo    
    .WriteTo.MongoDB("mongodb://root:123456@localhost:27017/Logs?authMechanism=DEFAULT") // Configura MongoDB
    .CreateLogger();

// Reemplaza el logger predeterminado por Serilog
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddScoped<DuckBankContext>();
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<CuentaBl>();
builder.Services.AddScoped<TransaccionBl>();
builder.Services.AddScoped<HistorialBl>();
builder.Services.AddScoped<CategoriaBl>();
builder.Services.AddScoped<SubcategoriaBl>();
builder.Services.AddScoped<VersionBl>();
builder.Services.AddScoped<TipoDeCuentaBl>();
builder.Services.AddScoped<PresupuestoBl>();
builder.Services.AddScoped<PeriodoBl>();
builder.Services.AddScoped<MovimientoBl>();
//Repositorio MongoDb
builder.Services.AddScoped<ICategoryRepository, CategoriaRepository>();
builder.Services.AddScoped<ISubcategoriaRepository, SubcategoriaRepo>();
builder.Services.AddScoped<IGastosRepository, GastoRepository>();
builder.Services.AddScoped<IAhorroRepository, AhorrosRepository>();
builder.Services.AddScoped<ITipoDeCuentaRepository, TipoDeCuentaRepository>();
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IPeriodoRepository, PeriodoRepo>();
//Servicio a DuckbankMs
HttpClientHandler httpClientHandler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
};
builder.Services.AddHttpClient(string.Empty, c => { }).ConfigurePrimaryHttpMessageHandler(h => httpClientHandler);

builder.Services.AddScoped<AhorrosRepository>();

var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<BancaMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    )
);
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "/swagger/v1/swagger.json");
    x.RoutePrefix = "";
});

app.UseCors("AllowWebApp");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

// Asegúrate de cerrar el logger al final del programa
Log.CloseAndFlush();