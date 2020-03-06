using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Core3.Basic
{
    public partial class Startup
    {
        //using Microsoft.AspNetCore.Authentication.AzureAD.UI;
        //using Microsoft.IdentityModel.Tokens;

        //private static void ConfigureAuthentication(IServiceCollection services)
        //{
        //    services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
        //        .AddJwtBearer("AzureAD", o =>
        //            {
        //                o.Audience = Configuration["AzureAd:ClientId"];
        //                o.Authority = Configuration["AzureAd:Instance"] + Configuration["AzureAd:TenantId"];

        //                o.TokenValidationParameters = new TokenValidationParameters
        //                {
        //                    ValidAudience = o.Audience,
        //                    ValidIssuer = o.Authority + "/v2.0"
        //                };
        //            }
        //        );
        //}

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
