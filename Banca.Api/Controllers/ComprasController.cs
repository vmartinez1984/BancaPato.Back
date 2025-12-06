using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Bl;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/TarjetaDeCredito/Compras")]
    [ApiController]
    public class ComprasTarjetaDeCreditoController : ControllerBase
    {
        private readonly CompraTarjetaDeCreditoBl _compraBl;

        public ComprasTarjetaDeCreditoController(CompraTarjetaDeCreditoBl compraTarjetaDeCreditoBl)
        {
            this._compraBl = compraTarjetaDeCreditoBl;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<CompraTdcDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> AgregarAsync(CompraTdcDtoIn compra)
        {
            var resultado = await _compraBl.AgregarAsync(compra);

            return Created("",resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosAsync()
        {
            var resultado = await _compraBl.ObtenerTodosAsync();
            var fechaActual = DateTime.Now;
            var fechaMesSiguiente = DateTime.Now.AddMonths(1);
            this.HttpContext.Response.Headers.Add("pagoMesActual", resultado.Where(x => x.FechaDePago.Month == fechaActual.Month && x.FechaDePago.Year == fechaActual.Year).Sum(x => x.Saldo).ToString());
            this.HttpContext.Response.Headers.Add("pagoMesSiguiente", resultado.Where(x => x.FechaDePago.Month == fechaMesSiguiente.Month && x.FechaDePago.Year == fechaMesSiguiente.Year).Sum(x => x.Saldo).ToString());
            this.HttpContext.Response.Headers.Add("total", resultado.Sum(x => x.Saldo).ToString());

            return Ok(resultado);
        }
    }
}
