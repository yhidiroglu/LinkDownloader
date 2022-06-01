using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RainbowWebService.Models;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RainbowWebService.Middlewares
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			// default status
			var code = (int)HttpStatusCode.InternalServerError;

			var errorMessage = $"An error occurred on {DateTime.UtcNow} date! Exception is {ex}";

			Log.Error(errorMessage);

			var handledErrorMessage = $"An error occurred on {DateTime.UtcNow} date!";

			var result = new Response
			{
				Message = handledErrorMessage
			};

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = code;
			await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
		}
	}
}
