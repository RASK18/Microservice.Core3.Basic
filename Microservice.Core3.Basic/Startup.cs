using AutoMapper;
using Microservice.Core3.Basic.Configurations.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder builder = services.AddControllers();

            InjectDependencies(services);
            ConfigureSwagger(services);
            ConfigureJsonSettings(builder);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Customize global external 400 bad request produced before the request enters into the application
            services.Configure<ApiBehaviorOptions>(a => a.InvalidModelStateResponseFactory = b =>
                throw new BadRequestCustomException(b.ModelState.Values.ToList().FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage));
        }

        // ReSharper disable once UnusedMember.Global
        public static void Configure(IApplicationBuilder app)
        {
            // Middleware for internal errors produced after the request is already in the application
            app.UseMiddleware<ExceptionsMiddleware>();

            ConfigureSwagger(app);

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
