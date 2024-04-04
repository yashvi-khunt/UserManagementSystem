using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Builder;

namespace LS.Core.Middlewares
{
    public class ExceptionMiddelware : IMiddleware
    {
        public ILogger<ExceptionMiddelware> _logger { get; }
        public ExceptionMiddelware(ILogger<ExceptionMiddelware> logger)
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

                _logger.LogError($"Somthing went wrong");
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ResponseError
            {
                errorCode = statusCode,
                errorMessage = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorResponse.ToString());
        }
    }


    public static class ExceptionMiddelwareExtention
    {
        public static void ConfigureExceptionMiddelware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddelware>();

        }
    }
}
