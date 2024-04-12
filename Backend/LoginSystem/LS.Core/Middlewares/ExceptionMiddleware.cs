using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Builder;

namespace LS.Core.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        public ILogger<ExceptionMiddleware> _logger { get; }
        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong");
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ResponseError
            {
                ErrorCode = statusCode,
                ErrorMessage = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }


    public static class ExceptionMiddlewareExtention
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}
