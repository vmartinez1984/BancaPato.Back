using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase //BancaBase
    {
        private readonly UnitOfWork _unitOfWork;
        
        public CategoriasController(UnitOfWork unitOfWork) //: base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            List<CategoriaDto> lista;

            lista = await _unitOfWork.Categoria.ObtenerAsync();

            return Ok(lista);
        }
    }
}
