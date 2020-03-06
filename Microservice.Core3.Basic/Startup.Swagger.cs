using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        private const string TitleV1 = "v1";
        private static readonly string ApiName = Assembly.GetExecutingAssembly().GetName().Name;

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, ApiName + ".xml");
                o.IncludeXmlComments(filePath);
                o.SwaggerDoc(TitleV1, new OpenApiInfo { Title = ApiName, Version = "v1" });
            });
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            // This is to make it work with my kubernetes structure, you can use: app.UseSwagger();
            string processName = Process.GetCurrentProcess().ProcessName;
            bool isLocal = processName == "iisexpress" || processName == ApiName;
            string basePath = isLocal ? "" : "/FoLdEr_SeRvEr_NaMe"; // ToDo: Remember change this

            app.UseSwagger(o =>
                o.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    swaggerDoc.Servers.Add(new OpenApiServer { Url = basePath })));

            app.UseSwaggerUI(o =>
            {
                o.DisplayOperationId();
                o.DocumentTitle = ApiName;
                o.SwaggerEndpoint($"{basePath}/swagger/{TitleV1}/swagger.json", " V1");
            });
        }

    }
}
