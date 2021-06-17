using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;

namespace MercadoLibre.Mutant.Dna.Api.Exceptions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Console.WriteLine(exception.Message);
                    ErrorDetails errorDetails = new ErrorDetails("Internal Server error");
                    var json = JsonConvert.SerializeObject(errorDetails);
                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
