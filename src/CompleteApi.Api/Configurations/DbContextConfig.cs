using CompleteApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompleteApi.Api.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MvcDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
