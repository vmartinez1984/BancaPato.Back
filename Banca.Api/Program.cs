using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Interfaces;
using Banca.Api.Repositories;
using Banca.BusinessLayer.Mappers;
using Gastos.ReglasDeNegocio.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configuración de archivos de appsettings según el entorno
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<TransaccionBl>();
builder.Services.AddScoped<HistorialBl>();
builder.Services.AddScoped<VersionBl>();
builder.Services.AddScoped<PresupuestoBl>();
builder.Services.AddScoped<PeriodoBl>();
builder.Services.AddScoped<MovimientoBl>();
//Repositorio MongoDb
//builder.Services.AddScoped<ICategoryRepository, CategoriaRepository>();
//builder.Services.AddScoped<ISubcategoriaRepository, SubcategoriaRepo>();
builder.Services.AddScoped<IGastosRepository, GastoRepository>();
//builder.Services.AddScoped<ITipoDeCuentaRepository, TipoDeCuentaRepository>();
builder.Services.AddScoped<IVersionRepository, VersionRepository>();
builder.Services.AddScoped<IPeriodoRepository, PeriodoRepo>();

builder.Services.AgregarGastos();

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

//app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();