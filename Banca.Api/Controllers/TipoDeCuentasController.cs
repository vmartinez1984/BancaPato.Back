using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDeAhorrosController : ControllerBase //: BancaBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TiposDeAhorrosController(UnitOfWork unitOfWork) //: base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            List<TipoDeAhorroDto> lista;

            lista = await _unitOfWork.TipoDeAhorro.ObtenerTodosAsync();

            return Ok(lista);
        }
    }
}
