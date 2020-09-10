using System;
using Microsoft.AspNetCore.Builder;

namespace Api.Helpers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseImperiusExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ImperiusExceptionMiddleware>();
        }
    }
}
