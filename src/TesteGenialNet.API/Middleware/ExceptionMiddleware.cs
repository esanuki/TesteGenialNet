using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace TesteGenialNet.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var json = new
                {
                    Error = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(json));

                return;
            }
        }
    }
}
