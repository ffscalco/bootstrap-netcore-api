using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Helpers;

namespace Api.Helpers
{
    public class ImperiusExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ImperiusExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (AppException ex)
            {
                await HandleAppExceptionAsync(context, ex).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message,
                ErrorStack = exception.StackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }

        private static Task HandleAppExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(exception.Message);
        }
    }
}
