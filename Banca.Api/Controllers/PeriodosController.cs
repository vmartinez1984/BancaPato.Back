using Banca.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AgregarPeriodo()
        {
            

            return Created("", new IdDto
            {

            });
        }

        [HttpPost("{periodoIdGuid}/Movimientos")]
        public async Task<IActionResult> AgregarMovimiento(string periodoIdGuid, MovimientoIn movimiento)
        {
            return Created("", new IdDto
            {

            });
        }
    }
}
