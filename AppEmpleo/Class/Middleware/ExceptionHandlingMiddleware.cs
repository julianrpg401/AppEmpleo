using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace AppEmpleo.Class.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unhandled exception");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<h2>Ocurrió un error inesperado. Por favor, inténtelo de nuevo más tarde.</h2>");
            }
        }
    }
}
