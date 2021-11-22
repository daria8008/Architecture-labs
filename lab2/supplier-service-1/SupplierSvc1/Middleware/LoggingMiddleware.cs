using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SupplierSvc1.Middleware
{
    public class LoggingMiddleware
    {
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"!New request arrived at '{context.Request.Path}' with query '{context.Request.QueryString}'");
            await _next.Invoke(context);
        }

        private readonly RequestDelegate _next;
    }
}
