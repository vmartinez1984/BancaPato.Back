using Banca.Api.Bl;
using Banca.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : BancaBase
    {
        public CategoriasController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
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
