using Microsoft.AspNetCore.Builder;
using TesteGenialNet.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddApiConfiguration()
    .AddDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiConfiguration();

app.Run();
