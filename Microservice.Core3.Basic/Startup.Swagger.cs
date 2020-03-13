using Microservice.Core3.Basic.Configurations.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                o.OperationFilter<AddDefaultValues>();
                o.OperationFilter<AddErrorResponses>();
                o.SwaggerDoc(TitleV1, new OpenApiInfo { Title = ApiName, Version = "v1" });
            });

            services.AddSwaggerGenNewtonsoftSupport();
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

    public class AddDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IList<OpenApiSchema> schemas = context.SchemaRepository.Schemas
                                           .Select(s => s.Value)
                                           .SelectMany(v => v.Properties)
                                           .Select(p => p.Value)
                                           .Where(v => v.Default != null)
                                           .ToList();

            foreach (OpenApiSchema schema in schemas)
                schema.Example = schema.Default;
        }
    }

    public class AddErrorResponses : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ExceptionDto), context.SchemaRepository);

            OpenApiReference reference = new OpenApiReference { Id = typeof(ExceptionDto).Name, Type = ReferenceType.Schema };
            OpenApiSchema schema = new OpenApiSchema { Reference = reference };
            OpenApiMediaType mediaType = new OpenApiMediaType { Schema = schema };
            Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType> { { "application/json", mediaType } };

            operation.Responses.Add("400", new OpenApiResponse { Description = StatusDetails.Title400, Content = content });
            operation.Responses.Add("500", new OpenApiResponse { Description = StatusDetails.Title500, Content = content });
        }
    }

}
