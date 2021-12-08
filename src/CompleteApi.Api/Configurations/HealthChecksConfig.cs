using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompleteApi.Api.Configurations
{
    public static class HealthChecksConfig
    {
        public static IServiceCollection AddHealthChecksConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            //services.AddHealthChecksUI();

            return services;
        }

        public static IApplicationBuilder UseHealthChecksConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/hc");

            //app.UseHealthChecksUI(options =>
            //    options.UIPath = "/api/hc-ui");

            return app;
        }
    }
}
