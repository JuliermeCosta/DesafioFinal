using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace DesafioFinal.Adapter.In.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EnableBufferingAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var request = context.HttpContext.Request;

            //1. Habilita o buffering no HttpContext antes de qualquer Model Binding
            request.EnableBuffering();

            //2. Garante que o stream possa ser lido do início
            request.Body.Position = 0;

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                var bodyString = await reader.ReadToEndAsync();

                //3. Guarda o JSON em texto no HttpContext para usar na Controller
                context.HttpContext.Items["RawRequestBody"] = bodyString;

                //4. ESSENCIAL: Volta o ponteiro para o início para que o Model Binder consiga ler o DTO
                request.Body.Position = 0;
            }

            await next();
        }
    }
}
