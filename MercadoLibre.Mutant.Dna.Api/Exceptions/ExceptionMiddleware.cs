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
                    ErrorDetails errorDetails;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;
                    if (exception.Message.Contains("Index"))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorDetails = new ErrorDetails("Array is not well formed");
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorDetails = new ErrorDetails("Internal Server error");
                    }
                    Console.WriteLine(exception.Message);
                    var json = JsonConvert.SerializeObject(errorDetails);
                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
