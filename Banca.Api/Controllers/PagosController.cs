using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Bl;
using Gastos.ReglasDeNegocio.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/TarjetaDeCredito/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly PagoDeTarjetaDeCreditoBl _pagoBl;

        public PagosController(PagoDeTarjetaDeCreditoBl pagoDeTarjetaDeCredito)
        {
            _pagoBl =pagoDeTarjetaDeCredito;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAsync(PagoTdcDtoIn pagoTdc)
        {
            var resultado = await _pagoBl.AgregarAsync(pagoTdc);

            return Created("", resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodosPorCompraIdEncodedkeyAsync(string idEncodedkey) 
        {
            List<PagoTdcDto> pagos;

            pagos = await _pagoBl.ObtenerTodosPorCompraIdEncodedkeyAsync(idEncodedkey);

            return Ok(pagos);
        }
    }
}
