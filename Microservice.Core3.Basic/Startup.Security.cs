using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    b => b.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
