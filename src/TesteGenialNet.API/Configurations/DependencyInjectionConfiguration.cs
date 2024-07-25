using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TesteGenialNet.Business.Interfaces.Querys;
using TesteGenialNet.Business.Interfaces.Repositorys;
using TesteGenialNet.Business.Interfaces;
using TesteGenialNet.Business.Notificator;
using TesteGenialNet.Business.Querys;
using TesteGenialNet.Data.Repositorys;

namespace TesteGenialNet.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IProdutoFornecedorRepository, ProdutoFonecedorRepository>();
            builder.Services.AddScoped<IFornecedorQuery, FornecedorQuery>();
            builder.Services.AddScoped<IProdutoQuery, ProdutoQuery>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<INotificator, Notificator>();

            return builder;
        }
    }
}
