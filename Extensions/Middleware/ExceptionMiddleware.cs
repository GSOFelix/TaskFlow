using System.Net;
using System.Text.Json;
using TaskFlow.Application.Dtos.ErrorDto;
using TaskFlow.Domain.Exceptions;

namespace TaskFlow.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerException(ex, context);
            }
        }

        private static Task HandlerException(Exception ex, HttpContext context)
        {
            var statusCode = ex switch
            {
                NotFoundException => HttpStatusCode.NotFound,

                BadRequestException => HttpStatusCode.BadRequest,

                UnauthorizeException => HttpStatusCode.Unauthorized,

                ForbiddenException => HttpStatusCode.Forbidden,

                InternalErrorException => HttpStatusCode.InternalServerError,

                _ => HttpStatusCode.InternalServerError

            };

            var response = new ErroResponseDto
            {
                StatusCode = (int)statusCode,
                Message = ex.Message,
                Details = ex.InnerException?.Message ?? string.Empty,
                Path = context.Request.Path,
            };


            var json = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(json);
        }
    }
}
