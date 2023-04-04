using Interfaces;
using LoggerService;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace RsoApplicationApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureApiDma(this IServiceCollection services, IConfiguration configuration) =>
            services.AddHttpClient("DMAClient", client =>
            {
                var urlBase = Environment.GetEnvironmentVariable("UriDma") ?? configuration["UriDma"];
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Roll Shop Optimizer Backend", Version = "v1" });
            });
        }
    }
}
