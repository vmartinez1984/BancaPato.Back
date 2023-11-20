using AutoMapper;
using Banca.Api.Bl;
using Banca.BusinessLayer.Bl;
using Banca.BusinessLayer.Mappers;
using Banco.Repositorios.Entities;

var builder = WebApplication.CreateBuilder(args);

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

var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<BancaMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
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
app.UseSwaggerUI();

app.UseCors("AllowWebApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();