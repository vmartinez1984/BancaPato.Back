using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresupuestosController : BancaBase
{
    public PresupuestosController(UnitOfWork unitOfWork) : base(unitOfWork) { }

    [HttpGet("{presupuestoId}")]
    public async Task<IActionResult> ObtenerPresupuestos(int presupuestoId)
    {
        PresupuestoDto lista;

        lista = await _unitOfWork.Presupuesto.ObtenerAsync(presupuestoId);

        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> AgregarPresupuesto(PresupuestoDtoIn presupuesto)
    {
        IdDto id;

        id = await _unitOfWork.Presupuesto.AgregarAsync(presupuesto);

        return Created("", id);
    }

    [HttpPut("{presupuestoId}")]
    public async Task<IActionResult> ActualizarPresupuesto(int presupuestoIdGuid, PresupuestoDtoIn presupuesto)
    {
        await _unitOfWork.Presupuesto.ActualizarAsync(presupuestoIdGuid, presupuesto);

        return Accepted("", new IdDto { });
    }

    [HttpDelete("{presupuestoId}")]
    public async Task<IActionResult> BorrarPresupuesto(string presupuestoIdGuid)
    {
        await _unitOfWork.Version.BorrarAsync(presupuestoIdGuid);

        return Accepted("", new IdDto { });
    }
}