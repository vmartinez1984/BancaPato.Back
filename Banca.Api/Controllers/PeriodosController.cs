using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodosController : BancaBase
    {
        private readonly string ahorroEje;
        public PeriodosController(UnitOfWork unitOfWork, IConfiguration configuration) : base(unitOfWork)
        {
            ahorroEje = configuration.GetSection("AhorroFondeadorGuid").Value;
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

        [HttpDelete("{periodoId}")]
        public async Task<IActionResult> BorrarAsync(string periodoId)
        {
            await _unitOfWork.Periodo.BorrarAsync(periodoId);

            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> AgregarPeriodo(PeriodoDtoIn periodo)
        {
            IdDto id;

            id = await _unitOfWork.Periodo.AgregarAsync(periodo);

            return Created(string.Empty, id);
        }

        [HttpPost("{periodoId}/Transacciones")]
        public async Task<IActionResult> AgregarTransaccion(int periodoId, TransaccionDtoIn movimiento)
        {            
            AhorroDto ahorro;
                     
            ahorro = await _unitOfWork.Ahorro.ObtenerAsync(ahorroEje);
            if (ahorro.Balance < movimiento.Cantidad)
            {
                return StatusCode(400, new { Mensaje = "No has sufuciente camarón" });
            }

            IdDto id;

            id = await _unitOfWork.Transaccion.AgregarAsync(periodoId, movimiento);

            return Created("", id);
        }

        [HttpGet("{periodoId}/Presupuestos")]
        public async Task<IActionResult> ObtenerMovimiento(int periodoId)
        {
            List<PresupuestoDto> lista;

            lista = await _unitOfWork.Periodo.ObtenerPresupestosDelPeriodoAsync(periodoId);

            return Ok(lista);
        }
    }
}
