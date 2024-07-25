using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Notificator;
using TesteGenialNet.Data.Repositorys;
using TesteGenialNet.API.Queries;
using TesteGenialNet.Business.Interfaces.Queries;

namespace TesteGenialNet.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IProdutoFornecedorRepository, ProdutoFonecedorRepository>();
            builder.Services.AddScoped<IFornecedorQuerie, FornecedorQuerie>();
            builder.Services.AddScoped<IProdutoQuerie, ProdutoQuerie>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<INotificator, Notificator>();

            return builder;
        }
    }
}
