using MovieStore.Services;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;

namespace MovieStore.MiddleWares
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IloggerService _logger;

        public CustomExceptionMiddleWare(RequestDelegate next, IloggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {

                string message = "[Request] HTTP " + context.Request.Method.ToString() + " - " + context.Request.Path.ToString();
                _logger.Write(message);
                await _next(context);
                watch.Stop();
                message = "[Request] HTTP " + context.Request.Method.ToString() + " - " + context.Request.Path.ToString() + " responsed " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _logger.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);

            }

        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {

            context.Response.ContentType = "aplication/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[ERROR]  HTTP  " + context.Request.Method.ToString() + context.Response.StatusCode.ToString() + " ERROR MESSAGE " + ex.Message + " in " + watch.Elapsed.Milliseconds + " ms ";
            _logger.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }
    public static class CustomExceptionMiddleWareExtension
    {
        public static IApplicationBuilder useCustomExceptionMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}