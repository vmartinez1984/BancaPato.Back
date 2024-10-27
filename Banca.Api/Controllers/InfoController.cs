using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<InfoController> logger;

        public InfoController(IConfiguration configuration, ILogger<InfoController> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        [HttpGet("Ambiente")]
        public IActionResult Get()
        {
            string ambiente = configuration.GetSection("Ambiente").Value;
            logger.LogInformation($"Ambiente: {ambiente}");

            return Ok( new { Ambiente = ambiente });
        }

        [HttpDelete("Errores")]
        public IActionResult Delete(string error)
        {
            if(string.IsNullOrEmpty(error)) 
                error = Guid.NewGuid().ToString();
            throw new Exception(error);
        }
    }
}
