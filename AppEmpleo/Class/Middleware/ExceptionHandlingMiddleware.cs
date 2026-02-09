using System.Net;
using AppEmpleo.Class.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AppEmpleo.Class.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            const string defaultMessage = "Ocurrió un error inesperado. Por favor, inténtelo de nuevo más tarde.";
            try
            {
                await _next(context);
            }
            catch (AppValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error");
                await WriteErrorResponseAsync(context, HttpStatusCode.BadRequest, defaultMessage);
            }
            catch (AppAuthenticationException ex)
            {
                _logger.LogInformation(ex, "Authentication error");
                await WriteErrorResponseAsync(context, HttpStatusCode.Unauthorized, defaultMessage);
            }
            catch (AppAuthorizationException ex)
            {
                _logger.LogInformation(ex, "Authorization error");
                await WriteErrorResponseAsync(context, HttpStatusCode.Forbidden, defaultMessage);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Resource not found");
                await WriteErrorResponseAsync(context, HttpStatusCode.NotFound, defaultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await WriteErrorResponseAsync(context, HttpStatusCode.InternalServerError, defaultMessage);
            }
        }

        private static async Task WriteErrorResponseAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"<h2>{message}</h2>");
        }
    }
}
