using Microservice.Core3.Basic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        private static void InjectDependencies(IServiceCollection services)
        {
            services.AddScoped<ValueService>();
        }
    }
}
