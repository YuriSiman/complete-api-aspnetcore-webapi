using CompleteApi.Api.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompleteApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextConfiguration(Configuration);
            services.AddIdentityConfiguration(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddMvcConfiguration();
            services.AddApiBehaviorOptionsConfiguration();
            services.ResolveDependencies();
            services.AddSwaggerConfiguration();
            services.AddHealthChecksConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIdentityConfiguration();
            app.UseMvcConfiguration(env);
            app.UseHealthChecksConfiguration();
        }
    }
}
