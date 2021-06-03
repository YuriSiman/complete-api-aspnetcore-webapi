using CompleteApi.Business.Interfaces;
using CompleteApi.Business.Notifications;
using CompleteApi.Business.Services;
using CompleteApi.Data.Context;
using CompleteApi.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CompleteApi.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MvcDbContext>();

            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
