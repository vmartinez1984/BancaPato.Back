﻿using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Banca.Api.Repositories;
public class PeriodoRepo : BaseRepositorio
{
    private readonly IMongoCollection<Periodo> _collection;

    public PeriodoRepo(IConfiguration configurations) : base(configurations)
    {
        _collection = _mongoDatabase.GetCollection<Periodo>("Periodos");
    }

    public async Task<List<Periodo>> ObtenerAsync(bool estaActivo = true) => await _collection.Find(x => x.EstaActivo == estaActivo).ToListAsync();


    private async Task<int> ObtenerId()
    {
        var item = await
        _collection
         .Find(new BsonDocument()) // Puedes agregar filtros si es necesario
        .SortByDescending(r => r.Id) // Ordenar por fecha de forma descendente
        .FirstOrDefaultAsync();
        ;
        if (item == null)
            return 1;

        return item.Id + 1;
    }

    public async Task<int> AgregarAsync(Periodo entidad)
    {
        if (entidad.Id == 0)
            entidad.Id = await ObtenerId();

        await _collection.InsertOneAsync(entidad);

        return entidad.Id;
    }

    public async Task<Periodo> ObtenerAsync(string idGuid)
    {
        int id;

        if (int.TryParse(idGuid, out id))
            return (await _collection.FindAsync(x => x.Id == id)).FirstOrDefault();
        else
            return (await _collection.FindAsync(x => x.Guid == idGuid)).FirstOrDefault();
    }

    public async Task ActualizarAsinc(Periodo periodo) => await _collection.ReplaceOneAsync(x => x._id == periodo._id, periodo);

}
