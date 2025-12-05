using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Bl;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasTarjetaDeCreditoController : ControllerBase
    {
        private readonly CompraTarjetaDeCreditoBl _compraBl;

        public ComprasTarjetaDeCreditoController(CompraTarjetaDeCreditoBl compraTarjetaDeCreditoBl)
        {
            this._compraBl = compraTarjetaDeCreditoBl;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAsync(CompraTdcDtoIn compra)
        {
            var resultado = await _compraBl.AgregarAsync(compra);

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosAsync()
        {
            var resultado = await _compraBl.ObtenerTodosAsync();
            return Ok(resultado);
        }
    }
}
