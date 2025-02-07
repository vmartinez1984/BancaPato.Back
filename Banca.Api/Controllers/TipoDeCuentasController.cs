using Banca.Api.Bl;
using Banca.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeCuentasController : BancaBase
    {
        public TipoDeCuentasController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            List<TipoDeCuentaDto> lista;

            lista = await _unitOfWork.TipoDeCuenta.ObtenerTodosAsync();

            return Ok(lista);
        }
    }
}
