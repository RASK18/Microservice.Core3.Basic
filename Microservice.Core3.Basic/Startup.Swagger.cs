using System;
using System.IO;
using System.Reflection;
using Microservice.Core3.Basic.Literals;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        private static readonly string ApiName = Assembly.GetExecutingAssembly().GetName().Name;

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ApiName, Version = "v1" });

                string filePath = Path.Combine(AppContext.BaseDirectory, ApiName + ".xml");
                c.IncludeXmlComments(filePath);
            });
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = ApiName;
                c.SwaggerEndpoint(Config.SwaggerEndpoint, " V1");
                c.DisplayOperationId();
            });
        }

    }
}
