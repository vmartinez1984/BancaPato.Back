using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Entities;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl;

public class VersionBl //: BaseBl
{
    private readonly Repositorio _repositorioMongo;

    public VersionBl(Repositorio repositorio)
    {
        _repositorioMongo = repositorio;
    }

    public async Task ActualizarAsync(string versionIdGuid, VersionDtoIn version)
    {
        VersionDePresupuesto entity;

        entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
        entity.FechaInicial = version.FechaInicial;
        entity.FechaFinal = version.FechaFinal;
        entity.Nombre = version.Nombre;

        await _repositorioMongo.Version.ActualizarAsync(entity);
    }

    public async Task<IdDto> AgregarAsync(VersionDtoIn version)
    {
        int id;

        if (version.Guid == null)
            version.Guid = Guid.NewGuid().ToString();
        id = await _repositorioMongo.Version.AgregarAsync(version.ToEntity());

        return new IdDto { Guid = version.Guid.ToString(), Id = id };
    }

    public async Task<List<VersionDto>> ObtenerAsync() => (await _repositorioMongo.Version.ObtenerTodosAsync()).ToDtos();

    public async Task BorrarAsync(string versionIdGuid)
    {
        VersionDePresupuesto entity;

        entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);
        entity.EstaActivo = false;

        await _repositorioMongo.Version.ActualizarAsync(entity);
    }

    internal async Task<VersionDto> ObtenerAsync(string versionIdGuid)
    {
        VersionDePresupuesto entity;

        entity = await _repositorioMongo.Version.ObtenerAsync(versionIdGuid);

        return entity.ToDto();
    }

    public async Task<VersionDto> ObtenerAsync(int versionId) => (await _repositorioMongo.Version.ObtenerAsync(versionId.ToString())).ToDto();

}

public class PresupuestoBl
{
    private readonly Repositorio _repositorioMongo;

    public PresupuestoBl(Repositorio repositorio)
    {
        _repositorioMongo = repositorio;
    }

    public async Task<IdDto> AgregarAsync(PresupuestoDtoIn presupuesto)
    {
        Presupuesto entity;

        if (presupuesto.Guid == null)
            presupuesto.Guid = Guid.NewGuid().ToString();
        entity = presupuesto.ToEntity();
        await _repositorioMongo.Presupuesto.AgregarAsync(entity);

        return new IdDto { Id = entity.Id, Guid = entity.Guid.ToString() };
    }

    public async Task<List<PresupuestoDto>> ObtenerTodosAsync(int versionId) 
        => (await _repositorioMongo.Presupuesto.ObtenerPorVersionIdAsync(versionId)).ToDtos();

    public async Task<PresupuestoDto> ObtenerAsync(int presupuestoId) => (await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId)).ToDto();

    public async Task ActualizarAsync(int presupuestoId, PresupuestoDtoIn presupuesto)
    {
        Presupuesto presupuesto1;

        presupuesto1 = await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId);
        presupuesto1.AhorroId = presupuesto.AhorroId;
        presupuesto1.Cantidad = presupuesto.Cantidad;
        presupuesto1.VersionId = presupuesto.VersionId;
        presupuesto1.SubcategoriaId = presupuesto.SubcategoriaId;

        await _repositorioMongo.Presupuesto.ActualizarAsync(presupuesto1);
    }

    public async Task BorrarPresupuestoAsync(int presupuestoId)
    {
        Presupuesto presupuesto1;

        presupuesto1 = await _repositorioMongo.Presupuesto.ObtenerAsync(presupuestoId);
        presupuesto1.EstaActivo = false;

        await _repositorioMongo.Presupuesto.ActualizarAsync(presupuesto1);
    }
}