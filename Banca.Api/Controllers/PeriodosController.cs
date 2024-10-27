using Banca.Api.Bl;
using Banca.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodosController : BancaBase
    {
        public PeriodosController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            List<PeriodoDto> periodos;

            periodos = await _unitOfWork.Periodo.ObtenerTodosAsync();

            return Ok(periodos);
        }

        [HttpGet("{periodoId}")]
        public async Task<IActionResult> Obtener(string periodoId)
        {
            PeriodoDto periodos;

            periodos = await _unitOfWork.Periodo.ObtenerAsync(periodoId);

            return Ok(periodos);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPeriodo(PeriodoDtoIn periodo)
        {
            IdDto id;

            id = await _unitOfWork.Periodo.AgregarAsync(periodo);

            return Created(string.Empty, id);
        }

        [HttpPost("{periodoIdGuid}/Movimientos")]
        public async Task<IActionResult> AgregarMovimiento(string periodoIdGuid, MovimientoDtoIn movimiento)
        {
            IdDto id;

            id = await _unitOfWork.Movimiento.AgregarAsync(periodoIdGuid, movimiento);

            return Created("", id);
        }

        [HttpGet("{periodoIdGuid}/Movimientos")]
        public async Task<IActionResult> ObtenerMovimiento(string periodoIdGuid)
        {
            List<MovimientoDto> lista;

            lista = await _unitOfWork.Movimiento.ObtenerTodosAsync(periodoIdGuid);

            return Ok(lista);
        }
    }
}
