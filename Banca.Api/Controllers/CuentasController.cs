using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AhorrosController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string _ahorroFondeador;

        public AhorrosController(
            UnitOfWork unitOfWork,
            IConfiguration configuration
        )
        {
            this._unitOfWork = unitOfWork;
            _ahorroFondeador = configuration.GetSection("AhorroFondeadorGuid").Value;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(AhorroDtoIn cuenta)
        {
            IdDto id;

            id = await _unitOfWork.Ahorro.AgregarAsync(cuenta);

            return Created($"Ahorros/{id.Id}", id);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {

            List<AhorroDto> lista;

            lista = await _unitOfWork.Ahorro.ObtenerAsync();

            HttpContext.Response.Headers.Append("TotalDeRegistros", lista.Count().ToString());
            return Ok(lista.OrderBy(x => x.Nombre));
        }

        [HttpGet("Fondeador")]
        public async Task<IActionResult> ObtenerPrincipal()
        {
            AhorroDto ahorro;

            ahorro = await _unitOfWork.Ahorro.ObtenerAsync(_ahorroFondeador);

            return Ok(ahorro);
        }

        [HttpGet("{ahorroId}")]
        public async Task<IActionResult> Obtener(string ahorroId)
        {
            AhorroDto ahorro;

            ahorro = await _unitOfWork.Ahorro.ObtenerAsync(ahorroId);

            return Ok(ahorro);
        }

        [HttpGet("{ahorroId}/Movimientos")]
        public async Task<IActionResult> ObtenerMovimientos(string ahorroId)
        {
            List<MovimientoDto> lista;

            lista = await _unitOfWork.Ahorro.ObtenerMovimientosAsync(ahorroId);

            return Ok(lista);
        }

        [HttpPut("{ahorroId}")]
        public async Task<IActionResult> ActualizarAhorro(string ahorroId, AhorroDtoIn ahorro)
        {

            await _unitOfWork.Ahorro.ActualizarAsync(ahorroId, ahorro);

            return Accepted();
        }

        [HttpDelete("{ahorroId}")]
        public async Task<IActionResult> Borrar(string ahorroId)
        {
            await _unitOfWork.Ahorro.BorrarAsync(ahorroId);

            return Accepted();
        }

        [HttpPost("{cuentaIdGuid}/depositos")]
        public async Task<IActionResult> Depositar(string cuentaIdGuid, MovimientoDtoIn deposito)
        {
            IdDto id;

            id = await _unitOfWork.Ahorro.DepositarAsync(cuentaIdGuid, deposito);

            return Created("", id);
        }

        [HttpPost("{cuentaIdGuid}/retiros")]
        public async Task<IActionResult> Retirar(string cuentaIdGuid, MovimientoDtoIn retiro)
        {
            IdDto id;

            id = await _unitOfWork.Ahorro.RetirarAsync(cuentaIdGuid, retiro);

            return Created("", id);
        }
    }
}
