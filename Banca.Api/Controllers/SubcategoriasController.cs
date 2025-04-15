using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriasController : ControllerBase// BancaBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SubcategoriasController(UnitOfWork unitOfWork) //: base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene la lista de subcategorias
        /// </summary>
        /// <response code="200">Listad de Subcategorias</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<SubcategoriaDto>), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            var lista = await _unitOfWork.Subcategoria.ObtenerAsync();
            return Ok(lista.OrderBy(x => x.Nombre));
        }

        /// <summary>
        /// Obtiene Subcategoria
        /// </summary>
        /// <response code="200">Subcategoria</response>
        [HttpGet("{idGuid}")]
        [ProducesResponseType(typeof(SubcategoriaDto), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string idGuid)
        {
            SubcategoriaDto subcategoria;

            subcategoria = await _unitOfWork.Subcategoria.ObtenerAsync(idGuid);
            if (subcategoria == null)
            {
                return NotFound(new { Mensaje = "Elemento no encontrado" });
            }

            return Ok(subcategoria);
        }

        /// <summary>
        /// Agregar una subcategoria
        /// </summary>
        /// <param name="subcategoria"></param>
        /// <response code="200">Id Guid</response>        
        [HttpPost]
        [ProducesResponseType(typeof(IdDto), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] SubcategoriaDtoIn subcategoria)
        {
            IdDto id;
            SubcategoriaDto subcategoriaDto = null;

            if (subcategoria.Guid != null)
                subcategoriaDto = await _unitOfWork.Subcategoria.ObtenerAsync(subcategoria.Guid.ToString());
            if (subcategoriaDto == null)
            {
                if (ModelState.IsValid)
                {
                    id = await _unitOfWork.Subcategoria.AgregarAsync(subcategoria);

                    return Created($"Subcategorias/{id.Id}", id);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                id = new IdDto
                {
                    Id = subcategoriaDto.Id,
                    Guid = subcategoriaDto.Guid.ToString()
                };

                return Ok(id);
            }

        }

        /// <summary>
        /// Actualiza subcategoria
        /// </summary>
        /// <param name="subcategoria"></param>
        /// <param name="idGuid"></param> 
        /// <response code="202">Actualización con exito</response>
        [HttpPut("{idGuid}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromBody] SubcategoriaDtoIn subcategoria, string idGuid)
        {
            SubcategoriaDto subcategoriaDto;

            subcategoriaDto = await _unitOfWork.Subcategoria.ObtenerAsync(idGuid);
            if (subcategoriaDto == null)
            {
                return NotFound(new { Mensaje = "No se encontro la subcategoria" });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Subcategoria.ActualizarAsync(subcategoria, idGuid);

                    return Accepted(new { Mensaje = "registro actualizado" });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
        }

        /// <summary>
        /// Eliminar subcategoria
        /// </summary>
        /// <param name="idGuid"></param>
        /// <returns></returns>
        [HttpDelete("{idGuid}")]
        public async Task<IActionResult> Delete(string idGuid)
        {
            SubcategoriaDto subcategoriaDto;

            subcategoriaDto = await _unitOfWork.Subcategoria.ObtenerAsync(idGuid);
            if (subcategoriaDto == null)
                return NotFound(new { Mensaje = "Elemento no encontrado" });
            await _unitOfWork.Subcategoria.BorrarAsync(idGuid);

            return Accepted(new { Mensaje = "Elemento borrado" });
        }
    }
}
