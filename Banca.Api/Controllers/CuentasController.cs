using Banca.Api.Bl;
using Banca.Api.Dtos;
using Banca.Comun.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly string _ahorroFondeador;

        public CuentasController(
            UnitOfWork unitOfWork,
            IConfiguration configuration
        )
        {
            this._unitOfWork = unitOfWork;
            _ahorroFondeador = configuration.GetSection("AhorroFondeadorGuid").Value;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(CuentaDtoIn cuenta)
        {
            IdDto id;

            id = await _unitOfWork.Cuenta.AgregarAsync(cuenta);

            return Created($"Cuentas/{id.Id}", id);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {

            List<AhorroDto> lista;

            lista = await _unitOfWork.Cuenta.Obtener();

            return Ok(lista.OrderBy(x => x.Nombre));
        }

        [HttpGet("Fondeador")]
        public async Task<IActionResult> ObtenerPrincipal()
        {
            AhorroDto ahorro;

            ahorro = await _unitOfWork.Cuenta.ObtenerAsync(_ahorroFondeador);

            return Ok(ahorro);
        }

        [HttpGet("{ahorroId}")]
        public async Task<IActionResult> Obtener(string ahorroId)
        {
            AhorroDto ahorro;

            ahorro = await _unitOfWork.Cuenta.ObtenerAsync(ahorroId);

            return Ok(ahorro);
        }

        [HttpPut("{ahorroId}")]
        public async Task<IActionResult> ActualizarAhorro(string ahorroId, CuentaDtoIn ahorro)
        {

            await _unitOfWork.Cuenta.ActualizarAsync(ahorroId, ahorro);

            return Accepted();
        }

        [HttpDelete("{ahorroId}")]
        public async Task<IActionResult> Borrar(string ahorroId)
        {
            await _unitOfWork.Cuenta.BorrarAsync(ahorroId);

            return Accepted();
        }

        [HttpPost("{cuentaIdGuid}/depositos")]
        public async Task<IActionResult> Depositar(string cuentaIdGuid, DepositoDtoIn deposito)
        {
            string id;

            id = await _unitOfWork.Transaccion.Depositar(cuentaIdGuid, deposito);

            return Created("", new { Id = id });
        }

        [HttpPost("{cuentaIdGuid}/retiros")]
        public async Task<IActionResult> Retirar(string cuentaIdGuid, RetiroDtoIn retiro)
        {
            string id;

            id = await _unitOfWork.Transaccion.Retirar(cuentaIdGuid, retiro);

            return Created("", new { Id = id });
        }
    }
}
