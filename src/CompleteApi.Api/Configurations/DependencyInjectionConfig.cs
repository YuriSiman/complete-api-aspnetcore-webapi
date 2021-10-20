using CompleteApi.Business.Interfaces;
using CompleteApi.Data.Context;
using CompleteApi.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CompleteApi.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApiDbContext>();

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            return services;
        }
    }
}
