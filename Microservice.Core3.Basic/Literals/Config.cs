namespace Microservice.Core3.Basic.Literals
{
    public static class Config
    {
        public const string ApiController = "api/[controller]";
        public const string ApplicationJson = "application/json";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";

        public static string LogLevel => Startup.Configuration["Logging:LogLevel:Default"];
    }
}
