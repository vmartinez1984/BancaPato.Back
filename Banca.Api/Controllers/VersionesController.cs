using Banca.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Gastos.ReglasDeNegocio;

namespace Banca.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VersionesController : ControllerBase //: BancaBase
{
    private readonly UnitOfWork _unitOfWork;

    public VersionesController(UnitOfWork unitOfWork) //: base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> AgregarVersion(VersionDtoIn version)
    {
        IdDto id;

        id = await _unitOfWork.Version.AgregarAsync(version);

        return Created("", id);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerVersiones()
    {
        List<VersionDto> lista;

        lista = await _unitOfWork.Version.ObtenerAsync();

        return Ok(lista);
    }

    [HttpGet("{versionId}")]
    public async Task<IActionResult> ObtenerPorIdAsync(int versionId)
    {
        VersionDto lista;

        lista = await _unitOfWork.Version.ObtenerAsync(versionId);

        return Ok(lista);
    }

    [HttpPut("{versionIdGuid}")]
    public async Task<IActionResult> ActualizarVersion(string versionIdGuid, VersionDtoIn version)
    {
        await _unitOfWork.Version.ActualizarAsync(versionIdGuid, version);

        return Accepted();
    }

    [HttpDelete("{versionIdGuid}")]
    public async Task<IActionResult> BorrarVersion(string versionIdGuid)
    {
        await _unitOfWork.Version.BorrarAsync(versionIdGuid);

        return Accepted();
    }

    [HttpGet("{versionIdGuid}/Presupuestos")]
    public async Task<IActionResult> ObtenerPresupuestos(int versionIdGuid)
    {
        List<PresupuestoDto> lista;

        lista = await _unitOfWork.Presupuesto.ObtenerTodosAsync(versionIdGuid);

        return Ok(lista);
    }
}


//HL 8361 G