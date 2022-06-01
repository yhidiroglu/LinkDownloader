using Microsoft.AspNetCore.Http;
using RainbowWebService.Helper;
using RainbowWebService.Models;
using Serilog;
using System;
using System.Threading.Tasks;


namespace RainbowWebService.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var requestDate = DateTime.Now;
            var method = httpContext.Request.Method;
            string request = string.Empty;
            string endpoint = httpContext.Request.Path;

            if (httpContext.Request.QueryString.HasValue)
            {
                request = httpContext.Request.QueryString.Value;
            }

            await next(httpContext);

            int responseStatus = httpContext.Response.StatusCode;

            if (!endpoint.Contains("swagger"))
            {
                Log.Information($"Request Information: {method} " +
                    $"Date: {requestDate} " +
                    $"Endpoint: {endpoint} " +
                    $"Request: {request} " +
                    $"ResponseStatusCode: {responseStatus}");
            }
        }
    }
}
