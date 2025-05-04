using Banca.Core.Dtos;
using JwtTokenService;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioDeSesionesController(JwtToken jwtToken) : ControllerBase
    {
        private readonly JwtToken _jwtToken = jwtToken;

        /// <summary>
        /// Retorna token
        /// </summary>
        /// <param name="inicioDeSesion"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult IniciarSesion(InicioDeSesionDto inicioDeSesion)
        {
            if (inicioDeSesion.Usuario == "ahal_tocob@hotmail.com" && inicioDeSesion.Contraseña == "Macross#2012")
            {
                var minutos = 20;
                var fechaActual = DateTime.Now;
                var fechaDeExpiracion = fechaActual.AddMinutes(minutos);
                var token = _jwtToken.ObtenerToken(
                    "Víctor Mtz",
                    "Yo merengues",
                    "2025",
                    "ahal_tocob@hotmail.com",
                    fechaDeExpiracion
                );

                return Ok(new TokenDto
                {
                    FechaDeExpiracion = fechaDeExpiracion,
                    Token = token,
                    ExpiracionEnMinutos = minutos,
                    Fecha = fechaActual
                });
            }

            return NotFound(new { Mensaje = "No ni mergas" });
        }
    }
}
