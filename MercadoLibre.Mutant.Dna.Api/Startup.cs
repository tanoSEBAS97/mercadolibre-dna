using Amazon;
using Amazon.DynamoDBv2;
using MercadoLibre.Mutant.Dna.Api.Exceptions;
using MercadoLibre.Mutant.Dna.Core.Repositories;
using MercadoLibre.Mutant.Dna.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace MercadoLibre.Mutant.Dna.Api
{
    public class Startup
    {
        private RegionEndpoint regionEndpoint = RegionEndpoint.USEast1;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAmazonDynamoDB>(x => new AmazonDynamoDBClient(regionEndpoint));
            services.AddControllers();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddScoped<IDnaServiceCore, DnaServiceCore>();
            services.AddTransient<IDnaRepository, DnaDynamoRepository>();
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            });
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/ping");
                endpoints.MapControllers();
            });
        }
    }
}
