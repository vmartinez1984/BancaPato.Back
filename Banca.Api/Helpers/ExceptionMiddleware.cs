using Microsoft.Extensions.Configuration;

namespace Banca.Api.Helpers
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly bool _mostrarError;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IConfiguration configuration
        )
        {
            this.next = next;
            this.logger = logger;
            _mostrarError = bool.Parse(configuration.GetSection("MostrarErrores").Value);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                EventId eventId = new EventId(0, Guid.NewGuid().ToString());
                logger.LogError(eventId, exception, exception.Message);
                logger.LogError(exception, exception.Message);
                context.Response.StatusCode = 500;
                if (_mostrarError)
                    await context.Response.WriteAsJsonAsync(exception);
                else
                    await context.Response.WriteAsJsonAsync(new { Mensaje = "El minge ya esta reiniciado el servidor", Id = eventId.Name });
                // DO SOMETHING
            }
        }
    }
}
