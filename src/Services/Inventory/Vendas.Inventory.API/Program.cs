using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Vendas.EventBus.Abstractions;
using Vendas.EventBus.RabbitMQ;
using Vendas.Inventory.Application.Interfaces.Servicos;
using Vendas.Inventory.Application.Services;
using Vendas.Inventory.Domain.Interfaces.Repository;
using Vendas.Inventory.Infrastructure.Data.Context;
using Vendas.Inventory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var factory = new ConnectionFactory
{
    HostName = "localhost", 
    UserName = "guest",
    Password = "guest",
    Port = 5672 
};

var connection = await factory.CreateConnectionAsync();
builder.Services.AddSingleton(connection);
builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//To-do: Centralizar instâncias em application e infra
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();