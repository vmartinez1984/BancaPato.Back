using Banca.Api.Bl;
using Banca.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using static Banca.Core.Dtos.PresupuestoDto;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionesController : BancaBase
    {
        public VersionesController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
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
        public async Task<IActionResult> ObtenerPresupuestos(string versionIdGuid)
        {
            VersionDto version;

            version = await _unitOfWork.Version.ObtenerAsync(versionIdGuid);           

            return Ok(version.Presupuestos);
        }

        [HttpPost("{versionIdGuid}/Presupuestos")]
        public async Task<IActionResult> AgregarPresupuesto(string versionIdGuid, PresupuestoDtoIn presupuesto)
        {
            IdDto id;

            id = await _unitOfWork.Presupuesto.AgregarAsync(versionIdGuid, presupuesto);

            return Created("", id);
        }

        [HttpPut("{versionIdGuid}/Presupuestos/{presupuestoIdGuid}")]
        public async Task<IActionResult> ActualizarPresupuesto(string versionIdGuid, string presupuestoIdGuid, PresupuestoDtoIn presupuesto)
        {
            await _unitOfWork.Presupuesto.ActualizarAsync(versionIdGuid, presupuestoIdGuid, presupuesto);

            return Accepted("", new IdDto { });
        }

        [HttpDelete("{versionIdGuid}/Presupuestos/{presupuestoIdGuid}")]
        public async Task<IActionResult> BorrarPresupuesto(string versionIdGuid, string presupuestoIdGuid)
        {
            await _unitOfWork.Presupuesto.BorrarAsync(versionIdGuid, presupuestoIdGuid);

            return Accepted("", new IdDto { });
        }
    }
}
//HL 8361 G