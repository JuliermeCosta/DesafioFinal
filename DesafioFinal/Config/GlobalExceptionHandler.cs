using DesafioFinal.Application.DTO;
using Microsoft.AspNetCore.Diagnostics;

namespace DesafioFinal.Config
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

            var mensagemErro = env.IsDevelopment()
                ? exception.Message
                : "Ocorreu um erro interno no servidor. Tente novamente mais tarde.";

            var response = ResultResponse.CriarFalha([mensagemErro], "Erro interno no servidor.");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
