using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System;
using TesteGenialNet.Business.Models;
using TesteGenialNet.Business.Services;
using TesteGenialNet.Data;
using Microsoft.Extensions.Hosting;
using TesteGenialNet.API.Middleware;

namespace TesteGenialNet.API.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder) 
        {
            builder.Services.AddDbContext<TesteGenialNetContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.Configure<Configuration>(builder.Configuration.GetSection("Configuration"));

            builder.Services.AddHttpClient<ConsultaCepService>((serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;
                client.BaseAddress = new Uri(config.UrlCep);
            });

            return builder;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            return app;
        }
    }
}
