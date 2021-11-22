using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SupplierSvc1.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!!Error: {ex}");
                await Return500Async(context);
            }
        }

        private Task Return500Async(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new { message = $"An internal error occurred" }));
        }

        private readonly RequestDelegate _next;
    }
}
